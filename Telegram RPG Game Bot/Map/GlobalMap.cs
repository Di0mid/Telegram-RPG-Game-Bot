using System.Numerics;
using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Characters;

namespace Telegram_RPG_Game_Bot.Map;

public class GlobalMap
{
    [JsonProperty]
    private MapRegion[,] _regions = new MapRegion[3, 3];

    [JsonProperty]
    private List<CharacterOnMap> _characterOnMap = new();

    public void TryPlaceCharacter(int characterId)
    {
        if(_characterOnMap.Any(map => map.CharacterId == characterId))
            return;
        
        var region = GetRandomRegion();
        var sector = region.GetRandomSector();
        
        _characterOnMap.Add(new CharacterOnMap(characterId, region.Id, sector.Id));
    }
    
    public void GenerateMap()
    {
        for (var i = 0; i < _regions.GetLength(0); i++)
        {
            for (var j = 0; j < _regions.GetLength(1); j++)
            {
                _regions[i, j] = new MapRegion(new Vector2(i, j));
            }
        }
    }

    public void ShowMap(Character character)
    {
        if(_characterOnMap.Count == 0)
            return;
        
        var characterOnMap = _characterOnMap.First(map => map.CharacterId == character.Id);
        GetRegion(characterOnMap.RegionId).ShowRegion();
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
}