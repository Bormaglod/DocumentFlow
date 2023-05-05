//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.04.2019
//
// Версия 2022.11.26
//  - изменен метод ButtonSelect_Click для проверки свойства RefreshMethod
//    и вызова метода RefreshDataSource()
//  - изменен метод set свойства Value. Если список значений заполняется
//    при открытии, то при загрузке он пуст и соответственно нет возможности
//    использовать его для установки значения, поэтому это значение
//    напрямую читается из соответствующего репозитария.
// Версия 2022.12.2
//  - при вызове метода Value.set может осуществляться вызов метода
//    GetById в котором парметр fullInformation был установлен в true,
//    что вызывало проблемы - изменен на false
// Версия 2022.12.24
//  - изменения связанные с добавлением методов GetSingleValueRepositoryType
//    и GetDocument в класс DataSourceControl
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.5.5
//  - из параметров конструктора удалены headerWidth и editorWidth
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Controls.Core;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Controls.Editors;

public partial class SelectBox<T> : DataSourceControl<Guid, T>, IBindingControl, IAccess
    where T : class, IIdentifier<Guid>
{
    private readonly List<T> items = new();
    private T? selectedItem;
    private bool requird = false;
    private OpenDialog<T>? open;
    private ControlValueChanged<T?>? valueChanged;
    private ControlValueChanged<T?>? valueSelected;

    public SelectBox(string property, string header) 
        : base(property)
    {
        InitializeComponent();
        SetLabelControl(label1, header);
        SetNestedControl(panelEdit);

        buttonOpen.Visible = false;
        panelSeparator3.Visible = false;
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
        get => textValue.ReadOnly;
        set
        {
            textValue.ReadOnly = value;
            buttonSelect.Enabled = !value;
            buttonDelete.Enabled = !value;
        }
    }

    public OpenDialog<T>? OpenAction
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
            if (selectedItem != null)
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
                selectedItem = items.FirstOrDefault(x => x.Id == id);
                if (selectedItem == null && RefreshMethod == DataRefreshMethod.OnOpen) 
                {
                    selectedItem = GetDocument(id);
                }

                textValue.Text = selectedItem?.ToString();
                OnValueChanged(selectedItem);
            }
            else
            {
                if (value == null)
                {
                    ClearCurrent();
                }
                else
                {
                    throw new ArgumentException($"Значение поля [{Header}] должно быть идентификатором типа GUID");
                }
            }
        }
    }

    #endregion

    public string ValueText => textValue.Text;

    public T? SelectedItem
    {
        get => selectedItem;
        protected set => selectedItem = value;
    }

    public IEnumerable<T> Items => items;

    public void ClearSelectedValue()
    {
        textValue.Text = string.Empty;
        selectedItem = null;
    }

    public void ClearCurrent()
    {
        ClearSelectedValue();
        OnValueChanged(null);
    }

    protected override void DoRefreshDataSource(IEnumerable<T> data) => items.AddRange(data);

    protected override void ClearItems() => items.Clear();

    protected virtual void OnSelect() { }

    protected void SetValueChangedAction(ControlValueChanged<T?>? action) => valueChanged = action;

    protected void SetValueSelectedAction(ControlValueChanged<T?>? action) => valueSelected = action;

    protected void SetTextValue(string text) => textValue.Text = text;

    protected void OnValueChanged(T? newValue) => valueChanged?.Invoke(newValue);

    protected void OnValueSelected(T? newValue) => valueSelected?.Invoke(newValue);

    private void ButtonDelete_Click(object sender, EventArgs e) => ClearCurrent();

    private void ButtonSelect_Click(object sender, EventArgs e)
    {
        if (RefreshMethod == DataRefreshMethod.OnOpen)
        {
            RefreshDataSource();
        }

        OnSelect();
    }

    private void ButtonOpen_Click(object sender, EventArgs e)
    {
        if (open != null && SelectedItem != null)
        {
            open(SelectedItem);
        }
    }
}
