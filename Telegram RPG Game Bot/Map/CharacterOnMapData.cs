using System.Numerics;
using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Characters;

namespace Telegram_RPG_Game_Bot.Map;

public class CharacterOnMapData
{
    
    public CharacterOnMapData(Character character, Vector2 regionId, Vector2 sectorId)
    {
        Character = character;
        RegionId = regionId;
        SectorId = sectorId;
    }

    [JsonProperty]
    public Character Character { get; private set; }

    [JsonProperty]
    public Vector2 RegionId { get; private set; }
    [JsonProperty]
    public Vector2 SectorId { get; private set; }

    public void UpdateSectorId(Vector2 id)
    {
        SectorId = id;
    }
}