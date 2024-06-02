using Telegram_RPG_Game_Bot.Characters;
using Telegram_RPG_Game_Bot.Core;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Managers;

public static class CharacterManager
{
    private static Dictionary<Chat, Dictionary<User, Character>> _characters = new();

    public static async void TryCreateCharacter(Chat chat, User user, NewCharacterData characterData)
    {
        if (HasCharacter(chat, user))
        {
            await Bot.SendTextMessageAsync($"@{user.Username}, у тебя уже есть персонаж");
            return;
        }

        CreateCharacter(chat, user, characterData);        
    }

    private static async void CreateCharacter(Chat chat, User user, NewCharacterData characterData)
    {
        var character = new Character(characterData);
        if (HasChat(chat))
        {
            _characters[chat].Add(user, character);
        }
        else
        {
            _characters.Add(chat, new Dictionary<User, Character> { { user, character } });
        }
        
        await Bot.SendTextMessageAsync($"*{character.Name}*, добро пожаловать!");
    }
    
    #region CHECKS
    
    private static bool HasCharacter(Chat chat, User user)
    {
        if (HasChat(chat))
        {
            return _characters[chat].Keys.Any(u => u.Id == user.Id);
        }

        return false;
    }

    private static bool HasChat(Chat chat)
    {
        return _characters.Keys.Any(c => c.Id == chat.Id);
    }

    #endregion
}