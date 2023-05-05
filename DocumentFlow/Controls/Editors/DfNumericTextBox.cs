﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.5.5
//  - из параметров конструктора удалены headerWidth и editorWidth
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Editors;

public partial class DfNumericTextBox : BaseNumericTextBox<decimal, DecimalTextBox>, IAccess, INumericTextBoxControl
{
    public DfNumericTextBox(string property, string header) :
        base(property, header)
    {
        InitializeComponent();

        NumberDecimalDigits = 0;

        TextBox.Style = TextBoxExt.theme.Office2016Colorful;
        TextBox.DecimalValueChanged += DecimalText_DecimalValueChanged;
    }

    public int NumberDecimalDigits { get => TextBox.NumberDecimalDigits; set => TextBox.NumberDecimalDigits = value; }

    public override void ClearSelectedValue() => TextBox.Text = string.Empty;

    protected override decimal GetValueTextBox() => TextBox.DecimalValue;

    protected override void UpdateTextControl(decimal value) => TextBox.DecimalValue = value;

    private void DecimalText_DecimalValueChanged(object? sender, EventArgs e) => UpdateNumericValue();

    #region INumericTextBoxControl interface

    INumericTextBoxControl INumericTextBoxControl.SetNumberDecimalDigits(int digits)
    {
        NumberDecimalDigits = digits;
        return this;
    }

    #endregion
}
