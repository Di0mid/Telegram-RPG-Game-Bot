using System.Numerics;
using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Characters;
using Telegram_RPG_Game_Bot.Core;

namespace Telegram_RPG_Game_Bot.Map;

[JsonObject(MemberSerialization.OptIn)]
public class GlobalMap
{
    [JsonConstructor]
    private GlobalMap(){}

    public GlobalMap(int regionCountX, int regionCountY)
    {
        _regions = new MapRegion[regionCountX, regionCountY];
    }
    
    [JsonProperty]
    private MapRegion[,] _regions;

    public void GenerateMap()
    {
        for (var y = 0; y < _regions.GetLength(0); y++)
        {
            for (var x = 0; x < _regions.GetLength(1); x++)
            {
                _regions[y, x] = new MapRegion(new Vector2(y, x));
            }
        }
    }

    public MapRegion GetRandomRegion()
    {
        var random = new Random();
        return _regions[random.Next(0, _regions.GetLength(0)), random.Next(0, _regions.GetLength(1))];
    }

    public bool TryGetRegion(Vector2 id, out MapRegion region)
    {
        if ((id.X > _regions.GetLength(0) - 1 || id.X < 0) || 
            (id.Y > _regions.GetLength(1) - 1 || id.Y < 0))
        {
            region = null;
            return false;
        }

        region = GetRegion(id);
        return true;
    }

    public MapRegion GetRegion(Vector2 id)
    {
        return _regions[(int)id.X, (int)id.Y];
    }
}