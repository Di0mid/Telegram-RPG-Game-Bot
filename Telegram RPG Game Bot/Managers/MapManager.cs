using System.Numerics;
using Telegram_RPG_Game_Bot.Characters;
using Telegram_RPG_Game_Bot.Core;
using Telegram_RPG_Game_Bot.Map;

namespace Telegram_RPG_Game_Bot.Managers;

public static class MapManager
{
    private static GlobalMap _map = new(3, 3);

    public static void MoveCharacter(Character character, Vector2 direction, int stepCount)
    {
        _map.MoveCharacter(character, direction, stepCount);
    }
    
    public static void PlaceCharacter(Character character)
    {
        _map.PlaceCharacter(character);
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