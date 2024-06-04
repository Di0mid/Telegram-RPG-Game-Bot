using System.Numerics;
using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Characters;

namespace Telegram_RPG_Game_Bot.Map;

[JsonObject(MemberSerialization.OptIn)]
public class CharacterOnMapData
{
    [JsonConstructor]
    private CharacterOnMapData() { }
    
    public CharacterOnMapData(Character character, Vector2 regionId, Vector2 sectorId)
    {
        CharacterId = character.Id;
        CharacterMapIcon = character.MapIcon;
        CharacterName = character.Name;
        
        RegionId = regionId;
        SectorId = sectorId;
    }

    [JsonProperty] 
    public int CharacterId { get; private set; }
    [JsonProperty]
    public string CharacterMapIcon { get; private set; }
    [JsonProperty]
    public string CharacterName { get; private set; }

    [JsonProperty]
    public Vector2 RegionId { get; set; }
    [JsonProperty]
    public Vector2 SectorId { get; set; }
}