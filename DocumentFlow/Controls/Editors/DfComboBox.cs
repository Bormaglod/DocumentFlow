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
// Версия 2023.5.5
//  - из параметров конструктора удалены headerWidth и editorWidth
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Controls.Core;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Editors;

public partial class DfComboBox<T> : DataSourceControl<Guid, T>, IBindingControl, IAccess, IComboBoxControl<T>
    where T : class, IDocumentInfo
{
    private bool required = false;
    private bool lockManual = false;
    private OpenDialog<T>? open;
    private ControlValueChanged<T?>? valueChanged;
    private ControlValueChanged<T?>? valueSelected;

    public DfComboBox(string property, string header) : base(property)
    {
        InitializeComponent();
        SetLabelControl(label1, header);
        SetNestedControl(panelEdit);

        buttonOpen.Visible = false;
        panelSeparator3.Visible = false;
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

    #region IBindingControl interface

    public object? Value
    {
        get
        {
            if (comboBoxAdv1.SelectedItem != null && comboBoxAdv1.SelectedItem is T selectedItem)
            {
                return selectedItem.Id;
            }

            if (required)
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

            ClearSelectedValue();
        }
    }

    #endregion

    public void ClearSelectedValue() => Selected = null;

    protected override void ClearItems() => comboBoxAdv1.Items.Clear();

    protected override void DoRefreshDataSource(IEnumerable<T> data) => comboBoxAdv1.Items.AddRange(data.ToArray());

    private void OnValueChanged(T? value)
    {
        valueChanged?.Invoke(value);
        if (!lockManual)
        {
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

    private void ButtonDelete_Click(object sender, EventArgs e) => ClearSelectedValue();

    private void ButtonOpen_Click(object sender, EventArgs e)
    {
        if (open != null && Selected != null)
        {
            open(Selected);
        }
    }

    #region IComboBoxControl interface

    IComboBoxControl<T> IComboBoxControl<T>.Required()
    {
        required = true;
        panelSeparator1.Visible = false;
        buttonDelete.Visible = false;

        return this;
    }

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
            open = pageManager.ShowEditor<E, T>;
            buttonOpen.Visible = true;
            panelSeparator3.Visible = true;
        }

        return this;
    }

    IComboBoxControl<T> IComboBoxControl<T>.SetDataSource(GettingDataSource<T> func, DataRefreshMethod refreshMethod, Guid? selectValue)
    {
        RefreshMethod = refreshMethod;
        SetDataSource(func);
        if (selectValue != null)
        {
            Value = selectValue;
        }

        return this;
    }

    IComboBoxControl<T> IComboBoxControl<T>.ItemChanged(ControlValueChanged<T?> action)
    {
        valueChanged = action;
        return this;
    }

    IComboBoxControl<T> IComboBoxControl<T>.ItemSelected(ControlValueChanged<T?> action)
    {
        valueSelected = action;
        return this;
    }

    #endregion
}
