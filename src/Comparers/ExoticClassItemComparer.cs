using ItemSortingTool.Models;

namespace ItemSortingTool.Comparers;

public static class ExoticClassItemComparer
{
    public static bool AreEqual(DestinyItem a, DestinyItem b)
    {
        if (!a.IsExoticClassItem || !b.IsExoticClassItem)
            throw new ArgumentException("Both items must be exotic class items.");

        ICollection<string> firstExoticPerks = a.GetExoticClassItemPerks();
        ICollection<string> secondExoticPerks = b.GetExoticClassItemPerks();

        return firstExoticPerks.All(secondExoticPerks.Contains);
    }
}