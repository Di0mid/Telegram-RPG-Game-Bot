using System.Numerics;
using Telegram_RPG_Game_Bot.Characters;
using Telegram_RPG_Game_Bot.Core;
using Telegram_RPG_Game_Bot.Database;
using Telegram_RPG_Game_Bot.Map;

namespace Telegram_RPG_Game_Bot.Managers;

public static class MapManager
{
    private static GlobalMap _map = new(3, 3);
    private static List<CharacterOnMapData> _characterOnMapData = new();

    public static void MoveCharacter(Character character, Vector2 direction, int stepCount)
    {
        var data = GetCharacterOnMapData(character);

        var currentSectorId = data.SectorId;
        var nextSectorId = currentSectorId + direction * stepCount;
        
        if (_map.TryGetRegion(data.RegionId, out var region))
        {
            if (region.TryGetSector(nextSectorId, out var nextSector))
            {
                data.SectorId = nextSector.Id;
                FogOfWarManager.DiscoverSectorsOnWay(_map, direction, currentSectorId, nextSectorId, data);
            }
            else
            {
                var nextRegionId = data.RegionId + direction;
                if (!_map.TryGetRegion(nextRegionId, out region))
                    return;
            
                data.RegionId = region.Id;
                
                nextSectorId = currentSectorId with { Y = 0 };
                if (!region.TryGetSector(nextSectorId, out nextSector))
                    return;
            
                data.SectorId = nextSector.Id;
                FogOfWarManager.DiscoverSectorsOnWay(_map, direction, currentSectorId, nextSectorId, data);
            }
        }
        
        MapDisplayer.ShowMapRegion(_map, data);
    }
    
    public static void PlaceCharacter(Character character)
    {
        // TEST
        if(_characterOnMapData.Any(map => map.CharacterId == character.Id))
            return;
        // TEST
        
        var region = _map.GetRandomRegion();
        var sector = region.GetRandomSector();

        var data = new CharacterOnMapData(character, region.Id, sector.Id);
        _characterOnMapData.Add(data);
        
        FogOfWarManager.DiscoverSectorsAroundCharacter(_map, data);
    }
    
    public static void ShowMapRegion(Character character)
    {
        MapDisplayer.ShowMapRegion(_map, GetCharacterOnMapData(character));
    }

    public static void ShowMapSectorInfo(Character character)
    {
        MapDisplayer.ShowMapSectorInfo(_map, GetCharacterOnMapData(character));
    }
    
    private static CharacterOnMapData GetCharacterOnMapData(Character character)
    {
        return _characterOnMapData.First(map => map.CharacterId == character.Id);
    }

    #region SAVE AND LOAD
    
    public static async void Save()
    {
        await SaveAndLoadManager.Save(_map, SavePathDatabase.MapDataSavePath, nameof(_map));
        await SaveAndLoadManager.Save(_characterOnMapData, SavePathDatabase.MapDataSavePath,
            nameof(_characterOnMapData));
    }
    
    public static void GenerateOrLoad()
    {
        var map = SaveAndLoadManager.Load<GlobalMap>(SavePathDatabase.MapDataSavePath, nameof(_map));
        var characterOnMapData = SaveAndLoadManager.Load<List<CharacterOnMapData>>(SavePathDatabase.MapDataSavePath,
            nameof(_characterOnMapData));

        if (map == null)
        {
            _map.GenerateMap();
        }
        else
        {
            _map = map;
        }

        if (characterOnMapData != null)
        {
            _characterOnMapData = characterOnMapData;
        }
    }

    #endregion
}