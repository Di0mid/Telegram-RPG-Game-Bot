using Telegram_RPG_Game_Bot.Characters;
using Telegram_RPG_Game_Bot.Core;
using Telegram_RPG_Game_Bot.Database;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Managers;

public static class CharacterManager
{
    //private static List<ChatUserCharacters> _chatUserCharacters = new();
    private static Dictionary<long, Dictionary<long, Character>> _characters = new();

    public static async void TryCreateCharacter(Chat chat, User user, NewCharacterData data)
    {
        if (HasCharacter(chat, user))
        {
            await Bot.SendTextMessageAsync($"@{user.Username}, у тебя уже есть персонаж");
            return;
        }

        if (!AvailableCharacterMapIcons.IconAvailable(data.MapIcon))
        {
            await Bot.SendTextMessageAsync($"\"{data.MapIcon}\" - эта иконка недоступна" +
                                           $"\nДоступные иконки:" +
                                           $"\n{AvailableCharacterMapIcons.GetAvailableIcons()}");
            return;
        }
        
        var newCharacter = new Character(_characters.Count + 1, data);
        if (HasChat(chat))
        {
            foreach (var character in _characters[chat.Id].Values
                         .Where(character => character.MapIcon.Equals(data.MapIcon)))
            {
                await Bot.SendTextMessageAsync(
                    $"\"{data.MapIcon}\" - эта иконка занята персонажем *{character.Name}*" +
                    $"\nДругие иконки:" +
                    $"\n{AvailableCharacterMapIcons.GetAvailableIcons()}");

                return;
            }

            _characters[chat.Id].Add(user.Id, newCharacter);
        }
        else
        {
            _characters.Add(chat.Id, new Dictionary<long, Character> { { user.Id, newCharacter } });
        }
        
        MapManager.PlaceCharacter(newCharacter);
        await Bot.SendTextMessageAsync($"*{newCharacter.Name}*, добро пожаловать!");        
    }

    #region CHARACTER GET
    
    public static bool TryGetCharacter(Chat chat, User user, out Character? character)
    {
        if (_characters.TryGetValue(chat.Id, out var userCharacters))
        {
            return userCharacters.TryGetValue(user.Id, out character);
        }

        character = null;
        return false;
    }
    
    public static bool TryGetCharacterByName(Chat chat, string characterName, out Character? character)
    {
        if (_characters.TryGetValue(chat.Id, out var userCharacters))
        {
            foreach (var c in userCharacters.Values.Where(c => c.Name.Equals(characterName)))
            {
                character = c;
                return true;
            }
        }

        character = null;
        return false;
    }

    public static bool TryGetCharacterById(long chatId, int characterId, out Character? character)
    {
        if (_characters.TryGetValue(chatId, out var userCharacters))
        {
            foreach (var c in userCharacters.Values.Where(c => c.Id == characterId))
            {
                character = c;
                return true;
            }
        }

        character = null;
        return false;
    }

    public static Character GetCharacterById(long chatId, int characterId)
    {
        return _characters[chatId].Values.First(c => c.Id == characterId);
    }

    #endregion
    
    private static bool HasCharacter(Chat chat, User user)
    {
        return _characters.ContainsKey(chat.Id) && _characters[chat.Id].ContainsKey(user.Id);
    }

    private static bool HasChat(Chat chat)
    {
        return _characters.ContainsKey(chat.Id);
    }

    #region SAVE AND LOAD
    
    public static async void SaveCharacters()
    {
        await SaveAndLoadManager.Save(_characters, SavePaths.CharactersSavePath, nameof(_characters));
    }

    public static void LoadCharacters()
    {
        var characters = SaveAndLoadManager.Load<Dictionary<long, Dictionary<long, Character>>>(
            SavePaths.CharactersSavePath, nameof(_characters));

        if (characters == null) 
            return;
        
        _characters = characters;
        
        // TEST
        /*foreach (var character in _characters.SelectMany(
                     chatUserCharacters => chatUserCharacters.GetAllCharacters()))
        {
            MapManager.PlaceCharacter(character);
        }*/
        // TEST
    }
    
    #endregion
}