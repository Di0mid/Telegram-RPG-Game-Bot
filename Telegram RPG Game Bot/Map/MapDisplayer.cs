using Telegram_RPG_Game_Bot.Characters;
using Telegram_RPG_Game_Bot.Core;

namespace Telegram_RPG_Game_Bot.Map;

public static class MapDisplayer
{
    public static async void ShowMapRegion(GlobalMap map, CharacterOnMapData data)
    {
        var regionId = data.RegionId;
        var region = map.GetRegion(regionId);
        
        var sectors = region.GetAllSector();
        var sectorsInfo = "";
        
        for (var y = 0; y < sectors.GetLength(0); y++)
        {
            for (var x = 0; x < sectors.GetLength(1); x++)
            {
                var sector = sectors[y, x];
                
                if (sector.Id == data.SectorId)
                {
                    sectorsInfo += data.CharacterMapIcon;
                    continue;
                }
                
                sectorsInfo += sector.Icon;
            }

            sectorsInfo += "\n";
        }

        var regionInfo =
            $"== *КАРТА РЕГИОНА ({regionId.X}, {regionId.Y})* ==" +
            $"\n========= *{data.CharacterName}* =========" +
            $"\n" +
            $"\n{sectorsInfo}";
        
        await Bot.SendTextMessageAsync(regionInfo);
    }

    public static async void ShowMapSectorInfo(GlobalMap map, CharacterOnMapData data)
    {
        var region = map.GetRegion(data.RegionId);
        var sector = region.GetSector(data.SectorId);

        await Bot.SendTextMessageAsync(sector.Info());
    }
}