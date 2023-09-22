//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Interfaces;

using Syncfusion.Windows.Forms.Tools;

using System.ComponentModel;
using System.Data;

namespace DocumentFlow.Controls.Editors;

[ToolboxItem(true)]
public partial class DfMultiSelectionComboBox : DfControl, IAccess
{
    private List<string> selected = new();
    private bool enabledEditor = true;
    private bool lockAdd;

    public event EventHandler? SelectedItemsChanged;

    public DfMultiSelectionComboBox()
    {
        InitializeComponent();
        SetNestedControl(multiSelectionComboBox1);

        multiSelectionComboBox1.DisplayMember = "ItemName";
        multiSelectionComboBox1.ValueMember = "Code";
        multiSelectionComboBox1.VisualItemInputMode = VisualItemInputMode.ValueMemberMode;
    }

    public bool EnabledEditor
    {
        get => enabledEditor;
        set
        {
            if (enabledEditor != value) 
            {
                enabledEditor = value;
                multiSelectionComboBox1.Enabled = value;
            }
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string[] SelectedItems
    {
        get => selected.ToArray();
        set
        {
            if (!selected.SequenceEqual(value))
            {
                selected = new List<string>(value);

                lockAdd = true;
                try
                {
                    multiSelectionComboBox1.SelectedItems.Clear();
                    foreach (var item in DataSource.Where(x => selected.Contains(x.Code))) 
                    {
                        multiSelectionComboBox1.AddVisualItem(item.Code);

                        var vi = multiSelectionComboBox1.VisualItems.OfType<VisualItem>().FirstOrDefault(x => x.Text == item.Code);
                        if (vi != null)
                        {
                            vi.CloseButtonClicked += Visualitem_CloseButtonClicked;
                        }
                    }
                }
                finally
                {
                    lockAdd = false;
                }

                OnSelectedItemsChanged();
            }
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IEnumerable<IItem> DataSource
    {
        get => (IEnumerable<IItem>)multiSelectionComboBox1.DataSource;
        set => multiSelectionComboBox1.DataSource = value;
    }

    protected void OnSelectedItemsChanged() => SelectedItemsChanged?.Invoke(this, EventArgs.Empty);

    private void UpdateSelectedItems()
    {
        selected = new List<string>(multiSelectionComboBox1.VisualItems.OfType<VisualItem>().Select(x => x.Text));
        OnSelectedItemsChanged();
    }

    private void Visualitem_CloseButtonClicked(object? sender, EventArgs e) => UpdateSelectedItems();

    private void MultiSelectionComboBox1_VisualItemsCollectionChanged(object sender, VisualItemCollectionChangedArgs e)
    {
        if (lockAdd)
        {
            return;
        }

        UpdateSelectedItems();
    }
}
