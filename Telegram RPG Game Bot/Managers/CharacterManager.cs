using Telegram_RPG_Game_Bot.Characters;
using Telegram_RPG_Game_Bot.Core;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Managers;

public static class CharacterManager
{
    private static List<ChatUserCharacterPair> _characters = new();

    public static async void TryCreateCharacter(Chat chat, User user, NewCharacterData characterData)
    {
        if (HasCharacter(chat, user))
        {
            await Bot.SendTextMessageAsync($"@{user.Username}, у тебя уже есть персонаж");
            return;
        }

        var character = new Character(characterData);
        if (HasChat(chat))
        {
            var chatUsers = _characters.First(c => c.CompareChat(chat));
            chatUsers.AddUserCharacterPair(new UserCharacterPair(user, character));
        }
        else
        {
            _characters.Add(new ChatUserCharacterPair(chat, new List<UserCharacterPair> { new(user, character) }));
        }
        
        await Bot.SendTextMessageAsync($"*{character.Name}*, добро пожаловать!");        
    }

    #region CHECKS
    
    private static bool HasCharacter(Chat chat, User user)
    {
        if (HasChat(chat))
        {
            var chatUserCharacterPair = _characters.First(c => c.CompareChat(chat));
            return chatUserCharacterPair.HasUser(user);
        }

        return false;
    }

    private static bool HasChat(Chat chat)
    {
        return _characters.Any(c => c.CompareChat(chat));
    }

    #endregion

    #region SAVE AND LOAD
    
    public static async void SaveCharacters()
    {
        await SaveAndLoadManager.Save(_characters, SavePathDatabase.CharactersSavePath, nameof(_characters));
    }

    public static void LoadCharacters()
    {
        var characters = SaveAndLoadManager.Load<List<ChatUserCharacterPair>>(
            SavePathDatabase.CharactersSavePath, nameof(_characters));
        
        if(characters != null)
            _characters = characters;
    }
    
    #endregion
}