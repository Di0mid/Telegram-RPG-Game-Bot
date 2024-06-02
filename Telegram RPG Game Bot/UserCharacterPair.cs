using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Characters;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot;

public struct UserCharacterPair
{
    public UserCharacterPair(User user, Character character)
    {
        _user = user;
        _character = character;
    }

    [JsonProperty("User")]
    private User _user;
    [JsonProperty("Character")]
    private Character _character;

    public bool CompareUser(User user)
    {
        return _user.Id == user.Id;
    }
}