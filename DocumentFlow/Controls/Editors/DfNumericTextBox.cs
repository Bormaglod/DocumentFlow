//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Editors;

public partial class DfNumericTextBox : BaseNumericTextBox<decimal, DecimalTextBox>, IAccess
{
    public DfNumericTextBox(string property, string header, int headerWidth, int editorWidth = default) :
        base(property, header, headerWidth, editorWidth)
    {
        InitializeComponent();

        NumberDecimalDigits = 0;

        TextBox.Style = TextBoxExt.theme.Office2016Colorful;
        TextBox.DecimalValueChanged += DecimalText_DecimalValueChanged;
    }

    public bool ReadOnly
    {
        get => TextBox.ReadOnly;
        set => TextBox.ReadOnly = value;
    }

    public int NumberDecimalDigits { get => TextBox.NumberDecimalDigits; set => TextBox.NumberDecimalDigits = value; }

    public override void ClearValue() => TextBox.Text = string.Empty;

    protected override decimal GetValueTextBox() => TextBox.DecimalValue;

    protected override void UpdateTextControl(decimal value) => TextBox.DecimalValue = value;

    private void DecimalText_DecimalValueChanged(object? sender, EventArgs e) => UpdateNumericValue();
}
