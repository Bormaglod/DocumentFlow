//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2021
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.5.5
//  - из параметров конструктора удалён headerWidth
// Версия 2023.5.11
//  - добавлен метод GetDefaultEditorWidth
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Controls.Core;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Editors;

public partial class DfToggleButton : BaseControl, IBindingControl, IAccess, IToggleButtonControl
{
    private ControlValueChanged<bool>? changed;

    public DfToggleButton(string property, string header) : base(property)
    {
        InitializeComponent();
        SetLabelControl(label1, header);
        SetNestedControl(toggleButton1);
    }

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

    public void ClearSelectedValue() => toggleButton1.ToggleState = ToggleButtonState.Inactive;

    protected override int GetDefaultEditorWidth() => 90;

    private void ToggleButton1_ToggleStateChanged(object sender, ToggleStateChangedEventArgs e) => changed?.Invoke(ToggleValue);

    #region IToggleButtonControl interface

    IToggleButtonControl IToggleButtonControl.ToggleChanged(ControlValueChanged<bool> action)
    {
        changed = action;
        return this;
    }

    #endregion
}
