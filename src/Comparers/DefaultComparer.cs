using ItemSortingTool.Models;

namespace ItemSortingTool.Comparers;

public class DefaultComparer : IEqualityComparer<DestinyItem>
{
    public bool Equals(DestinyItem? x, DestinyItem? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null) return false;
        if (y is null) return false;
        if (x.GetType() != y.GetType()) return false;

        if (x.GetArmorVersion() != y.GetArmorVersion())
            return false;

        bool isEqual = string.Equals(x.Name, y.Name, StringComparison.InvariantCultureIgnoreCase)
                       && x.Hash == y.Hash
                       && x.Archetype == y.Archetype
                       && x.TertiaryStat == y.TertiaryStat
                       && x.WeaponsBase == y.WeaponsBase
                       && x.HealthBase == y.HealthBase
                       && x.ClassBase == y.ClassBase
                       && x.GrenadeBase == y.GrenadeBase
                       && x.SuperBase == y.SuperBase
                       && x.MeleeBase == y.MeleeBase
                       && x.TotalBase == y.TotalBase
                       && x.TuningStat == y.TuningStat;

        return x.IsExoticClassItem && y.IsExoticClassItem
            ? ExoticClassItemComparer.AreEqual(x, y) && isEqual
            : isEqual;
    }

    public int GetHashCode(DestinyItem obj)
    {
        var hashCode = new HashCode();

        hashCode.Add(obj.Name, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Hash);
        hashCode.Add(obj.Archetype);
        hashCode.Add((int)obj.TertiaryStat);
        hashCode.Add((int)obj.TuningStat);
        hashCode.Add(obj.WeaponsBase);
        hashCode.Add(obj.HealthBase);
        hashCode.Add(obj.ClassBase);
        hashCode.Add(obj.GrenadeBase);
        hashCode.Add(obj.SuperBase);
        hashCode.Add(obj.MeleeBase);
        hashCode.Add(obj.TotalBase);

        return hashCode.ToHashCode();
    }
}