//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.11.2021
//
// Версия 2022.11.26
//  - добавлен метод RefreshDataSourceOnLoad
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Editors;

public partial class DfMultiSelectionComboBox : BaseControl, IBindingControl, IDataSourceControl, IAccess
{
    private readonly List<IItem> items = new();
    private Func<IEnumerable<IItem>?>? dataSource;

    public DfMultiSelectionComboBox(string property, string header, int headerWidth = default, int editorWidth = default) 
        : base(property)
    {
        InitializeComponent();
        SetLabelControl(label1, header, headerWidth);
        SetNestedControl(multiSelectionComboBox1, editorWidth);

        multiSelectionComboBox1.Style = MultiSelectionComboBoxStyle.Office2016Colorful;
        multiSelectionComboBox1.DisplayMember = "ItemName";
        multiSelectionComboBox1.ValueMember = "Code";
        multiSelectionComboBox1.VisualItemInputMode = VisualItemInputMode.ValueMemberMode;
    }

    public event EventHandler? ValueChanged;

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
                        .Where(x => values.Contains(x.Code))
                        .ToList()
                        .ForEach(x => multiSelectionComboBox1.AddVisualItem(x.Code));
                }
            }
        }
    }

    public void SetDataSource(Func<IEnumerable<IItem>?> func) => dataSource = func;

    #region IDataSourceControl interface

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

    public void RefreshDataSourceOnLoad() => RefreshDataSource();

    #endregion

    public void ClearValue() => multiSelectionComboBox1.VisualItems.Clear();

    private void MultiSelectionComboBox1_SelectedItemCollectionChanged(object sender, SelectedItemCollectionChangedArgs e) => ValueChanged?.Invoke(this, e);
}
