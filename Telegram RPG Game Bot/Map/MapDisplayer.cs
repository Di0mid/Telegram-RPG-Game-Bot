using Telegram_RPG_Game_Bot.Characters;
using Telegram_RPG_Game_Bot.Core;
using Telegram.Bot.Types;

namespace Telegram_RPG_Game_Bot.Map;

public static class MapDisplayer
{
    private static Message? _lastMapShowMessage;
    
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
        
        _lastMapShowMessage = await Bot.SendTextMessageAsync(regionInfo);
        UpdatingLastShowMapMessage(0);
    }

    public static async void ShowMapSectorInfo(GlobalMap map, CharacterOnMapData data)
    {
        var region = map.GetRegion(data.RegionId);
        var sector = region.GetSector(data.SectorId);

        await Bot.SendTextMessageAsync(sector.Info());
    }

    private static async void UpdatingLastShowMapMessage(int updatedCount)
    {
        if(_lastMapShowMessage == null)
            return;

        await Task.Delay(TimeSpan.FromSeconds(1));
        
        await Bot.EditMessageTextAsync(_lastMapShowMessage.MessageId, updatedCount.ToString());
        
        if(updatedCount == 100)
            return;
        
        UpdatingLastShowMapMessage(updatedCount + 1);
    } 
}