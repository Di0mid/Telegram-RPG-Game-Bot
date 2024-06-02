namespace Telegram_RPG_Game_Bot.Characters;

public struct NewCharacterData
{
    public NewCharacterData(string name)
    {
        Name = name;
    }

    public readonly string Name;
}