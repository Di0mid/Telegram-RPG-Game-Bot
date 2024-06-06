using Telegram_RPG_Game_Bot.Characters;
using Telegram_RPG_Game_Bot.Core;
using Telegram_RPG_Game_Bot.Database;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Managers;

public static class CharacterManager
{
    // TODO: Поработать над названием листа
    private static List<ChatUserCharacters> _characters = new();
    
    public static async void TryCreateCharacter(Chat chat, User user, NewCharacterData data)
    {
        if (HasCharacter(chat, user))
        {
            await Bot.SendTextMessageAsync($"@{user.Username}, у тебя уже есть персонаж");
            return;
        }

        if (!AvailableCharacterMapIcons.IconAvailable(data.MapIcon))
        {
            await Bot.SendTextMessageAsync($"\"{data.MapIcon}\" - эта иконка недоступна." +
                                           $"\nДоступные иконки:" +
                                           $"\n{AvailableCharacterMapIcons.GetAvailableIcons()}");
            return;
        }
        
        var character = new Character(_characters.Count + 1, data);
        if (HasChat(chat))
        {
            var chatUsers = _characters.First(c => c.CompareChat(chat));
            chatUsers.AddCharacter(user, character);
        }
        else
        {
            _characters.Add(new ChatUserCharacters(chat, new List<UserCharacters> { new(user, character) }));
        }
        
        MapManager.PlaceCharacter(character);
        await Bot.SendTextMessageAsync($"*{character.Name}*, добро пожаловать!");        
    }
    
    public static bool TryGetCharacter(Chat chat, User user, out Character character)
    {
        if (!HasChat(chat))
        {
            character = null;
            return false;
        }

        var chatUserCharacters = _characters.First(chatUserCharacters => chatUserCharacters.CompareChat(chat));
        
        return chatUserCharacters.TryGetCharacter(user, out character);
    }

    #region CHECKS
    
    private static bool HasCharacter(Chat chat, User user)
    {
        
        if (HasChat(chat))
        {
            var chatUserCharacters = _characters.First(chatUserCharacters => chatUserCharacters.CompareChat(chat));
            return chatUserCharacters.HasUser(user);
        }

        return false;
    }

    
    // TODO: Улучшить логику метода
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
        var characters = SaveAndLoadManager.Load<List<ChatUserCharacters>>(
            SavePathDatabase.CharactersSavePath, nameof(_characters));

        if (characters == null) return;
        
        _characters = characters;
        
        // TEST
        foreach (var character in _characters.SelectMany(
                     chatUserCharacters => chatUserCharacters.GetAllCharacters()))
        {
            MapManager.PlaceCharacter(character);
        }
        // TEST
    }
    
    #endregion
}