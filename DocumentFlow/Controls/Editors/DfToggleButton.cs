//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;

using Syncfusion.Windows.Forms.Tools;

using System.ComponentModel;

namespace DocumentFlow.Controls.Editors;

[ToolboxItem(true)]
public partial class DfToggleButton : DfControl, IAccess
{
    private bool enabledEditor = true;
    private bool state;

    public event EventHandler? ToggleValueChanged;

    public DfToggleButton()
    {
        InitializeComponent();
        SetNestedControl(toggleButton1);
    }

    public bool EnabledEditor
    {
        get => enabledEditor;
        set
        {
            if (enabledEditor != value)
            {
                enabledEditor = value;
                toggleButton1.Enabled = value;
            }
        }
    }

    public bool ToggleValue
    {
        get => state;
        set
        {
            if (state != value)
            {
                state = value;
                toggleButton1.ToggleState = value ? ToggleButtonState.Active : ToggleButtonState.Inactive;
                ToggleValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void SetState(bool value) 
    {
        
    }

    private void ToggleButton1_ToggleStateChanged(object sender, ToggleStateChangedEventArgs e)
    {
        ToggleValue = e.ToggleState == ToggleButtonState.Active;
    }
}
