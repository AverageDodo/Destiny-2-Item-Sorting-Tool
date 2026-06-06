using ItemSortingTool.Models;

namespace ItemSortingTool.Services;

public sealed class ItemRepository
{
    private ICollection<DestinyItem> _initialImportedCollection = [];

    public void SetImportedCollection(ICollection<DestinyItem> items) => _initialImportedCollection = items;

    public IEnumerable<DestinyItem> GetCopyOfImport() => _initialImportedCollection;
}