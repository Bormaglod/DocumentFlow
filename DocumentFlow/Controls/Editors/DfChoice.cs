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
using DocumentFlow.Infrastructure.Controls.Core;
using DocumentFlow.Infrastructure.Data;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Editors;

public partial class DfChoice<T> : DataSourceControl<T, IChoice<T>>, IBindingControl, IAccess, IChoiceControl<T>
    where T : struct, IComparable
{
    private readonly List<IChoice<T>> choices = new();
    private bool required = false;
    private bool lockManual = false;
    private ControlValueChanged<T?>? choiceChanged;
    private ControlValueChanged<T?>? choiceSelected;

    public DfChoice(string property, string header, int headerWidth = default, int editorWidth = default) : base(property)
    {
        InitializeComponent();
        SetLabelControl(label1, header, headerWidth);
        SetNestedControl(panelEdit, editorWidth);
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

    public T? SelectedValue { get => (T?)Value; set => Value = value; }

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
                if (required)
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

            ClearSelectedValue();
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

    public void ClearSelectedValue() => SetSelectedItem(null);

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
        choiceChanged?.Invoke(value);
        if (!lockManual)
        {
            choiceSelected?.Invoke(value);
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

    private void ButtonDelete_Click(object sender, EventArgs e) => ClearSelectedValue();

    #region IChoiceControl interface

    IChoiceControl<T> IChoiceControl<T>.ReadOnly()
    {
        ReadOnly = true;
        return this;
    }

    IChoiceControl<T> IChoiceControl<T>.Required()
    {
        required = true;
        panelSeparator1.Visible = false;
        buttonDelete.Visible = false;
        return this;
    }

    IChoiceControl<T> IChoiceControl<T>.SetChoiceValues(IReadOnlyDictionary<T, string> keyValues, bool autoRefresh)
    {
        SetChoiceValues(keyValues, autoRefresh);
        return this;
    }

    IChoiceControl<T> IChoiceControl<T>.SetDataSource(GettingDataSource<IChoice<T>> func, DataRefreshMethod refreshMethod)
    {
        RefreshMethod = refreshMethod;
        SetDataSource(func);
        return this;
    }

    IChoiceControl<T> IChoiceControl<T>.ChoiceChanged(ControlValueChanged<T?> action)
    {
        choiceChanged = action;
        return this;
    }

    IChoiceControl<T> IChoiceControl<T>.ChoiceSelected(ControlValueChanged<T?> action)
    {
        choiceSelected = action;
        return this;
    }

    #endregion
}
