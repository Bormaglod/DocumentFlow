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
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Controls.Editor;

public partial class DfCheckBox : BaseControl, IBindingControl, IAccess, ICheckBoxControl
{
    public DfCheckBox(string property, string header, int headerWidth = 100, bool allowThreeState = false) : base(property)
    {
        InitializeComponent();

        Header = header;
        HeaderWidth = headerWidth;
        PropertyName = property;

        AllowThreeState = allowThreeState;
        if (allowThreeState)
        {
            checkBoxAdv1.CheckState = CheckState.Indeterminate;
        }
    }

    public event EventHandler? ValueChanged;

    public string Header { get => label1.Text; set => label1.Text = value; }

    public int HeaderWidth { get => label1.Width; set => label1.Width = value; }

    public bool HeaderAutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

    public ContentAlignment HeaderTextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

    public bool HeaderVisible { get => label1.Visible; set => label1.Visible = value; }

    public int EditorWidth { get => checkBoxAdv1.Width; set => checkBoxAdv1.Width = value; }

    public bool Checked
    {
        get => checkBoxAdv1.Checked;
        set => checkBoxAdv1.Checked = value;
    }

    public bool ReadOnly
    {
        get => !checkBoxAdv1.Enabled;
        set => checkBoxAdv1.Enabled = !value;
    }

    public bool AllowThreeState { get => checkBoxAdv1.Tristate; set => checkBoxAdv1.Tristate = value; }

    public object? Value
    {
        get => checkBoxAdv1.CheckState == CheckState.Indeterminate ? null : Checked;
        set => checkBoxAdv1.CheckState = value == null ? CheckState.Indeterminate : (((bool?)value).Value ? CheckState.Checked : CheckState.Unchecked);
    }

    public void ClearValue() => checkBoxAdv1.CheckState = AllowThreeState ? CheckState.Indeterminate : CheckState.Unchecked;

    private void CheckBoxAdv1_CheckedChanged(object sender, EventArgs e) => ValueChanged?.Invoke(this, e);

    #region IControl interface

    IControl IControl.SetHeaderWidth(int width)
    {
        HeaderWidth = width;
        return this;
    }

    IControl IControl.SetEditorWidth(int width)
    {
        EditorWidth = width;
        return this;
    }

    IControl IControl.Disable()
    {
        Enabled = false;
        return this;
    }

    IControl IControl.ReadOnly()
    {
        ReadOnly = true;
        return this;
    }

    IControl IControl.DefaultAsValue()
    {
        DefaultAsNull = false;
        return this;
    }

    #endregion

    #region ICheckBoxControl interface

    ICheckBoxControl ICheckBoxControl.AllowThreeState()
    {
        AllowThreeState = true;
        return this;
    }

    #endregion
}
