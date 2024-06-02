namespace Telegram_RPG_Game_Bot.Core;

public static class SavePathDatabase
{
    private const string CoreSavePath = "A:/C# Projects/Telegram RPG Game Bot/Save/";
    
    public static string CharactersSavePath { get; private set; } = CoreSavePath;
    public static string ChatsSavePath { get; private set; } = CoreSavePath;

}