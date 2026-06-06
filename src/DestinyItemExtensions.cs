using ItemSortingTool.Models;

namespace ItemSortingTool;

public static class DestinyItemExtensions
{
    public static ArmorVersion GetArmorVersion(this DestinyItem item)
    {
        var zeroBaseStats = 0;

        if (item.HealthBase == 0) zeroBaseStats++;
        if (item.MeleeBase == 0) zeroBaseStats++;
        if (item.GrenadeBase == 0) zeroBaseStats++;
        if (item.SuperBase == 0) zeroBaseStats++;
        if (item.ClassBase == 0) zeroBaseStats++;
        if (item.WeaponsBase == 0) zeroBaseStats++;

        return zeroBaseStats == 3 ? ArmorVersion.V3 : ArmorVersion.V2;
    }
}