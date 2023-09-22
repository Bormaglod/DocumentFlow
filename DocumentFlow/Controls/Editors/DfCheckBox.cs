//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;

using System.ComponentModel;

namespace DocumentFlow.Controls.Editors;

[ToolboxItem(true)]
public partial class DfCheckBox : DfControl, IAccess
{
    private bool enabledEditor = true;
    private bool allowThreeState = false;
    private bool? checkValue = null;

    public event EventHandler? CheckValueChanged;

    public DfCheckBox()
    {
        InitializeComponent();
        SetNestedControl(checkBoxAdv1);

        var binding = new Binding(nameof(checkBoxAdv1.CheckState), this, nameof(CheckValue))
        {
            FormattingEnabled = true,
            DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        };

        binding.Format += BoolToCheckState;
        binding.Parse += CheckStateToBool;

        checkBoxAdv1.DataBindings.Add(binding);
    }

    public bool EnabledEditor
    {
        get => enabledEditor;
        set
        {
            if (enabledEditor != value)
            {
                enabledEditor = value;
                checkBoxAdv1.Enabled = value;
            }
        }
    }

    public bool? CheckValue
    {
        get => checkValue;
        set
        {
            if (checkValue != value)
            {
                checkValue = value;
                CheckValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public bool AllowThreeState
    {
        get => allowThreeState;
        set
        {
            if (allowThreeState != value)
            {
                allowThreeState = value;
                checkBoxAdv1.Tristate = value;
            }
        }
    }

    private void BoolToCheckState(object? sender, ConvertEventArgs arg)
    {
        if (arg.DesiredType == typeof(CheckState))
        {
            var boolValue = (bool?)arg.Value;
            if (boolValue == null)
            {
                arg.Value = allowThreeState ? CheckState.Indeterminate : CheckState.Unchecked;
            }
            else
            {
                arg.Value = boolValue.Value ? CheckState.Checked : CheckState.Unchecked;
            }
        }
    }

    private void CheckStateToBool(object? sender, ConvertEventArgs arg)
    {
        if (arg.DesiredType == typeof(bool?))
        {
            var state = CheckState.Unchecked;
            if (arg.Value != null)
            {
                state = (CheckState)arg.Value;
            }

            arg.Value = state switch
            {
                CheckState.Checked => true,
                CheckState.Unchecked => false,
                _ => allowThreeState ? null : false
            };
        }
    }
}
