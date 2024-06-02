using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Characters;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot;

public struct UserCharacters
{
    public UserCharacters(User user, Character character)
    {
        _user = user;
        Character = character;
    }

    [JsonProperty("User")]
    private User _user;
    
    [JsonProperty("Character")]
    public readonly Character Character;

    public bool Equals(User user)
    {
        return _user.Id == user.Id;
    }
}