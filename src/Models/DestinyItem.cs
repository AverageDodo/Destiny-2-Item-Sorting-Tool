namespace ItemSortingTool.Models;

public sealed class DestinyItem
{
    [CsvColumn("Name", 0)]
    public string Name { get; set; } = string.Empty;

    [CsvColumn("Hash", 1)]
    public uint Hash { get; set; }

    /// <summary>
    ///     Kept as string because the CSV value may be quoted and can exceed safe numeric handling needs.
    ///     Example: """6917530156775205664"""
    /// </summary>
    [CsvColumn("Id", 2)]
    public string Id { get; set; } = string.Empty;

    [CsvColumn("Tag", 3)]
    public string? Tag { get; set; }

    [CsvColumn("Rarity", 4)]
    public Rarity Rarity { get; set; }

    [CsvColumn("Tier", 5)]
    public int Tier { get; set; }

    [CsvColumn("Type", 6)]
    public string Type { get; set; } = string.Empty;

    [CsvColumn("Source", 7)]
    public string? Source { get; set; }

    [CsvColumn("Equippable", 8)]
    public Class Equippable { get; set; }

    [CsvColumn("Power", 9)]
    public int Power { get; set; }

    [CsvColumn("Energy Capacity", 10)]
    public int EnergyCapacity { get; set; }

    [CsvColumn("Archetype", 11)]
    public ArmorArchetype Archetype { get; set; }

    [CsvColumn("Tertiary Stat", 12)]
    public Stat TertiaryStat { get; set; }

    [CsvColumn("Tuning Stat", 13)]
    public Stat TuningStat { get; set; }

    [CsvColumn("Masterwork Tier", 14)]
    public int MasterworkTier { get; set; }

    [CsvColumn("Owner", 15)]
    public string Owner { get; set; } = string.Empty;

    [CsvColumn("Locked", 16)]
    public bool Locked { get; set; }

    [CsvColumn("Equipped", 17)]
    public bool Equipped { get; set; }

    [CsvColumn("New Gear", 18)]
    public bool NewGear { get; set; }

    [CsvColumn("Holofoil", 19)]
    public bool Holofoil { get; set; }

    [CsvColumn("Year", 20)]
    public int Year { get; set; }

    [CsvColumn("Season", 21)]
    public int Season { get; set; }

    [CsvColumn("Event", 22)]
    public string? Event { get; set; }

    [CsvColumn("Weapons", 23)]
    public int Weapons { get; set; }

    [CsvColumn("Health", 24)]
    public int Health { get; set; }

    [CsvColumn("Class", 25)]
    public int Class { get; set; }

    [CsvColumn("Grenade", 26)]
    public int Grenade { get; set; }

    [CsvColumn("Super", 27)]
    public int Super { get; set; }

    [CsvColumn("Melee", 28)]
    public int Melee { get; set; }

    [CsvColumn("Total", 29)]
    public int Total { get; set; }

    [CsvColumn("Weapons (Base)", 30)]
    public int WeaponsBase { get; set; }

    [CsvColumn("Health (Base)", 31)]
    public int HealthBase { get; set; }

    [CsvColumn("Class (Base)", 32)]
    public int ClassBase { get; set; }

    [CsvColumn("Grenade (Base)", 33)]
    public int GrenadeBase { get; set; }

    [CsvColumn("Super (Base)", 34)]
    public int SuperBase { get; set; }

    [CsvColumn("Melee (Base)", 35)]
    public int MeleeBase { get; set; }

    [CsvColumn("Total (Base)", 36)]
    public int TotalBase { get; set; }

    [CsvColumn("Seasonal Mod", 37)]
    public string? SeasonalMod { get; set; }

    [CsvColumn("Loadouts", 38)]
    public string? Loadouts { get; set; }

    [CsvColumn("Notes", 39)]
    public string? Notes { get; set; }

    [CsvColumn("Perks 0", 40)]
    public string? Perks0 { get; set; }

    [CsvColumn("Perks 1", 41)]
    public string? Perks1 { get; set; }

    [CsvColumn("Perks 2", 42)]
    public string? Perks2 { get; set; }

    [CsvColumn("Perks 3", 43)]
    public string? Perks3 { get; set; }

    [CsvColumn("Perks 4", 44)]
    public string? Perks4 { get; set; }

    [CsvColumn("Perks 5", 45)]
    public string? Perks5 { get; set; }

    [CsvColumn("Perks 6", 46)]
    public string? Perks6 { get; set; }

    [CsvColumn("Perks 7", 47)]
    public string? Perks7 { get; set; }

    [CsvColumn("Perks 8", 48)]
    public string? Perks8 { get; set; }

    [CsvColumn("Perks 9", 49)]
    public string? Perks9 { get; set; }

    [CsvColumn("Perks 10", 50)]
    public string? Perks10 { get; set; }

    [CsvColumn("Perks 11", 51)]
    public string? Perks11 { get; set; }

    [CsvColumn("Perks 12", 52)]
    public string? Perks12 { get; set; }

    public ICollection<ItemPerk> Perks { get; set; } = [];

    public bool IsExoticClassItem => Equippable switch
    {
        Models.Class.Warlock => Hash == Constants.c_WarlockExoticClassItemHash,
        Models.Class.Titan => Hash == Constants.c_TitanExoticClassItemHash,
        Models.Class.Hunter => Hash == Constants.c_HunterExoticClassItemHash,
        _ => false
    };

    public ICollection<string> GetExoticClassItemPerks()
    {
        string?[] perks = Perks.Select(p => p.Value).ToArray();

        return perks
            .Where(perk => !string.IsNullOrEmpty(perk) && perk.Contains(
                    "spirit",
                    StringComparison.InvariantCultureIgnoreCase
                )
            )
            .ToArray()!;
    }
}

public record ItemStat(Stat Stat, int Value);

public record ItemPerk(int Ordinal, string? Value);
