namespace Telegram_RPG_Game_Bot.Map.Sector;

public static class MapSectorTypeNameMapping
{
    private static readonly Dictionary<MapSectorType, string> SectorTypeNameMapping = new()
    {
        {MapSectorType.Forest, "Лес"}, 
        {MapSectorType.Plain, "Равнина"}, 
    };

    public static string GetMapSectorName(MapSectorType type)
    {
        return SectorTypeNameMapping[type];
    }
}