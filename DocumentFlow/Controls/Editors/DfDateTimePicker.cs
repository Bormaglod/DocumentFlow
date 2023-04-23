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
using DocumentFlow.Infrastructure.Controls.Core;

namespace DocumentFlow.Controls.Editors;

public partial class DfDateTimePicker : BaseControl, IBindingControl, IAccess, IDateTimePickerControl
{
    private ControlValueChanged<DateTime?>? dateChanged;

    public DfDateTimePicker(string property, string header, int headerWidth = default, int editorWidth = default) : base(property)
    {
        InitializeComponent();
        SetLabelControl(label1, header, headerWidth);
        SetNestedControl(datePickerAdv, editorWidth);

        Value = DateTime.Now;
        Required = true;
    }

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

    public void ClearSelectedValue()
    {
        datePickerAdv.ResetText();
        if (datePickerAdv.ShowCheckBox)
        {
            datePickerAdv.Checked = false;
        }
    }

    private void OnDateChanged() => dateChanged?.Invoke((DateTime?)Value);

    private void DatePickerAdv_ValueChanged(object sender, EventArgs e) => OnDateChanged();

    private void DatePickerAdv_CheckBoxCheckedChanged(object sender, EventArgs e)
    {
        if (datePickerAdv.ShowCheckBox)
        {
            datePickerAdv.ShowDropButton = datePickerAdv.Checked;
        }

        OnDateChanged();
    }

    #region IDateTimePickerControl interface

    DateTime? IDateTimePickerControl.Value => (DateTime?)Value;

    IDateTimePickerControl IDateTimePickerControl.DateChanged(ControlValueChanged<DateTime?> action)
    {
        dateChanged = action;
        return this;
    }

    IDateTimePickerControl IDateTimePickerControl.ReadOnly()
    {
        ReadOnly = true;
        return this;
    }

    IDateTimePickerControl IDateTimePickerControl.NotRequired()
    {
        Required = false;
        return this;
    }

    IDateTimePickerControl IDateTimePickerControl.SetFormat(DateTimePickerFormat format)
    {
        Format = format; 
        return this;
    }

    IDateTimePickerControl IDateTimePickerControl.SetCustomFormat(string format)
    {
        Format = DateTimePickerFormat.Custom;
        CustomFormat = format;
        return this;
    }

    #endregion
}
