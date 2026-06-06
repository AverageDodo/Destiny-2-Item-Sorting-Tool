using System.Reflection;

using ItemSortingTool.Models;

using Microsoft.Extensions.Logging;

namespace ItemSortingTool.Services;

public class CsvReaderService(ILogger<CsvReaderService> logger)
{
    private string CsvHeaderRow { get; set; } = string.Empty;
    private string[] CsvHeaders => CsvHeaderRow.Split(",");

    public async IAsyncEnumerator<DestinyItem> ImportFromCsvAsync(FileInfo csvFile, CancellationToken token = default)
    {
        if (!csvFile.Exists || token.IsCancellationRequested)
            yield break;

        logger.LogInformation("Starting import from file '{Name}'.", csvFile.Name);

        IAsyncEnumerator<string> lineEnumerator;
        try
        {
            lineEnumerator = File.ReadLinesAsync(csvFile.FullName, token).GetAsyncEnumerator(token);
        }
        catch (Exception e)
        {
            logger.LogError(e, "");
            yield break;
        }

        await lineEnumerator.MoveNextAsync();
        CsvHeaderRow = lineEnumerator.Current;

        while (await lineEnumerator.MoveNextAsync())
        {
            if (token.IsCancellationRequested) break;

            string line = lineEnumerator.Current;
            yield return ParseItemFromLine(line);
        }
    }

    private DestinyItem ParseItemFromLine(string csvLine)
    {
        string[] sections = csvLine.Split(",");
        var values = CsvHeaders.Zip(sections)
            .Select((tuple, i) => new { Ordinal = i, Name = tuple.First, Value = tuple.Second })
            .ToArray();

        var destinyItem = new DestinyItem();

        destinyItem.GetType()
            .GetProperties()
            .Select(p => new
                {
                    Property = p,
                    Column = p.GetCustomAttribute<CsvColumnAttribute>()
                }
            )
            .Where(x => x.Column is not null)
            .AsParallel()
            .ForAll(obj =>
            {
                PropertyInfo prop = obj.Property;
                CsvColumnAttribute attr = obj.Column!;

                string value = values.FirstOrDefault(v => v.Ordinal == attr.Order)?.Value ?? string.Empty;
                try
                {
                    ApplyPropertyValue(destinyItem, prop, attr, value);
                }
                catch (Exception e)
                {
                    logger.LogError(
                        e,
                        "Exception caught attempting to populate property '{PropName}' with value '{Value}'.",
                        prop.Name,
                        value
                    );
                }
            });

        return destinyItem;
    }

    private static void ApplyPropertyValue(DestinyItem item, PropertyInfo property, CsvColumnAttribute attribute, string value)
    {
        Type propType = property.PropertyType;

        if (string.IsNullOrEmpty(value))
        {
            property.SetValue(item, default);

            return;
        }

        if (attribute.Name.Contains("perks", StringComparison.InvariantCultureIgnoreCase))
        {
            var perk = new ItemPerk(attribute.Order, value);
            item.Perks.Add(perk);

            return;
        }

        if (propType == typeof(ArmorArchetype))
        {
            ArmorArchetype statValue = Enum.TryParse(value, true, out ArmorArchetype stat) ? stat : ArmorArchetype.None;
            property.SetValue(item, statValue);

            return;
        }

        if (propType == typeof(Rarity))
        {
            Rarity rarityValue = Enum.TryParse(value, true, out Rarity rarity) ? rarity : Rarity.None;
            property.SetValue(item, rarityValue);

            return;
        }

        if (propType == typeof(Stat))
        {
            Stat statValue = Enum.TryParse(value, true, out Stat stat) ? stat : Stat.None;
            property.SetValue(item, statValue);

            return;
        }

        if (propType == typeof(Class))
        {
            Class statValue = Enum.TryParse(value, true, out Class stat) ? stat : Class.None;
            property.SetValue(item, statValue);

            return;
        }

        if (propType == typeof(int))
        {
            int intValue = int.TryParse(value, out int result) ? result : 0;
            property.SetValue(item, intValue);

            return;
        }

        if (propType == typeof(uint))
        {
            uint hashValue = uint.TryParse(value, out uint result) ? result : 0;
            property.SetValue(item, hashValue);

            return;
        }

        if (propType == typeof(bool))
        {
            bool boolValue = bool.TryParse(value, out bool val) && val;
            property.SetValue(item, boolValue);

            return;
        }

        if (propType == typeof(string))
        {
            property.SetValue(item, value);

            return;
        }

        throw new Exception(
            $"Value '{value}' could not be matched to property '{property.Name}' with type '{propType}'."
        );
    }
}