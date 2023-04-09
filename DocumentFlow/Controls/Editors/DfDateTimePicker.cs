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
    public DfDateTimePicker(string property, string header, int headerWidth = default, int editorWidth = default) : base(property)
    {
        InitializeComponent();
        SetLabelControl(label1, header, headerWidth);
        SetNestedControl(datePickerAdv, editorWidth);

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
