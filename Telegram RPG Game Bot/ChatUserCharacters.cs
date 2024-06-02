using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Characters;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot;

public struct ChatUserCharacters
{
    public ChatUserCharacters(Chat chat, List<UserCharacters> userCharacters)
    {
        _chat = chat;
        _userCharacters = userCharacters;
    }
        
    [JsonProperty]
    private Chat _chat;
    
    [JsonProperty] 
    private List<UserCharacters> _userCharacters;
    
    public void AddCharacter(User user, Character character)
    {
        _userCharacters.Add(new UserCharacters(user, character));
    }

    public bool Equals(Chat chat)
    {
        return _chat.Id == chat.Id;
    }

    public bool ContainsUser(User user)
    {
        return _userCharacters.Any(u => u.Equals(user));
    }

    public bool TryGetCharacter(User user, out Character character)
    {
        if (!ContainsUser(user))
        {
            character = null;
            return false;
        }

        character = _userCharacters.Find(p => p.Equals(user)).Character;
        return character != null;
    }
}