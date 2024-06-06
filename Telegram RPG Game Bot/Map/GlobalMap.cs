using System.Numerics;
using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Characters;
using Telegram_RPG_Game_Bot.Core;

namespace Telegram_RPG_Game_Bot.Map;

[JsonObject(MemberSerialization.OptIn)]
public class GlobalMap
{
    [JsonConstructor]
    private GlobalMap(){}

    public GlobalMap(int regionCountX, int regionCountY)
    {
        _regions = new MapRegion[regionCountX, regionCountY];
        _characterOnMapData = new List<CharacterOnMapData>();
    }
    
    [JsonProperty]
    private MapRegion[,] _regions;

    [JsonProperty]
    private List<CharacterOnMapData> _characterOnMapData;

    
    
    public void MoveCharacter(Character character, Vector2 movementDirection, int stepCount)
    {
        var characterOnMapData = GetCharacterOnMapData(character);

        var currentSectorId = characterOnMapData.SectorId;
        var nextSectorId = currentSectorId + movementDirection * stepCount;
        
        if (TryGetRegion(characterOnMapData.RegionId, out var nextRegion))
        {
            if (nextRegion.TryGetSector(nextSectorId, out var nextSector))
            {
                characterOnMapData.SectorId = nextSector.Id;
            }
            else
            {
                var nextRegionId = characterOnMapData.RegionId + movementDirection;
                if (!TryGetRegion(nextRegionId, out nextRegion))
                    return;
            
                characterOnMapData.RegionId = nextRegion.Id;
                
                nextSectorId = currentSectorId with { Y = 0 };
                if (!nextRegion.TryGetSector(nextSectorId, out nextSector))
                    return;
            
                characterOnMapData.SectorId = nextSector.Id;
            }
        }

        ShowMap(characterOnMapData);
    }
    
    public void PlaceCharacter(Character character)
    {
        // TEST
        if(_characterOnMapData.Any(map => map.CharacterId == character.Id))
            return;
        // TEST
        
        var region = GetRandomRegion();
        var sector = region.GetRandomSector();
        
        _characterOnMapData.Add(new CharacterOnMapData(character, region.Id, sector.Id));
    }
    
    public void GenerateMap()
    {
        for (var y = 0; y < _regions.GetLength(0); y++)
        {
            for (var x = 0; x < _regions.GetLength(1); x++)
            {
                _regions[y, x] = new MapRegion(new Vector2(y, x));
            }
        }
    }

    public async void ShowSectorInfo(Character character)
    {
        var characterOnMapData = GetCharacterOnMapData(character);
        var region = GetRegion(characterOnMapData.RegionId);
        var sector = region.GetSector(characterOnMapData.SectorId);
        
        await Bot.SendTextMessageAsync(sector.Info());
    }
    
    public void ShowMap(Character character)
    {
        if(_characterOnMapData.Count == 0)
            return;

        var characterOnMapData = GetCharacterOnMapData(character);
        ShowMap(characterOnMapData);
    }

    private void ShowMap(CharacterOnMapData characterOnMapData)
    {
        if (TryGetRegion(characterOnMapData.RegionId, out var region))
        {
            region.ShowRegion(characterOnMapData);
        }
    }
    
    private MapRegion GetRandomRegion()
    {
        var random = new Random();
        return _regions[random.Next(0, _regions.GetLength(0)), random.Next(0, _regions.GetLength(1))];
    }

    private bool TryGetRegion(Vector2 id, out MapRegion region)
    {
        if ((id.X > _regions.GetLength(0) - 1 || id.X < 0) || 
            (id.Y > _regions.GetLength(1) - 1 || id.Y < 0))
        {
            region = null;
            return false;
        }

        region = GetRegion(id);
        return true;
    }

    private MapRegion GetRegion(Vector2 id)
    {
        return _regions[(int)id.X, (int)id.Y];
    }

    private CharacterOnMapData GetCharacterOnMapData(Character character)
    {
        return _characterOnMapData.First(map => map.CharacterId == character.Id);
    }
}