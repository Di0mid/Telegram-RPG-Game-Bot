﻿using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Characters;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot;

[JsonObject(MemberSerialization.OptIn)]
public class ChatUserCharacters
{
    [JsonConstructor]
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

    public bool CompareChat(Chat chat)
    {
        return _chat.Id == chat.Id;
    }

    public bool HasUser(User user)
    {
        return _userCharacters.Any(userCharacters => userCharacters.CompareUser(user));
    }

    public bool TryGetCharacterByUser(User user, out Character character)
    {
        if (!HasUser(user))
        {
            character = null;
            return false;
        }
        
        character = _userCharacters.First(userCharacters => userCharacters.CompareUser(user)).Character;
        return true;
    }

    public bool TryGetCharacterByName(string characterName, out Character? character)
    {
        foreach (var userCharacter in _userCharacters.Where(userCharacter =>
                     userCharacter.Character.Name.Equals(characterName)))
        {
            character = userCharacter.Character;
            return true;
        }

        character = null;
        return false;
    }
    
    public List<Character> GetAllCharacters()
    {
        return _userCharacters.Select(userCharacters => userCharacters.Character).ToList();
    }
}