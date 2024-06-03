using System.Numerics;
using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Characters;

namespace Telegram_RPG_Game_Bot.Map;

public class GlobalMap
{
    [JsonProperty]
    private MapRegion[,] _regions = new MapRegion[3, 3];

    [JsonProperty]
    private List<CharacterOnMapData> _characterOnMapData = new();

    public void MoveCharacter(Character character)
    {
        var characterOnMapData = GetCharacterOnMapData(character);

        var nextSectorId = characterOnMapData.SectorId - new Vector2(1, 0);
        if (!GetRegion(characterOnMapData.RegionId).TryGetSector(nextSectorId, out var sector)) 
            return;
        
        characterOnMapData.UpdateSectorId(sector.Id);
        ShowMap(character);
    }
    
    public void TryPlaceCharacter(Character character)
    {
        if(_characterOnMapData.Any(map => map.Character.Id == character.Id))
            return;
        
        var region = GetRandomRegion();
        var sector = region.GetRandomSector();
        
        _characterOnMapData.Add(new CharacterOnMapData(character, region.Id, sector.Id));
    }
    
    public void GenerateMap()
    {
        for (var i = 0; i < _regions.GetLength(0); i++)
        {
            for (var j = 0; j < _regions.GetLength(1); j++)
            {
                _regions[i, j] = new MapRegion(new Vector2(j, i));
            }
        }
    }

    public void ShowMap(Character character)
    {
        if(_characterOnMapData.Count == 0)
            return;

        var characterOnMapData = GetCharacterOnMapData(character);
        GetRegion(characterOnMapData.RegionId).ShowRegion(characterOnMapData);
    }

    private MapRegion GetRandomRegion()
    {
        var random = new Random();
        return _regions[random.Next(0, _regions.GetLength(0)), random.Next(0, _regions.GetLength(1))];
    }

    private MapRegion GetRegion(Vector2 id)
    {
        return _regions[(int)id.X, (int)id.Y];
    }

    private CharacterOnMapData GetCharacterOnMapData(Character character)
    {
        return _characterOnMapData.First(map => map.Character.Id == character.Id);
    }
}