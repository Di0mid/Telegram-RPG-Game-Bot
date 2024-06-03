using System.Numerics;
using Newtonsoft.Json;

namespace Telegram_RPG_Game_Bot.Map;

[JsonObject(MemberSerialization.OptIn)]
public class MapSector
{
    [JsonConstructor]
    public MapSector(Vector2 id, string icon)
    {
        Id = id;
        Icon = icon;
    }

    [JsonProperty]
    public Vector2 Id { get; private set; }
    
    [JsonProperty]
    public string Icon { get; private set; }
}