//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2021
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Editors;

public partial class DfToggleButton : BaseControl, IBindingControl, IAccess
{
    public DfToggleButton(string property, string header, int headerWidth = default) : base(property)
    {
        InitializeComponent();
        SetLabelControl(label1, header, headerWidth);
        SetNestedControl(toggleButton1, 90);
    }

    public event EventHandler? ValueChanged;

    public bool ToggleValue
    {
        get => toggleButton1.ToggleState == ToggleButtonState.Active;
        set => toggleButton1.ToggleState = ((bool?)value ?? false) ? ToggleButtonState.Active : ToggleButtonState.Inactive;
    }

    public bool ReadOnly
    {
        get => !toggleButton1.Enabled;
        set => toggleButton1.Enabled = !value;
    }

    public object? Value
    {
        get => ToggleValue;
        set => ToggleValue = ((bool?)value ?? false);
    }

    public void ClearValue() => toggleButton1.ToggleState = ToggleButtonState.Inactive;

    private void ToggleButton1_ToggleStateChanged(object sender, ToggleStateChangedEventArgs e) => ValueChanged?.Invoke(this, e);
}
