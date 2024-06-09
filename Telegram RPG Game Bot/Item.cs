namespace Telegram_RPG_Game_Bot;

public abstract class Item
{
    public Item(string name)
    {
        Name = name;
    }
    
    public string Name { get; private set; }

    public string Info()
    {
        return $"*Название*: {Name}";
    }
}