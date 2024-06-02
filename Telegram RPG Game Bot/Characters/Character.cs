namespace Telegram_RPG_Game_Bot.Characters;

public class Character
{
    public Character(NewCharacterData data)
    {
        Name = data.Name;
    }

    public string Name { get; private set; }
}