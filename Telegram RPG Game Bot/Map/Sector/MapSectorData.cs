namespace Telegram_RPG_Game_Bot.Map.Sector;

public struct MapSectorData
{
    public MapSectorData(string icon, MapSectorType type)
    {
        Icon = icon;
        Type = type;
        TypeName = MapSectorTypeNameMapping.GetMapSectorName(Type);
    }

    public string Icon { get; private set; }
    public MapSectorType Type { get; private set; }
    public string TypeName { get; private set; }
}