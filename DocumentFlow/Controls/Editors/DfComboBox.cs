﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Data.Infrastructure;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Editors;

public partial class DfComboBox<T> : DataSourceControl<Guid, T>, IBindingControl, IAccess
    where T : class, IIdentifier<Guid>
{
    private bool requird = false;
    private bool lockManual = false;

    public DfComboBox(string property, string header, int headerWidth, int editorWidth = default) : base(property)
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

    public event EventHandler<SelectedValueChanged<T>>? ValueChanged;
    public event EventHandler<SelectedValueChanged<T>>? ManualValueChange;

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

    public Guid? SelectedItem => (Guid?)Value;

    #region IBindingControl interface

    public object? Value
    {
        get
        {
            if (comboBoxAdv1.SelectedItem != null && comboBoxAdv1.SelectedItem is T selectedItem)
            {
                return selectedItem.id;
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
                T? identifier = comboBoxAdv1.Items.OfType<T>().FirstOrDefault(x => x.id.CompareTo(id) == 0);
                SetSelectedItem(identifier);
                return;
            }

            ClearValue();
        }
    }

    #endregion

    public void ClearValue() => SetSelectedItem(null);

    protected override void ClearItems() => comboBoxAdv1.Items.Clear();

    protected override void DoRefreshDataSource(IEnumerable<T> data) => comboBoxAdv1.Items.AddRange(data.ToArray());

    private void SetSelectedItem(T? item)
    {
        lockManual = true;
        try
        {
            comboBoxAdv1.SelectedItem = item;
        }
        finally
        {
            lockManual = false;
        }
    }

    private void OnValueChanged(T? value)
    {
        ValueChanged?.Invoke(this, new SelectedValueChanged<T>(value));
        if (!lockManual)
        {
            ManualValueChange?.Invoke(this, new SelectedValueChanged<T>(value));
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
}