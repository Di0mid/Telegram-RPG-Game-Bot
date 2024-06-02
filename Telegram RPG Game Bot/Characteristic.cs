namespace Telegram_RPG_Game_Bot;

public class Characteristic
{
    public Characteristic(string name, int value)
    {
        Name = name;
        Value = value;
        
        CalculateModifier();
    }

    public string Name { get; private set; }
    public int Value { get; private set; }
    public int Modifier { get; private set; }

    private void CalculateModifier()
    {
        Modifier = (Value - 10) / 2;
    }
}