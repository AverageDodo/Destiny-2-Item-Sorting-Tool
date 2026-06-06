namespace ItemSortingTool.Models;

public record DuplicatesTableEntry(
    ICollection<DestinyItem> Duplicates
)
{
    public string ItemName => Duplicates.First().Name;
    public int Count => Duplicates.Count;
    public string DimQuery => GetDimQuery();

    private string GetDimQuery()
    {
        var dimQuery = string.Empty;

        try
        {
            dimQuery =
                Duplicates
                    .Select(item => item.Id.Replace("\"", string.Empty))
                    .Select(idString => $"id:{idString}")
                    .Aggregate((acc, cur) => $"{acc} or {cur}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return dimQuery;
    }
}