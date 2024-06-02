using Newtonsoft.Json;

namespace Telegram_RPG_Game_Bot;

public class Characteristics
{
    public Characteristics(int strength, int dexterity, int constitution)
    {
        _strength = new Characteristic("СИЛ", strength);
        _dexterity = new Characteristic("ЛОВ", dexterity);
        _constitution = new Characteristic("ТЕЛ", constitution);
    }

    [JsonProperty("Strength")] 
    private Characteristic _strength;
    
    [JsonProperty("Dexterity")] 
    private Characteristic _dexterity;
    
    [JsonProperty("Constitution")] 
    private Characteristic _constitution;

    public string Info()
    {
        return $"=== *ХАРАКТЕРИСТИКИ* ===" +
               $"\n" +
               $"\n{_strength.Info()}" +
               $"\n{_dexterity.Info()}" +
               $"\n{_constitution.Info()}";
    }
}