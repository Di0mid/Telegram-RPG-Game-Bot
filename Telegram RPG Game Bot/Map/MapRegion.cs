using System.Numerics;
using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Core;
using Telegram_RPG_Game_Bot.Database;
using Telegram_RPG_Game_Bot.Map.Sector;

namespace Telegram_RPG_Game_Bot.Map;

[JsonObject(MemberSerialization.OptIn)]
public class MapRegion
{
    [JsonConstructor]
    private MapRegion() {}
    
    public MapRegion(Vector2 id)
    {
        Id = id;
        _sectors = new MapSector[9, 9];
        GenerateSectors();
    }

    [JsonProperty]
    public Vector2 Id { get; private set; }
    
    [JsonProperty]
    private MapSector[,] _sectors;

    public MapSector GetRandomSector()
    {
        var random = new Random();
        return _sectors[random.Next(0, _sectors.GetLength(0)), random.Next(0, _sectors.GetLength(1))];
    }

    public bool TryGetSector(Vector2 id, out MapSector sector)
    {
        if ((id.X > _sectors.GetLength(0) - 1 || id.X < 0) || 
            (id.Y > _sectors.GetLength(1) - 1 || id.Y < 0))
        {
            sector = null;
            return false;
        }

        sector = GetSector(id);
        return true;
    }

    public MapSector GetSector(Vector2 id)
    {
        return _sectors[(int)id.X, (int)id.Y];
    }

    public MapSector[,] GetAllSector()
    {
        return _sectors;
    }
    
    private void GenerateSectors()
    {
        for (var y = 0; y < _sectors.GetLength(0); y++)
        {
            for (var x = 0; x < _sectors.GetLength(1); x++)
            {
                //TEST
                var random = new Random();
                if (random.Next(1, 10) <= 5)
                {
                    _sectors[y, x] = new MapSector(new Vector2(y, x), MapSectorDatabase.Forest);
                }
                else
                {
                    _sectors[y, x] = new MapSector(new Vector2(y, x), MapSectorDatabase.Plain);
                }
                //TEST
                
                
                
                //_sectors[y, x] = new MapSector(new Vector2(y, x), MapSectorDatabase.Forest);
            }
        }
    }
}