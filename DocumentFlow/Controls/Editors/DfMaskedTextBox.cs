//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;

using System.ComponentModel;

namespace DocumentFlow.Controls.Editors;

[ToolboxItem(true)]
public partial class DfMaskedTextBox : DfControl, IAccess
{
    private bool leadingZero;
    private decimal decimalValue;
    private bool enabledEditor = true;
    private bool showSuffix = true;
    private string mask = string.Empty;
    private char promptCharacter = ' ';
    private string suffix = "суффикс";

    public event EventHandler? RequiredValueChanged;

    public DfMaskedTextBox()
    {
        InitializeComponent();
        SetNestedControl(textBox);

        var binding = new Binding(nameof(textBox.Text), this, nameof(DecimalValue))
        {
            FormattingEnabled = true,
            DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        };

        textBox.DataBindings.Add(binding);
    }

    public bool EnabledEditor
    {
        get => enabledEditor;
        set
        {
            if (enabledEditor != value)
            {
                enabledEditor = value;
                textBox.Enabled = value;
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

    public string Mask
    {
        get => mask;
        set
        {
            if (mask != value)
            {
                mask = value;
                textBox.Mask = value;
                textBox.Text = decimalValue.ToString();
            }
        }
    }

    public char PromptCharacter
    {
        get => promptCharacter;
        set
        {
            if (promptCharacter != value)
            {
                promptCharacter = value;
                textBox.PromptCharacter = value;
            }
        }
    }

    public decimal DecimalValue
    {
        get => decimalValue;

        set
        {
            if (decimalValue != value)
            {
                int index = textBox.SelectionStart;
                decimalValue = value;
                RequiredValueChanged?.Invoke(this, EventArgs.Empty);
                textBox.SelectionStart = index;
            }
        }
    }

    public bool LeadingZero
    {
        get => leadingZero;

        set
        {
            if (leadingZero != value)
            {
                leadingZero = value;
                var binding = textBox.DataBindings[nameof(textBox.Text)];
                if (leadingZero)
                {
                    binding.Format += ConvertDecimal;
                }
                else
                {
                    binding.Format -= ConvertDecimal;
                }
            }
        }
    }

    public int MaxLength { get; set; }

    private void ConvertDecimal(object? sender, ConvertEventArgs e)
    {
        if (e.DesiredType == typeof(string))
        {
            e.Value ??= 0;
            e.Value = ((decimal)e.Value).ToString().PadLeft(MaxLength, '0');
        }
    }
}
