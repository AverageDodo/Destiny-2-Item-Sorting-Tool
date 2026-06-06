namespace ItemSortingTool.Models;

[AttributeUsage(AttributeTargets.Property)]
public sealed class CsvColumnAttribute(string name, int order) : Attribute
{
    public string Name { get; } = name;
    public int Order { get; } = order;
}
