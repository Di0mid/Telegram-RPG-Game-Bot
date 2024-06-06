namespace Telegram_RPG_Game_Bot.Database;

public static class AvailableCharacterMapIcons
{
    private static readonly List<string> Icons = new()
    {
        "🥷🏻", "🦸‍♀️", "🦸", "🦸‍♂️", "🦹‍♀️", "🦹‍♂️", "🧙‍♀️", "🧙", "🧙‍♂️", "🧝‍♀️", "🧝", "🧝‍♂️", "🧌", "🧛‍♀️",
        "🧛", "🧛‍♂️", "🧜‍♀️", "🧜", "🧚‍♀️", "🧚", "🧍‍♀️", "🧍", "🧍‍♂️", "👻", "👽", "👾", "🤖", "🎃", "🤡", "👺",
        "👹", "😈", "🤠", "😎", "🥸", "🫥",
    };

    public static bool IconAvailable(string icon)
    {
        return Icons.Contains(icon);
    }

    public static string GetAvailableIcons()
    {
        return Icons.Aggregate("", (current, icon) => current + icon);
    }
}