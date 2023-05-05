//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.12.2019
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

public partial class DfPercentTextBox : BaseNumericTextBox<decimal, PercentTextBox>, IAccess, IPercentTextBoxControl
{
    public DfPercentTextBox(string property, string header) :
        base(property, header)
    {
        InitializeComponent();

        TextBox.Style = TextBoxExt.theme.Office2016Colorful;
        TextBox.DoubleValueChanged += PercentTextBox_DoubleValueChanged;
    }

    public int PercentDecimalDigits { get => TextBox.PercentDecimalDigits; set => TextBox.PercentDecimalDigits = value; }

    public double MaxValue { get => TextBox.MaxValue; set => TextBox.MaxValue = value; }

    public double MinValue { get => TextBox.MinValue; set => TextBox.MinValue = value; }

    public override void ClearSelectedValue() => TextBox.PercentValue = 0;

    protected override decimal GetValueTextBox() => Convert.ToDecimal(TextBox.DoubleValue) * 100;

    protected override void UpdateTextControl(decimal value) => TextBox.DoubleValue = Convert.ToDouble(value) / 100;

    private void PercentTextBox_DoubleValueChanged(object? sender, EventArgs e) => UpdateNumericValue();

    #region IPercentTextBoxControl interface

    IPercentTextBoxControl IPercentTextBoxControl.SetPercentDecimalDigits(int digits)
    {
        PercentDecimalDigits = digits;
        return this;
    }

    #endregion
}
