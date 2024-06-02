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

    //[JsonProperty("Strength")] 
    [JsonProperty] 
    private Characteristic _strength;
    
    [JsonProperty] 
    private Characteristic _dexterity;
    
    [JsonProperty] 
    private Characteristic _constitution;
}