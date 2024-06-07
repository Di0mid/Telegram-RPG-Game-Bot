﻿using Telegram_RPG_Game_Bot.Characters;
using Telegram_RPG_Game_Bot.Core;
using Telegram_RPG_Game_Bot.Database;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Managers;

public static class CharacterManager
{
    private static List<ChatUserCharacters> _chatUserCharacters = new();
    
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
        
        var character = new Character(_chatUserCharacters.Count + 1, data);
        if (TryGetChatUserCharacters(chat, out var chatUserCharacters))
        {
            chatUserCharacters.AddCharacter(user, character);
        }
        else
        {
            _chatUserCharacters.Add(new ChatUserCharacters(chat, new List<UserCharacters> { new(user, character) }));
        }
        
        MapManager.PlaceCharacter(character);
        await Bot.SendTextMessageAsync($"*{character.Name}*, добро пожаловать!");        
    }
    
    public static bool TryGetCharacter(Chat chat, User user, out Character character)
    {
        if (TryGetChatUserCharacters(chat, out var chatUserCharacters))
            return chatUserCharacters.TryGetCharacter(user, out character);
        
        character = null;
        return false;
    }
    
    private static bool TryGetChatUserCharacters(Chat chat, out ChatUserCharacters chatUserCharacters)
    {
        chatUserCharacters = _chatUserCharacters.FirstOrDefault(c => c.CompareChat(chat));
        
        return chatUserCharacters != null;
    }
    
    private static bool HasCharacter(Chat chat, User user)
    {
        return TryGetChatUserCharacters(chat, out var chatUserCharacters) && chatUserCharacters.HasUser(user);
    }

    #region SAVE AND LOAD
    
    public static async void SaveCharacters()
    {
        await SaveAndLoadManager.Save(_chatUserCharacters, SavePathDatabase.CharactersSavePath, nameof(_chatUserCharacters));
    }

    public static void LoadCharacters()
    {
        var characters = SaveAndLoadManager.Load<List<ChatUserCharacters>>(
            SavePathDatabase.CharactersSavePath, nameof(_chatUserCharacters));

        if (characters == null) return;
        
        _chatUserCharacters = characters;
        
        // TEST
        foreach (var character in _chatUserCharacters.SelectMany(
                     chatUserCharacters => chatUserCharacters.GetAllCharacters()))
        {
            MapManager.PlaceCharacter(character);
        }
        // TEST
    }
    
    #endregion
}