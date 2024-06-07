using System.Numerics;
using Telegram_RPG_Game_Bot.Map;

namespace Telegram_RPG_Game_Bot.Managers;

public static class FogOfWarManager
{
    public static void DiscoverSectorsOnWay(GlobalMap map, Vector2 direction, Vector2 startSectorId,
        Vector2 endSectorId,
        CharacterOnMapData data)
    {
        var region = map.GetRegion(data.RegionId);

        var currentSectorId = startSectorId;
        while (currentSectorId != endSectorId)
        {
            currentSectorId += direction;
            DiscoverSectorsAround(currentSectorId, region);
        }
    }

    public static void DiscoverSectorsAroundCharacter(GlobalMap map, CharacterOnMapData data)
    {
        var currentRegion = map.GetRegion(data.RegionId);
        var currentSector = currentRegion.GetSector(data.SectorId);

        DiscoverSectorsAround(currentSector.Id, currentRegion);
    }

    private static void DiscoverSectorsAround(Vector2 centerSectorId, MapRegion region)
    {
        for (var y = -1; y <= 1; y++)
        {
            for (var x = -1; x <= 1; x++)
            {
                var id = centerSectorId + new Vector2(y, x);
                if (!region.TryGetSector(id, out var sector)) 
                    continue;
                
                if (sector.IsDiscovered)
                    continue;

                sector.Discover();
            }
        }
    }
}