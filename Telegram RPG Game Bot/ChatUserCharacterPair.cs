using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot;

public struct ChatUserCharacterPair
{
    public ChatUserCharacterPair(Chat chat, List<UserCharacterPair> userCharacterPair)
    {
        _chat = chat;
        _userCharacterPair = userCharacterPair;
    }
        
    [JsonProperty("Chat")]
    private Chat _chat;
    [JsonProperty("UserCharacterPair")] 
    private List<UserCharacterPair> _userCharacterPair;

    public void AddUserCharacterPair(UserCharacterPair userCharacter)
    {
        _userCharacterPair.Add(userCharacter);
    }

    public bool CompareChat(Chat chat)
    {
        return _chat.Id == chat.Id;
    }

    public bool HasUser(User user)
    {
        return _userCharacterPair.Any(ucp => ucp.CompareUser(user));
    }
}