using System.Numerics;
using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Characters;

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

    public void MoveCharacter(Character character, Vector2 movementDirection)
    {
        var characterOnMapData = GetCharacterOnMapData(character);

        var nextSectorId = characterOnMapData.SectorId + movementDirection;
        if (!GetRegion(characterOnMapData.RegionId).TryGetSector(nextSectorId, out var sector)) 
            return;
        
        characterOnMapData.UpdateSectorId(sector.Id);
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

    public void ShowMap(Character character)
    {
        if(_characterOnMapData.Count == 0)
            return;

        var characterOnMapData = GetCharacterOnMapData(character);
        ShowMap(characterOnMapData);
    }

    private void ShowMap(CharacterOnMapData characterOnMapData)
    {
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
        return _characterOnMapData.First(map => map.CharacterId == character.Id);
    }
}