using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using ItemSortingTool.Comparers;
using ItemSortingTool.Models;
using ItemSortingTool.Services;

using Microsoft.Extensions.Logging;

namespace ItemSortingTool;

[SuppressMessage("Performance", "CA1873:Avoid potentially expensive logging")]
public partial class MainWindow : Form
{
    private Button _importCsvButton;

    private TextBox _selectedFileTextBox;
    private Label _selectedFileLabel;
    private GroupBox _statisticsGroupBox;
    private Label _linesImportedTitleLabel;
    private Label _linesImportedValueLabel;
    private Label _importStatusTitleLabel;
    private Label _importStatusValueLabel;
    private OpenFileDialog _csvOpenFileDialog;
    private GroupBox _resultGroupBox;
    private System.Windows.Forms.DataGridView _duplicatesDataGridView;
    private ComboBox _equalityComparerComboBox;

    private readonly ItemRepository _itemRepository;
    private readonly CsvReaderService _csvReaderService;
    private readonly ILogger<MainWindow> _logger;

    public MainWindow()
    {
        _logger = null!;
        _itemRepository = null!;
        _csvReaderService = null!;

        InitializeComponent();
        ConfigureDuplicatesTable();
    }

    public MainWindow(ItemRepository repository, ILogger<MainWindow> logger, CsvReaderService csvReaderService)
    {
        _logger = logger;
        _itemRepository = repository;
        _csvReaderService = csvReaderService;

        InitializeComponent();
        ConfigureDuplicatesTable();
        ConfigureEqualityComparerDropdown();
    }

    private void ConfigureDuplicatesTable()
    {
        _duplicatesDataGridView.AutoGenerateColumns = false;
        _duplicatesDataGridView.Columns.Clear();

        _duplicatesDataGridView.Columns.Add(
            new DataGridViewTextBoxColumn
            {
                HeaderText = "Item Name",
                DataPropertyName = nameof(DuplicatesTableEntry.ItemName),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            }
        );

        _duplicatesDataGridView.Columns.Add(
            new DataGridViewTextBoxColumn
            {
                HeaderText = "Count",
                DataPropertyName = nameof(DuplicatesTableEntry.Count),
                Width = 80
            }
        );

        _duplicatesDataGridView.Columns.Add(
            new DataGridViewButtonColumn
            {
                HeaderText = "DIM Query",
                Text = "Copy DIM Query",
                UseColumnTextForButtonValue = true,
                Width = 140
            }
        );

        _duplicatesDataGridView.CellContentClick += DuplicatesDataGridView_CellContentClick;
    }

    private IEqualityComparer<DestinyItem> GetSelectedEqualityComparer()
    {
        if (_equalityComparerComboBox.SelectedItem is EqualityComparerDropdownItem selectedItem)
            return selectedItem.Comparer;

        return EqualityComparer<DestinyItem>.Default;
    }

    private void ConfigureEqualityComparerDropdown()
    {
        EqualityComparerDropdownItem[] comparers = Assembly.GetExecutingAssembly()
            .DefinedTypes
            .Where(type => !type.IsAbstract)
            .Where(type => !type.IsInterface)
            .Where(type => type.ImplementedInterfaces.Contains(typeof(IEqualityComparer<DestinyItem>)))
            .Where(type => type.GetConstructor(Type.EmptyTypes) is not null)
            .Select(type => new EqualityComparerDropdownItem(
                    GetComparerDisplayName(type),
                    (IEqualityComparer<DestinyItem>)Activator.CreateInstance(type)!
                )
            )
            .OrderBy(item => item.Name)
            .ToArray();

        _logger.LogInformation("Found '{Count}' equality comparers for items in assembly.", comparers.Length);

        _equalityComparerComboBox.DataSource = comparers;
        _equalityComparerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

        int defaultIndex = Array.FindIndex(
            comparers,
            item => item.Comparer.GetType().Name == "DefaultComparer"
        );

        _equalityComparerComboBox.SelectedIndex = defaultIndex >= 0 ? defaultIndex : 0;
    }

    private static string GetComparerDisplayName(TypeInfo type)
    {
        return type.Name.EndsWith("Comparer", StringComparison.Ordinal)
            ? type.Name[..^"Comparer".Length]
            : type.Name;
    }

    private void DuplicatesDataGridView_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        if (_duplicatesDataGridView.Columns[e.ColumnIndex] is not DataGridViewButtonColumn)
            return;

        if (_duplicatesDataGridView.Rows[e.RowIndex].DataBoundItem is not DuplicatesTableEntry entry)
            return;

        Clipboard.SetText(entry.DimQuery);

        _importStatusValueLabel.Text = $"Copied DIM query for {entry.ItemName}";
    }

    private async void ImportCsvButton_Click(object? sender, EventArgs e)
    {
        if (_csvOpenFileDialog.ShowDialog(this) != DialogResult.OK) return;

        _selectedFileTextBox.Text = _csvOpenFileDialog.FileName;
        _importStatusValueLabel.Text = "File selected";

        IAsyncEnumerator<DestinyItem> result = _csvReaderService.ImportFromCsvAsync(
            new FileInfo(_csvOpenFileDialog.FileName),
            CancellationToken.None
        );

        var itemList = new List<DestinyItem>();
        while (await result.MoveNextAsync())
            itemList.Add(result.Current);

        int warlockCount = itemList.Count(item => item.Equippable == Class.Warlock);
        int hunterCount = itemList.Count(item => item.Equippable == Class.Hunter);
        int titanCount = itemList.Count(item => item.Equippable == Class.Titan);

        _logger.LogInformation(
            "Warlock count: {WarlockCount}; Hunter Count: {HunterCount}; Titan Count: {TitanCount}",
            warlockCount,
            hunterCount,
            titanCount
        );

        IEqualityComparer<DestinyItem> selectedComparer = GetSelectedEqualityComparer();

        DuplicatesTableEntry[] duplicateEntries = itemList
            .GroupBy(item => item, selectedComparer)
            .Where(grouping => grouping.Count() > 1)
            .Select(grouping => new DuplicatesTableEntry(grouping.ToArray()))
            .OrderByDescending(entry => entry.Count)
            .ThenBy(entry => entry.ItemName)
            .ToArray();

        foreach (DuplicatesTableEntry duplicateEntry in duplicateEntries)
        {
            _logger.LogInformation(
                "'{ItemName}': {Count} duplicate item instances detected. DIM query: '{DimQuery}'.",
                duplicateEntry.ItemName,
                duplicateEntry.Count,
                duplicateEntry.DimQuery
            );
        }

        _duplicatesDataGridView.DataSource = duplicateEntries;
        _resultGroupBox.Visible = duplicateEntries.Length > 0;

        _linesImportedValueLabel.Text = $"{itemList.Count}";
    }

    private sealed record EqualityComparerDropdownItem(
        string Name,
        IEqualityComparer<DestinyItem> Comparer
    )
    {
        public override string ToString() => Name;
    }
}