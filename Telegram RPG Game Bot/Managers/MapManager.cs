using Telegram_RPG_Game_Bot.Characters;
using Telegram_RPG_Game_Bot.Core;
using Telegram_RPG_Game_Bot.Map;

namespace Telegram_RPG_Game_Bot.Managers;

public static class MapManager
{
    private static GlobalMap _map = new();

    public static void MoveCharacter(Character character)
    {
        _map.MoveCharacter(character);
    }
    
    public static void TryPlaceCharacter(Character character)
    {
        _map.TryPlaceCharacter(character);
    }
    
    public static void ShowMap(Character character)
    {
        _map.ShowMap(character);
    }

    #region SAVE AND LOAD
    
    public static async void SaveMap()
    {
        await SaveAndLoadManager.Save(_map, SavePathDatabase.MapSavePath, nameof(_map));
    }
    
    public static void GenerateOrLoadMap()
    {
        var map = SaveAndLoadManager.Load<GlobalMap>(SavePathDatabase.MapSavePath, nameof(_map));

        if (map == null)
        {
            _map.GenerateMap();
        }
        else
        {
            _map = map;
        }
    }

    #endregion
}