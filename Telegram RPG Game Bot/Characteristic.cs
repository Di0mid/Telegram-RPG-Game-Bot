using Newtonsoft.Json;

namespace Telegram_RPG_Game_Bot;

[JsonObject(MemberSerialization.OptIn)]
public class Characteristic
{
    [JsonConstructor]
    private Characteristic(){}
    
    public Characteristic(string name, int value)
    {
        Name = name;
        Value = value;
        
        CalculateModifier();
    }

    [JsonProperty]
    public string Name { get; private set; }
    
    [JsonProperty]
    public int Value { get; private set; }
    
    [JsonProperty]
    public int Modifier { get; private set; }

    public void LevelUp()
    {
        Value++;
        CalculateModifier();
    }

    private void CalculateModifier()
    {
        Modifier = (Value - 10) / 2;
    }

    public string Info()
    {
        return $"*{Name.ToUpper()}*: {Value}({Modifier})";
    }
}