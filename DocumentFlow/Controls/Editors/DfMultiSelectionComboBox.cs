//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Data.Infrastructure;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Editors;

public partial class DfMultiSelectionComboBox : BaseControl, IBindingControl, IDataSourceControl, IAccess
{
    private List<IItem> items = new();
    private Func<IEnumerable<IItem>?>? dataSource;

    public DfMultiSelectionComboBox(string property, string header, int headerWidth, int editorWidth) : base(property)
    {
        InitializeComponent();

        Header = header;
        HeaderWidth = headerWidth;
        EditorWidth = editorWidth;
        
        multiSelectionComboBox1.Style = MultiSelectionComboBoxStyle.Office2016Colorful;
        multiSelectionComboBox1.DisplayMember = "item_name";
        multiSelectionComboBox1.ValueMember = "code";
        multiSelectionComboBox1.VisualItemInputMode = VisualItemInputMode.ValueMemberMode;
    }

    public event EventHandler? ValueChanged;

    public string Header { get => label1.Text; set => label1.Text = value; }

    public int HeaderWidth { get => label1.Width; set => label1.Width = value; }

    public bool HeaderAutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

    public ContentAlignment HeaderTextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

    public bool HeaderVisible { get => label1.Visible; set => label1.Visible = value; }

    public int EditorWidth { get => multiSelectionComboBox1.Width; set => multiSelectionComboBox1.Width = value; }

    public bool ReadOnly
    {
        get => !multiSelectionComboBox1.Enabled;
        set => multiSelectionComboBox1.Enabled = !value;
    }

    public object? Value
    {
        get
        {
            if (DefaultAsNull && multiSelectionComboBox1.VisualItems.Count == 0)
            {
                return null;
            }

            return multiSelectionComboBox1.VisualItems.OfType<VisualItem>().Select(x => x.Text).ToArray();
        }

        set
        {
            if (value == null)
            {
                ClearValue();
            }
            else
            {
                if (multiSelectionComboBox1.DataSource is IList<IItem> list && value is IEnumerable<string> values)
                {
                    list
                        .Where(x => values.Contains(x.code))
                        .ToList()
                        .ForEach(x => multiSelectionComboBox1.AddVisualItem(x.code));
                }
            }
        }
    }

    public bool EditorFitToSize
    {
        get => multiSelectionComboBox1.Dock == DockStyle.Fill;
        set => multiSelectionComboBox1.Dock = value ? DockStyle.Fill : multiSelectionComboBox1.Dock = DockStyle.Left;
    }

    public void SetDataSource(Func<IEnumerable<IItem>?> func) => dataSource = func;

    public void RefreshDataSource()
    {
        items.Clear();
        if (dataSource != null)
        {
            var ds = dataSource();
            if (ds != null)
            {
                items.AddRange(ds);
                multiSelectionComboBox1.DataSource = items;
            }
        }
    }

    public void ClearValue() => multiSelectionComboBox1.VisualItems.Clear();

    private void MultiSelectionComboBox1_SelectedItemCollectionChanged(object sender, SelectedItemCollectionChangedArgs e) => ValueChanged?.Invoke(this, e);
}
