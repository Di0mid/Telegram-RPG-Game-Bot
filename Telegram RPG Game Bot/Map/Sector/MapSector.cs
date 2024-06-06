using System.Numerics;
using Newtonsoft.Json;

namespace Telegram_RPG_Game_Bot.Map.Sector;

[JsonObject(MemberSerialization.OptIn)]
public class MapSector
{
    [JsonConstructor]
    private MapSector(){}
    
    public MapSector(Vector2 id, MapSectorData data)
    {
        Id = id;
        Icon = data.Icon;
        Type = data.Type;
        TypeName = data.TypeName;
    }

    [JsonProperty]
    public Vector2 Id { get; private set; }
    
    [JsonProperty]
    public string Icon { get; private set; }

    [JsonProperty] 
    public MapSectorType Type { get; private set; }
    [JsonProperty]
    public string TypeName { get; private set; }

    public string Info()
    {
        return $"=== *СЕКТОР ({Id.X}, {Id.Y})* ===" +
               $"\n*Тип*: {TypeName}" +
               $"\n*Враги*: В разработке" +
               $"\n*Персонажи*: В разработке";
    }
}