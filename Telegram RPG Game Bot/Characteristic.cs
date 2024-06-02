using Newtonsoft.Json;

namespace Telegram_RPG_Game_Bot;

public class Characteristic
{
    public Characteristic(string name, int value)
    {
        Name = name;
        Value = value;
        
        CalculateModifier();
    }

    [JsonProperty("Name")]
    public string Name { get; private set; }
    
    [JsonProperty("Value")]
    public int Value { get; private set; }
    
    [JsonProperty("Modifier")]
    public int Modifier { get; private set; }

    private void CalculateModifier()
    {
        Modifier = (Value - 10) / 2;
    }

    public string Info()
    {
        return $"*{Name}*: {Value}({Modifier})";
    }
}