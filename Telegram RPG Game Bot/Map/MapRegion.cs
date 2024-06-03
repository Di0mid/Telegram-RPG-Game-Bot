using System.Numerics;
using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Core;

namespace Telegram_RPG_Game_Bot.Map;

public class MapRegion
{
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
        if (id.X > _sectors.GetLength(0) || id.Y > _sectors.GetLength(1))
        {
            sector = null;
            return false;
        }

        sector = _sectors[(int)id.X, (int)id.Y];
        return true;
    }
    
    public async void ShowRegion(CharacterOnMapData characterOnMapData)
    {
        var sectors = "";
        
        for (var i = 0; i < _sectors.GetLength(0); i++)
        {
            for (var j = 0; j < _sectors.GetLength(1); j++)
            {
                var sector = _sectors[i, j];
                
                if (sector.Id == characterOnMapData.SectorId)
                {
                    sectors += characterOnMapData.Character.MapIcon;
                    continue;
                }
                
                sectors += sector.Icon;
            }

            sectors += "\n";
        }

        var region =
            $"== *КАРТА РЕГИОНА ({Id.X + 1}, {Id.Y + 1})* ==" +
            $"\n========= *{characterOnMapData.Character.Name}* =========" +
            $"\n" +
            $"\n{sectors}";
        
        await Bot.SendTextMessageAsync(region);
    }
    
    private void GenerateSectors()
    {
        for (var i = 0; i < _sectors.GetLength(0); i++)
        {
            for (var j = 0; j < _sectors.GetLength(1); j++)
            {
                _sectors[i, j] = new MapSector(new Vector2(j, i),"🌳");
            }
        }
    }
}