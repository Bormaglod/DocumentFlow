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
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Controls.Editors;

public partial class SelectBox<T> : DataSourceControl<Guid, T>, IBindingControl, IAccess
    where T : class, IIdentifier<Guid>
{
    private readonly List<T> items = new();
    private T? selectedItem;
    private bool requird = false;
    private Action<T>? open;

    public SelectBox(string property, string header, int headerWidth, int editorWidth = default) 
        : base(property)
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

        buttonOpen.Visible = false;
        panelSeparator3.Visible = false;
    }

    public event EventHandler<ChangeDataEventArgs<T>>? ValueChanged;

    public event EventHandler<ChangeDataEventArgs<T>>? ManualValueChange;

    public string Header { get => label1.Text; set => label1.Text = value; }

    public int HeaderWidth { get => label1.Width; set => label1.Width = value; }

    public bool HeaderAutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

    public ContentAlignment HeaderTextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

    public bool HeaderVisible { get => label1.Visible; set => label1.Visible = value; }

    public int EditorWidth { get => panelEdit.Width; set => panelEdit.Width = value; }

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
                T? oldValue = selectedItem;
                selectedItem = items.FirstOrDefault(x => x.Id == id);
                if (selectedItem == null && RefreshMethod == DataRefreshMethod.OnOpen) 
                {
                    selectedItem = GetDocument(id);
                }

                textValue.Text = selectedItem?.ToString();
                OnValueChanged(oldValue, selectedItem);
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

    public bool EditorFitToSize
    {
        get => panelEdit.Dock == DockStyle.Fill;
        set => panelEdit.Dock = value ? DockStyle.Fill : panelEdit.Dock = DockStyle.Left;
    }

    public string ValueText => textValue.Text;

    public T? SelectedItem
    {
        get => selectedItem;
        protected set => selectedItem = value;
    }

    public IEnumerable<T> Items => items;

    public void ClearValue()
    {
        textValue.Text = string.Empty;
        selectedItem = null;
    }

    public void ClearCurrent()
    {
        T? oldValue = selectedItem;
        ClearValue();
        OnValueChanged(oldValue, null);
    }

    protected override void DoRefreshDataSource(IEnumerable<T> data) => items.AddRange(data);

    protected override void ClearItems() => items.Clear();

    protected virtual void OnSelect() { }

    protected void SetTextValue(string text) => textValue.Text = text;

    protected void OnValueChanged(T? oldValue, T? newValue) => ValueChanged?.Invoke(this, new ChangeDataEventArgs<T>(oldValue, newValue));

    protected void OnManualValueChanged(T? oldValue, T? newValue) => ManualValueChange?.Invoke(this, new ChangeDataEventArgs<T>(oldValue, newValue));

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
