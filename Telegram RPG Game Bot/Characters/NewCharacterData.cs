namespace Telegram_RPG_Game_Bot.Characters;

public struct NewCharacterData
{
    public NewCharacterData(string name, string mapIcon)
    {
        Name = name;
        MapIcon = mapIcon;
    }

    public readonly string Name;
    public readonly string MapIcon;
}