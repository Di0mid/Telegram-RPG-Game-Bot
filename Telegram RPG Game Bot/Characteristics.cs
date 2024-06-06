using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Core;

namespace Telegram_RPG_Game_Bot;

[JsonObject(MemberSerialization.OptIn)]
public class Characteristics
{
    [JsonConstructor]
    private Characteristics() {}
    
    public Characteristics(int strength, int dexterity, int constitution)
    {
        LevelUpPoints = 2;

        _strength = new Characteristic("СИЛ", strength);
        _dexterity = new Characteristic("ЛОВ", dexterity);
        _constitution = new Characteristic("ТЕЛ", constitution);
        
        _characteristics = new List<Characteristic>
        {
            _strength,
            _dexterity,
            _constitution
        };
    }

    [JsonProperty]
    public int LevelUpPoints { get; private set; }

    #region CHARACTERISTICS
    
    [JsonProperty]
    private List<Characteristic> _characteristics;

    [JsonProperty] 
    private Characteristic _strength;
    
    [JsonProperty] 
    private Characteristic _dexterity;
    
    [JsonProperty] 
    private Characteristic _constitution;

    #endregion
    
    public string TryLevelUp(string characteristicName)
    {
        if (LevelUpPoints == 0)
            return $"у тебя недостаточно очков прокачки характеристики - *{LevelUpPoints}*";

        var characteristic =
            _characteristics.FirstOrDefault(c => c.Name.ToLower().Equals(characteristicName.ToLower()));

        if (characteristic == null)
            return $"*\"{characteristicName}\"* - такой характеристики нет";

        characteristic.LevelUp();
        LevelUpPoints--;

        return $"значение твоей характеристики *{characteristic.Name}* успешно повышено до *{characteristic.Value}*!";
    }

    public string Info()
    {
        return $"=== *ХАРАКТЕРИСТИКИ* ===" +
               $"\n" +
               $"\n*Очки прокачки*: {LevelUpPoints}" +
               $"\n" +
               $"{_characteristics.Aggregate("", (current, c) => current + $"\n{c.Info()}")}";
    }
}