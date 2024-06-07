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
        FogOfWarIcon = "🌫";
        
        Id = id;
        Icon = FogOfWarIcon;
        Type = data.Type;
        TypeName = data.TypeName;

        _baseIcon = data.Icon;

        IsDiscovered = false;
    }

    [JsonProperty]
    public Vector2 Id { get; private set; }
    
    [JsonProperty]
    public string Icon { get; private set; }
    [JsonProperty]
    public string FogOfWarIcon { get; private set; }

    [JsonProperty]
    private string _baseIcon;

    [JsonProperty] 
    public MapSectorType Type { get; private set; }
    [JsonProperty]
    public string TypeName { get; private set; }

    [JsonProperty]
    public bool IsDiscovered { get; private set; }

    public void UpdateIcon()
    {
        
    }
    
    public void Discover()
    {
        Icon = _baseIcon;
        IsDiscovered = true;
    }
    
    public string Info()
    {
        return $"=== *СЕКТОР ({Id.X}, {Id.Y})* ===" +
               $"\n*Тип*: {TypeName}" +
               $"\n*Враги*: В разработке" +
               $"\n*Персонажи*: В разработке";
    }
}