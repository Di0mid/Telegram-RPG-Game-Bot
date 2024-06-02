using Newtonsoft.Json;

namespace Telegram_RPG_Game_Bot.Characters;

public class Character
{
    public Character(NewCharacterData data)
    {
        Name = data.Name;
        _characteristics = new Characteristics(10, 10, 10);
    }
    
    public string Name { get; private set; }
    
    [JsonProperty] private Characteristics _characteristics;
}
