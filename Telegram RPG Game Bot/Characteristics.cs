using Newtonsoft.Json;

namespace Telegram_RPG_Game_Bot;

public class Characteristics
{
    public Characteristics(int str, int dex, int con)
    {
        _str = new Characteristic("СИЛ", str);
        _dex = new Characteristic("ЛОВ", dex);
        _con = new Characteristic("ТЕЛ", con);
    }

    [JsonProperty] 
    private Characteristic _str;
    
    [JsonProperty] 
    private Characteristic _dex;
    
    [JsonProperty] 
    private Characteristic _con;
}