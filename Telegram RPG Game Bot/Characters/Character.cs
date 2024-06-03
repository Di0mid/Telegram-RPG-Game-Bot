using Newtonsoft.Json;

namespace Telegram_RPG_Game_Bot.Characters;

[JsonObject(MemberSerialization.OptIn)]
public class Character
{
    [JsonConstructor]
    private Character() { }
    
    public Character(int id, NewCharacterData data)
    {
        Id = id;
        MapIcon = "🧝";
        
        Name = data.Name;

        Level = 1;
        Experience = 0;
        Coins = 100;

        _characteristics = new Characteristics(10, 10, 10);
    }
    
    [JsonProperty]
    public int Id { get; private set; }
    
    [JsonProperty] 
    public string MapIcon { get; private set; }
    
    [JsonProperty]
    public string Name { get; private set; }
    
    [JsonProperty]
    public int Level { get; private set; }
    
    [JsonProperty]
    public int Experience { get; private set; }
    
    [JsonProperty]
    public int Coins { get; private set; }
    
    [JsonProperty] 
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
