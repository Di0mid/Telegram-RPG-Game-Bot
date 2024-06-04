using System.Numerics;

namespace Telegram_RPG_Game_Bot.Map;

public static class MapMovementDirectionMapping
{
    private static readonly Dictionary<string, Vector2> MovementDirectionMapping = new()
    {
        { "север" , new Vector2(-1, 0)},
        { "северо-запад" , new Vector2(-1, -1)},
        { "северо-восток" , new Vector2(-1, 1)},
        
        { "юг" , new Vector2(1, 0)},
        { "юго-запад" , new Vector2(1, -1)},
        { "юго-восток" , new Vector2(1, 1)},
        
        { "восток" , new Vector2(0, 1)},
        { "запад" , new Vector2(0, -1)},
    };

    public static bool TryGetMovementDirection(string directionName, out Vector2 direction)
    {
        return MovementDirectionMapping.TryGetValue(directionName.ToLower(), out direction);
    }
}