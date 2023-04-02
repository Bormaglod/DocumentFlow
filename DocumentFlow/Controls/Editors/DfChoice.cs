//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
//
// Версия 2023.1.21
//  - в методе SetChoiceValues у параметра keyValues заменен тип с
//    IDictionary на IReadOnlyDictionary
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.4.2
//  - добавлено наследование от IChoiceControl
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Editors;

public partial class DfChoice<T> : DataSourceControl<T, IChoice<T>>, IBindingControl, IAccess, IChoiceControl<T>
    where T : struct, IComparable
{
    private readonly List<IChoice<T>> choices = new();
    private bool requird = false;
    private bool lockManual = false;
    private Action<T?>? manualValueChanged;

    public DfChoice(string property, string header, int headerWidth = 100, int editorWidth = default) : base(property)
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
                return selectedItem.Id;
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

    public void SetChoiceValues(IReadOnlyDictionary<T, string> keyValues, bool autoRefresh = false)
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

    protected override void ClearItems() => comboBoxAdv1.Items.Clear();

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
                var selected = comboBoxAdv1.Items.OfType<IChoice<T>>().FirstOrDefault(x => x.Id.CompareTo(item) == 0);
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
            manualValueChanged?.Invoke(value);
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
                OnValueChanged(value.Id);
            }
        }
    }

    private void ButtonDelete_Click(object sender, EventArgs e) => ClearValue();

    #region IControl interface

    string IControl.Tag => Tag?.ToString() ?? string.Empty;

    IControl IControl.SetHeaderWidth(int width)
    {
        HeaderWidth = width;
        return this;
    }

    IControl IControl.SetEditorWidth(int width)
    {
        EditorWidth = width;
        return this;
    }

    IControl IControl.Disable()
    {
        Enabled = false;
        return this;
    }

    IControl IControl.ReadOnly()
    {
        ReadOnly = true;
        return this;
    }

    IControl IControl.DefaultAsValue()
    {
        DefaultAsNull = false;
        return this;
    }

    IControl IControl.SetTag(string tag)
    {
        Tag = tag;
        return this;
    }

    IControl IControl.SetVisible(bool visible)
    {
        Visible = visible;
        return this;
    }

    #endregion

    #region IChoiceControl interface

    T? IChoiceControl<T>.Value => ChoiceValue;

    IChoiceControl<T> IChoiceControl<T>.Required()
    {
        Required = true;
        return this;
    }

    IChoiceControl<T> IChoiceControl<T>.SetChoiceValues(IReadOnlyDictionary<T, string> keyValues, bool autoRefresh)
    {
        SetChoiceValues(keyValues, autoRefresh); 
        return this;
    }

    IChoiceControl<T> IChoiceControl<T>.SetDataSource(Func<IEnumerable<IChoice<T>>?> func, DataRefreshMethod refreshMethod)
    {
        RefreshMethod = refreshMethod;
        SetDataSource(func);
        return this;
    }

    IChoiceControl<T> IChoiceControl<T>.ManualValueChanged(Action<T?> action)
    {
        manualValueChanged = action;
        return this;
    }

    #endregion
}
