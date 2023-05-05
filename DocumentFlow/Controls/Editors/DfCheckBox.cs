//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.12.2022
//
// Версия 2022.12.3
//  - добавлен параметр allowThreeState в конструктор и иниуиализация
//    checkBoxAdv1 в не нём
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.4.2
//  - добавлено наследование от ICheckBoxControl
// Версия 2023.5.5
//  - из параметров конструктора удалён headerWidth
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Controls.Core;

namespace DocumentFlow.Controls.Editors;

public partial class DfCheckBox : BaseControl, IBindingControl, IAccess, ICheckBoxControl
{
    private ControlValueChanged<bool>? checkChanged;

    public DfCheckBox(string property, string header) 
        : base(property)
    {
        InitializeComponent();
        SetLabelControl(label1, header);
        SetNestedControl(checkBoxAdv1);
    }

    public bool ReadOnly
    {
        get => !checkBoxAdv1.Enabled;
        set => checkBoxAdv1.Enabled = !value;
    }

    public object? Value
    {
        get => checkBoxAdv1.CheckState == CheckState.Indeterminate ? null : checkBoxAdv1.Checked;
        set => checkBoxAdv1.CheckState = value == null ? CheckState.Indeterminate : (((bool?)value).Value ? CheckState.Checked : CheckState.Unchecked);
    }

    private bool AllowThreeState { get => checkBoxAdv1.Tristate; set => checkBoxAdv1.Tristate = value; }

    #region IBindingControl interface

    void IBindingControl.ClearSelectedValue() => checkBoxAdv1.CheckState = AllowThreeState ? CheckState.Indeterminate : CheckState.Unchecked;

    #endregion

    #region ICheckBoxControl interface

    ICheckBoxControl ICheckBoxControl.ReadOnly()
    {
        ReadOnly = true;
        return this;
    }

    ICheckBoxControl ICheckBoxControl.AllowThreeState()
    {
        AllowThreeState = true;
        checkBoxAdv1.CheckState = CheckState.Indeterminate;
        return this;
    }

    ICheckBoxControl ICheckBoxControl.CheckChanged(ControlValueChanged<bool> action)
    {
        checkChanged = action;
        return this;
    }

    #endregion

    private void CheckBoxAdv1_CheckedChanged(object sender, EventArgs e) => checkChanged?.Invoke(checkBoxAdv1.Checked);
}
