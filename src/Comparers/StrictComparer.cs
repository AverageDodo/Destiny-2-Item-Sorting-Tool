using ItemSortingTool.Models;

namespace ItemSortingTool.Comparers;

public class StrictComparer : IEqualityComparer<DestinyItem>
{
    public bool Equals(DestinyItem? x, DestinyItem? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null) return false;
        if (y is null) return false;
        if (x.GetType() != y.GetType()) return false;

        return string.Equals(x.Name, y.Name, StringComparison.InvariantCultureIgnoreCase)
               && x.Hash == y.Hash
               && x.Rarity == y.Rarity
               && x.Tier == y.Tier
               && string.Equals(x.Type, y.Type, StringComparison.InvariantCultureIgnoreCase)
               && string.Equals(x.Source, y.Source, StringComparison.InvariantCultureIgnoreCase)
               && x.Equippable == y.Equippable
               && x.Archetype == y.Archetype
               && x.TertiaryStat == y.TertiaryStat
               && x.TuningStat == y.TuningStat
               && x.Holofoil == y.Holofoil
               && x.Year == y.Year
               && x.Season == y.Season
               && string.Equals(x.Event, y.Event, StringComparison.InvariantCultureIgnoreCase)
               && x.WeaponsBase == y.WeaponsBase
               && x.HealthBase == y.HealthBase
               && x.ClassBase == y.ClassBase
               && x.GrenadeBase == y.GrenadeBase
               && x.SuperBase == y.SuperBase
               && x.MeleeBase == y.MeleeBase
               && x.TotalBase == y.TotalBase
               && x.Perks.Zip(y.Perks).All(pair => pair.First == pair.Second);
    }

    public int GetHashCode(DestinyItem obj)
    {
        var hashCode = new HashCode();

        hashCode.Add(obj.Name, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Hash);
        hashCode.Add(obj.Rarity);
        hashCode.Add(obj.Tier);
        hashCode.Add(obj.Type, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add((int)obj.Equippable);
        hashCode.Add((int)obj.Archetype);
        hashCode.Add((int)obj.TertiaryStat);
        hashCode.Add((int)obj.TuningStat);
        hashCode.Add(obj.Holofoil);
        hashCode.Add(obj.Year);
        hashCode.Add(obj.Season);
        hashCode.Add(obj.Event, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.WeaponsBase);
        hashCode.Add(obj.HealthBase);
        hashCode.Add(obj.ClassBase);
        hashCode.Add(obj.GrenadeBase);
        hashCode.Add(obj.SuperBase);
        hashCode.Add(obj.MeleeBase);
        hashCode.Add(obj.TotalBase);
        hashCode.Add(obj.Perks0, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Perks1, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Perks2, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Perks3, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Perks4, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Perks5, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Perks6, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Perks7, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Perks8, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Perks9, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Perks10, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Perks11, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Perks12, StringComparer.InvariantCultureIgnoreCase);
        obj.Perks.AsParallel().ForAll(p => hashCode.Add(p));

        return hashCode.ToHashCode();
    }
}