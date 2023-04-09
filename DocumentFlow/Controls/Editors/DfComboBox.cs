//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
//
// Версия 2022.8.29
//  - добавлена кнопка для открытия нового окна
//  - добавлено свойство OpenAction
//  - свойство SelectedItem типа Guid? переименовано в SelectedValue
//  - добавлено свойство SelecredItem типа T?
//  - удалён метод SetSelectedItem
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.4.2
//  - добавлено наследование от IComboBoxControl
//  - тип универсального параметра T изменен на IDocumentInfo
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Editors;

public partial class DfComboBox<T> : DataSourceControl<Guid, T>, IBindingControl, IAccess, IComboBoxControl<T>
    where T : class, IDocumentInfo
{
    private bool requird = false;
    private bool lockManual = false;
    private Action<T>? open;
    private Action<T?>? valueChanged;
    private Action<T?>? valueSelected;

    public DfComboBox(string property, string header, int headerWidth = default, int editorWidth = default) : base(property)
    {
        InitializeComponent();
        SetLabelControl(label1, header, headerWidth);
        SetNestedControl(panelEdit, editorWidth);

        buttonOpen.Visible = false;
        panelSeparator3.Visible = false;
    }

    public event EventHandler<SelectedValueChanged<T>>? ValueChanged;
    public event EventHandler<SelectedValueChanged<T>>? ManualValueChange;

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

    public Guid? SelectedValue => (Guid?)Value;

    public T? Selected
    {
        get => comboBoxAdv1.SelectedItem as T;

        private set
        {
            lockManual = true;
            try
            {
                comboBoxAdv1.SelectedItem = value;
            }
            finally
            {
                lockManual = false;
            }
        }
    }

    public Action<T>? OpenAction
    {
        get => open;
        set
        {
            open = value;
            buttonOpen.Visible = open != null;
            panelSeparator3.Visible = open != null;
        }
    }

    #region IBindingControl interface

    public object? Value
    {
        get
        {
            if (comboBoxAdv1.SelectedItem != null && comboBoxAdv1.SelectedItem is T selectedItem)
            {
                return selectedItem.Id;
            }

            if (Required)
            {
                throw new ArgumentException($"Значение поля [{Header}] должно быть иметь значение.");
            }

            return null;
        }

        set
        {
            if (value is Guid id)
            {
                T? identifier = comboBoxAdv1.Items.OfType<T>().FirstOrDefault(x => x.Id.CompareTo(id) == 0);
                Selected = identifier;
                return;
            }

            ClearValue();
        }
    }

    #endregion

    public void ClearValue() => Selected = null;

    protected override void ClearItems() => comboBoxAdv1.Items.Clear();

    protected override void DoRefreshDataSource(IEnumerable<T> data) => comboBoxAdv1.Items.AddRange(data.ToArray());

    private void OnValueChanged(T? value)
    {
        ValueChanged?.Invoke(this, new SelectedValueChanged<T>(value));
        valueChanged?.Invoke(value);
        if (!lockManual)
        {
            ManualValueChange?.Invoke(this, new SelectedValueChanged<T>(value));
            valueSelected?.Invoke(value);
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
            if (comboBoxAdv1.Items[e.NewIndex] is T value)
            {
                OnValueChanged(value);
            }
        }
    }

    private void ButtonDelete_Click(object sender, EventArgs e) => ClearValue();

    private void ButtonOpen_Click(object sender, EventArgs e)
    {
        if (open != null && Selected != null)
        {
            open(Selected);
        }
    }

    #region IComboBoxControl interface

    IComboBoxControl<T> IComboBoxControl<T>.ReadOnly()
    {
        ReadOnly = true;
        return this;
    }

    IComboBoxControl<T> IComboBoxControl<T>.EnableEditor<E>()
    {
        var pageManager = Services.Provider.GetService<IPageManager>();
        if (pageManager != null)
        {
            OpenAction = pageManager.ShowEditor<E, T>;
        }

        return this;
    }

    IComboBoxControl<T> IComboBoxControl<T>.SetDataSource(Func<IEnumerable<T>?> func, DataRefreshMethod refreshMethod, Guid? selectValue)
    {
        RefreshMethod = refreshMethod;
        SetDataSource(func);
        if (selectValue != null)
        {
            Value = selectValue;
        }

        return this;
    }

    IComboBoxControl<T> IComboBoxControl<T>.ItemChanged(Action<T?> action)
    {
        valueChanged = action;
        return this;
    }

    IComboBoxControl<T> IComboBoxControl<T>.ItemSelected(Action<T?> action)
    {
        valueSelected = action;
        return this;
    }

    #endregion
}
