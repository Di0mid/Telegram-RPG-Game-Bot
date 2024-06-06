using Telegram_RPG_Game_Bot.Map.Sector;

namespace Telegram_RPG_Game_Bot.Database;

public static class MapSectorDatabase
{
    public static MapSectorData Forest { get; private set; } = new("🌳", MapSectorType.Forest);
    public static MapSectorData Plain { get; private set; } = new("🌱", MapSectorType.Plain);
}