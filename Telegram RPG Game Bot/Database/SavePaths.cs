namespace Telegram_RPG_Game_Bot.Database;

public static class SavePaths
{
    private const string CoreSavePath = "A:/C# Projects/Telegram RPG Game Bot/Save/";
    
    public static string CharactersSavePath { get; private set; } = CoreSavePath;
    public static string ChatsSavePath { get; private set; } = CoreSavePath;
    public static string MapDataSavePath { get; private set; } = CoreSavePath;
    public static string TeamsSavePath { get; private set; } = CoreSavePath;

}