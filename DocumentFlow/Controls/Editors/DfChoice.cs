//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Editors;

public partial class DfChoice<T> : DataSourceControl<T, IChoice<T>>, IBindingControl, IAccess
    where T : struct, IComparable
{
    private readonly List<IChoice<T>> choices = new();
    private bool requird = false;
    private bool lockManual = false;

    public DfChoice(string property, string header, int headerWidth, int editorWidth = default) : base(property)
    {
        InitializeComponent();

        Header = header;
        HeaderWidth = headerWidth;
        if (editorWidth == default)
        {
            EditorFitToSize = true;
        }
        else
        {
            EditorWidth = editorWidth;
        }
    }

    public event EventHandler<SelectedValueChanged<T?>>? ValueChanged;
    public event EventHandler<SelectedValueChanged<T?>>? ManualValueChange;

    public string Header { get => label1.Text; set => label1.Text = value; }

    public int HeaderWidth { get => label1.Width; set => label1.Width = value; }

    public bool HeaderAutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

    public ContentAlignment HeaderTextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

    public bool HeaderVisible { get => label1.Visible; set => label1.Visible = value; }

    public int EditorWidth { get => panelEdit.Width; set => panelEdit.Width = value; }

    public bool EditorFitToSize
    {
        get => panelEdit.Dock == DockStyle.Fill;
        set => panelEdit.Dock = value ? DockStyle.Fill : panelEdit.Dock = DockStyle.Left;
    }

    public bool Required
    {
        get => requird;

        set
        {
            requird = value;
            panelSeparator1.Visible = !value;
            buttonDelete.Visible = !value;
        }
    }

    public bool ReadOnly 
    {
        get => comboBoxAdv1.ReadOnly;
        set
        {
            comboBoxAdv1.ReadOnly = value;
            buttonDelete.Enabled = !value;
        }
    }

    public T? ChoiceValue { get => (T?)Value; set => Value = value; }

    #region IBindingControl interface

    public object? Value 
    { 
        get
        {
            if (comboBoxAdv1.SelectedItem != null && comboBoxAdv1.SelectedItem is IChoice<T> selectedItem)
            {
                return selectedItem.id;
            }

            if (DefaultAsNull)
            {
                if (Required)
                {
                    throw new ArgumentException($"Значение поля [{Header}] должно быть иметь значение.");
                }

                return null;
            }

            return default;
        }

        set
        {
            if (value is T id)
            {
                SetSelectedItem(id);
                return;
            }

            ClearValue();
        }
    }

    #endregion

    public void SetChoiceValues(IDictionary<T, string> keyValues, bool autoRefresh = false)
    {
        choices.Clear();
        foreach (var item in keyValues)
        {
            choices.Add(new Choice<T>(item.Key, item.Value));
        }

        SetDataSource(GetChoiceItems);

        if (autoRefresh)
        {
            RefreshDataSource();
        }
    }

    public void ClearValue() => SetSelectedItem(null);

    protected override void DoRefreshDataSource(IEnumerable<IChoice<T>> data)
    {
        foreach (var item in data)
        {
            comboBoxAdv1.Items.Add(item);
        }
    }

    protected override void ClearItems()
    {
        comboBoxAdv1.Items.Clear();
    }

    private void SetSelectedItem(T? item)
    {
        lockManual = true;
        try
        {
            if (item == null)
            {
                comboBoxAdv1.SelectedItem = null;
            }
            else
            {
                var selected = comboBoxAdv1.Items.OfType<IChoice<T>>().FirstOrDefault(x => x.id.CompareTo(item) == 0);
                comboBoxAdv1.SelectedItem = selected;
            }
        }
        finally
        {
            lockManual = false;
        }
    }

    private IEnumerable<IChoice<T>> GetChoiceItems() => choices;

    private void OnValueChanged(T? value)
    {
        ValueChanged?.Invoke(this, new SelectedValueChanged<T?>(value));
        if (!lockManual)
        {
            ManualValueChange?.Invoke(this, new SelectedValueChanged<T?>(value));
        }
    }

    private void ComboBoxAdv1_SelectedIndexChanging(object sender, SelectedIndexChangingArgs e)
    {
        if (e.NewIndex == e.PrevIndex)
        {
            return;
        }

        if (e.NewIndex == -1)
        {
            OnValueChanged(null);
        }
        else
        {
            if (comboBoxAdv1.Items[e.NewIndex] is IChoice<T> value)
            {
                OnValueChanged(value.id);
            }
        }
    }

    private void ButtonDelete_Click(object sender, EventArgs e) => ClearValue();
}
