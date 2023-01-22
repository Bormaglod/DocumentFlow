//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.10.2020
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Controls.Editors;

public partial class DfDateTimePicker : BaseControl, IBindingControl, IAccess
{
    public DfDateTimePicker(string property, string header, int headerWidth, int editorWidth = default) : base(property)
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

        Value = DateTime.Now;
        Required = true;
    }

    public event EventHandler? ValueChanged;

    public string Header { get => label1.Text; set => label1.Text = value; }

    public int HeaderWidth { get => label1.Width; set => label1.Width = value; }

    public bool HeaderAutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

    public ContentAlignment HeaderTextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

    public bool HeaderVisible { get => label1.Visible; set => label1.Visible = value; }

    public int EditorWidth { get => datePickerAdv.Width; set => datePickerAdv.Width = value; }

    public bool Required { get => !datePickerAdv.ShowCheckBox; set => datePickerAdv.ShowCheckBox = !value; }

    public bool ReadOnly
    {
        get => datePickerAdv.ReadOnly;
        set => datePickerAdv.ReadOnly = !value;
    }

    #region IBindingControl interface

    public object? Value
    {
        get
        {
            if (!datePickerAdv.ShowCheckBox || datePickerAdv.Checked)
            {
                return datePickerAdv.Value;
            }
            else
            {
                return null;
            }
        }
        set
        {
            if (value == null)
            {
                if (datePickerAdv.ShowCheckBox)
                {
                    datePickerAdv.Checked = false;
                }
            }
            else
            {
                if (datePickerAdv.ShowCheckBox)
                {
                    datePickerAdv.Checked = true;
                }

                datePickerAdv.Value = (DateTime)value;
            }
        }
    }

    #endregion

    public bool EditorFitToSize
    {
        get => datePickerAdv.Dock == DockStyle.Fill;
        set => datePickerAdv.Dock = value ? DockStyle.Fill : datePickerAdv.Dock = DockStyle.Left;
    }

    public string CustomFormat { get => datePickerAdv.CustomFormat; set => datePickerAdv.CustomFormat = value; }

    public DateTimePickerFormat Format { get => datePickerAdv.Format; set => datePickerAdv.Format = value; }

    public void ClearValue()
    {
        datePickerAdv.ResetText();
        if (datePickerAdv.ShowCheckBox)
        {
            datePickerAdv.Checked = false;
        }
    }

    private void DatePickerAdv_ValueChanged(object sender, EventArgs e) => ValueChanged?.Invoke(this, e);

    private void DatePickerAdv_CheckBoxCheckedChanged(object sender, EventArgs e)
    {
        if (datePickerAdv.ShowCheckBox)
        {
            datePickerAdv.ShowDropButton = datePickerAdv.Checked;
        }

        ValueChanged?.Invoke(this, e); 
    }
}
