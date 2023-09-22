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
public partial class DfPercentTextBox : DfControl, IAccess
{
    private decimal percentValue;
    private bool enabledEditor = true;
    private int numberDecimalDigits = 2;

    public event EventHandler? PercentValueChanged;

    public DfPercentTextBox()
    {
        InitializeComponent();
        SetNestedControl(percentTextBox1);

        percentTextBox1.DataBindings.Add(nameof(percentTextBox1.PercentValue), this, nameof(PercentValue), true, DataSourceUpdateMode.OnPropertyChanged);
    }

    public decimal PercentValue
    {
        get => percentValue;

        set
        {
            if (percentValue != value)
            {
                percentValue = value;
                PercentValueChanged?.Invoke(this, EventArgs.Empty);
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
                percentTextBox1.Enabled = value;
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
                percentTextBox1.PercentDecimalDigits = value;
            }
        }
    }
}
