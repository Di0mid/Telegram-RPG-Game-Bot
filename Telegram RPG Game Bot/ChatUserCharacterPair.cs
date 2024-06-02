using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot;

public struct ChatUserCharacterPair
{
    public ChatUserCharacterPair(Chat chat, List<UserCharacterPair> userCharacters)
    {
        _chat = chat;
        _userCharacters = userCharacters;
    }
        
    [JsonProperty]
    private Chat _chat;
    [JsonProperty] 
    private List<UserCharacterPair> _userCharacters;

    public void AddUserCharacterPair(UserCharacterPair userCharacter)
    {
        _userCharacters.Add(userCharacter);
    }

    public bool CompareChat(Chat chat)
    {
        return _chat.Id == chat.Id;
    }

    public bool HasUser(User user)
    {
        return _userCharacters.Any(ucp => ucp.CompareUser(user));
    }
}