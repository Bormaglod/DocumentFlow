//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.10.2020
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;

using System.ComponentModel;

namespace DocumentFlow.Controls.Editors;

[ToolboxItem(true)]
public partial class DfDateTimePicker : DfControl, IAccess
{
    private DateTime dateTime = DateTime.Now;
    private bool isNull;
    private bool required;
    private bool enabledEditor = true;
    private string customFormat = "dd.MM.yyyy HH:mm:ss";
    private DateTimePickerFormat format = DateTimePickerFormat.Long;

    public event EventHandler? DateTimeValueChanged;
    public event EventHandler? BindableValueChanged;

    public DfDateTimePicker()
    {
        InitializeComponent();
        SetNestedControl(datePickerAdv);

        datePickerAdv.DataBindings.Add(nameof(datePickerAdv.BindableValue), this, nameof(BindableValue), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    public bool Required
    {
        get => required;

        set
        {
            if (required != value)
            {
                required = value;
                datePickerAdv.ShowCheckBox = !required;
            }
        }
    }

    public bool EnabledEditor
    {
        get => enabledEditor;
        set
        {
            if (enabledEditor != value)
            {
                enabledEditor = value;
                datePickerAdv.Enabled = value;
            }
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateTime BindableValue
    {
        get => dateTime;
        set => DateTimeValue = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateTime DateTimeValue
    {
        get => isNull ? DateTime.MinValue : dateTime;

        set
        {
            if (dateTime != value)
            {
                isNull = value == default;
                dateTime = isNull ? DateTime.Now : value;

                datePickerAdv.Checked = !isNull;

                DateTimeValueChanged?.Invoke(this, EventArgs.Empty);
                BindableValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public string CustomFormat
    {
        get => customFormat;
        set
        {
            if (customFormat != value)
            {
                customFormat = value;
                datePickerAdv.CustomFormat = value;
            }
        }
    }

    public DateTimePickerFormat Format
    {
        get => format;
        set
        {
            if (format != value)
            {
                format = value;
                datePickerAdv.Format = value;
            }
        }
    }

    private void DatePickerAdv_CheckBoxCheckedChanged(object sender, EventArgs e)
    {
        isNull = !datePickerAdv.Checked;
        DateTimeValueChanged?.Invoke(this, EventArgs.Empty);
    }
}
