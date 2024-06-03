using System.Numerics;
using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Characters;

namespace Telegram_RPG_Game_Bot.Map;

public struct CharacterOnMap
{
    public CharacterOnMap(int characterId, Vector2 regionId, Vector2 sectorId)
    {
        CharacterId = characterId;
        RegionId = regionId;
        SectorId = sectorId;
    }

    [JsonProperty]
    public int CharacterId { get; private set; }
    [JsonProperty]
    public Vector2 RegionId { get; private set; }
    [JsonProperty]
    public Vector2 SectorId { get; private set; }
}