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

public partial class DfCurrencyTextBox : BaseNumericTextBox<decimal, CurrencyTextBox>, IAccess, ICurrencyTextBoxControl
{
    public DfCurrencyTextBox(string property, string header, int headerWidth = default, int editorWidth = default) :
        base(property, header, headerWidth, editorWidth)
    {
        InitializeComponent();

        TextBox.Style = TextBoxExt.theme.Office2016Colorful;
        TextBox.DecimalValueChanged += CurrencyTextBox_DecimalValueChanged;
    }

    public override void ClearValue() => TextBox.Text = string.Empty;

    protected override decimal GetValueTextBox() => TextBox.DecimalValue;

    protected override void UpdateTextControl(decimal value) => TextBox.DecimalValue = value;

    private void CurrencyTextBox_DecimalValueChanged(object? sender, EventArgs e) => UpdateNumericValue();

    
}
