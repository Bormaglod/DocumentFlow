//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.12.2019
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;

using System.ComponentModel;

namespace DocumentFlow.Controls.Editors;

[ToolboxItem(true)]
public partial class DfNumericTextBox : DfControl, IAccess
{
    private decimal decimalValue;
    private bool enabledEditor = true;
    private int numberDecimalDigits = 2;
    private bool showSuffix = false;
    private string suffix = "суффикс";

    public event EventHandler? DecimalValueChanged;

    public DfNumericTextBox()
    {
        InitializeComponent();
        SetNestedControl(decimalTextBox1);

        decimalTextBox1.DataBindings.Add(nameof(decimalTextBox1.DecimalValue), this, nameof(DecimalValue), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    public decimal DecimalValue
    {
        get => decimalValue;

        set
        {
            if (decimalValue != value)
            {
                decimalValue = value;
                DecimalValueChanged?.Invoke(this, EventArgs.Empty);
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
                decimalTextBox1.Enabled = value;
            }
        }
    }

    public int NumberDecimalDigits
    {
        get => numberDecimalDigits;
        set
        {
            if (numberDecimalDigits != value)
            {
                numberDecimalDigits = value;
                decimalTextBox1.NumberDecimalDigits = value;
            }
        }
    }

    public bool ShowSuffix
    {
        get => showSuffix;
        set
        {
            if (showSuffix != value)
            {
                showSuffix = value;
                labelSuffix.Visible = value;
            }
        }
    }

    public string Suffix
    {
        get => suffix;
        set
        {
            if (suffix != value)
            {
                suffix = value;
                labelSuffix.Text = value;
            }
        }
    }
}
