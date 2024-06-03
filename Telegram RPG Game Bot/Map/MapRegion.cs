using System.Numerics;
using Newtonsoft.Json;
using Telegram_RPG_Game_Bot.Core;
using Telegram_RPG_Game_Bot.Managers;

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

    public async void ShowRegion(CharacterOnMap characterOnMap)
    {
        var sectors = "";
        
        for (var i = 0; i < _sectors.GetLength(0); i++)
        {
            for (var j = 0; j < _sectors.GetLength(1); j++)
            {
                var sector = _sectors[i, j];
                
                if (sector.Id == characterOnMap.SectorId)
                {
                    if (CharacterManager.TryGetCharacter(characterOnMap.CharacterId, out var character))
                    {
                        sectors += character.MapIcon;
                        continue;
                    }
                }
                
                sectors += sector.Icon;
            }

            sectors += "\n";
        }

        var region =
            "==== *КАРТА РЕГИОНА* ====" +
            $"\n========== *{Id.X + 1}, {Id.Y + 1}* ==========" +
            $"\n{sectors}";
        
        await Bot.SendTextMessageAsync(region);
    }
    
    private void GenerateSectors()
    {
        for (var i = 0; i < _sectors.GetLength(0); i++)
        {
            for (var j = 0; j < _sectors.GetLength(1); j++)
            {
                _sectors[i, j] = new MapSector(new Vector2(i, j),"🌳");
            }
        }
    }
}