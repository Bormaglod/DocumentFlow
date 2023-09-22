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
public partial class DfCurrencyTextBox : DfControl, IAccess
{
    private decimal decimalValue;
    private bool enabledEditor = true;
    private int currencyDecimalDigits = 2;

    public event EventHandler? DecimalValueChanged;

    public DfCurrencyTextBox()
    {
        InitializeComponent();
        SetNestedControl(currencyTextBox1);

        currencyTextBox1.DataBindings.Add(nameof(currencyTextBox1.DecimalValue), this, nameof(DecimalValue), false, DataSourceUpdateMode.OnPropertyChanged);
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
                currencyTextBox1.Enabled = value;
            }
        }
    }

    public int CurrencyDecimalDigits
    {
        get => currencyDecimalDigits;
        set
        {
            if (currencyDecimalDigits != value)
            {
                currencyDecimalDigits = value;
                currencyTextBox1.CurrencyDecimalDigits = value;
            }
        }
    }
}
