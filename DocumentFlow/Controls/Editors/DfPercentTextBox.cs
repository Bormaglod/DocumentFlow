//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.12.2019
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Editors;

public partial class DfPercentTextBox : BaseNumericTextBox<decimal, PercentTextBox>, IAccess
{
    public DfPercentTextBox(string property, string header, int headerWidth, int editorWidth) :
        base(property, header, headerWidth, editorWidth)
    {
        InitializeComponent();

        TextBox.Style = TextBoxExt.theme.Office2016Colorful;
        TextBox.DoubleValueChanged += PercentTextBox_DoubleValueChanged;
    }

    public bool ReadOnly
    {
        get => TextBox.ReadOnly;
        set => TextBox.ReadOnly = value;
    }

    public int PercentDecimalDigits { get => TextBox.PercentDecimalDigits; set => TextBox.PercentDecimalDigits = value; }

    public double MaxValue { get => TextBox.MaxValue; set => TextBox.MaxValue = value; }

    public double MinValue { get => TextBox.MinValue; set => TextBox.MinValue = value; }

    public override void ClearValue()
    {
        TextBox.PercentValue = 0;
    }

    protected override decimal GetValueTextBox() => Convert.ToDecimal(TextBox.DoubleValue) * 100;

    protected override void UpdateTextControl(decimal value) => TextBox.DoubleValue = Convert.ToDouble(value) / 100;

    private void PercentTextBox_DoubleValueChanged(object? sender, EventArgs e) => UpdateNumericValue();
}
