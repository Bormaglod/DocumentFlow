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
public partial class DfIntegerTextBox : DfControl, IAccess
{
    private long integerValue;
    private bool enabledEditor = true;
    private bool showSuffix = false;
    private string suffix = "суффикс";
    private string numberGroupSeparator = " ";
    private int[] numberGroupSizes = new int[] { 3 };

    public event EventHandler? IntegerValueChanged;

    public DfIntegerTextBox()
    {
        InitializeComponent();
        SetNestedControl(integerTextBox1);

        integerTextBox1.DataBindings.Add(nameof(integerTextBox1.IntegerValue), this, nameof(IntegerValue), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    public long IntegerValue
    {
        get => integerValue;

        set
        {
            if (integerValue != value)
            {
                integerValue = value;
                IntegerValueChanged?.Invoke(this, EventArgs.Empty);
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
                integerTextBox1.Enabled = value;
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

    public string NumberGroupSeparator
    {
        get => numberGroupSeparator;
        set
        {
            if (numberGroupSeparator != value)
            {
                numberGroupSeparator = value;
                integerTextBox1.NumberGroupSeparator = value;
            }
        }
    }

    public int[] NumberGroupSizes 
    { 
        get => numberGroupSizes; 
        set
        {
            if (numberGroupSizes != value)
            {
                numberGroupSizes = value;
                integerTextBox1.NumberGroupSizes = value;
            }
        }
    }
}
