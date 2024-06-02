using Newtonsoft.Json;

namespace Telegram_RPG_Game_Bot.Characters;

public class Character
{
    public Character(NewCharacterData data)
    {
        Name = data.Name;

        Level = 1;
        Experience = 0;
        Coins = 100;

        _characteristics = new Characteristics(10, 10, 10);
    }
    
    [JsonProperty("Name")]
    public string Name { get; private set; }
    
    [JsonProperty("Level")]
    public int Level { get; private set; }
    
    [JsonProperty("Experience")]
    public int Experience { get; private set; }
    
    [JsonProperty("Coins")]
    public int Coins { get; private set; }
    
    [JsonProperty("Characteristics")] 
    private Characteristics _characteristics;

    public string MainInfo()
    {
        return $"=== *ПЕРСОНАЖ* ====" +
               $"\n" +
               $"\n*Имя*: {Name}" +
               $"\n*Уровень*: {Level}" +
               $"\n*Опыт*: {Experience}" +
               $"\n*Монеты*: {Coins}" +
               $"\n" +
               $"\n{_characteristics.Info()}";
    }
}
