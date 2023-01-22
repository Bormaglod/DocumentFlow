//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.12.2019
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Editors;

public partial class DfIntegerTextBox<T> : BaseNumericTextBox<T, IntegerTextBox>, IAccess
    where T : struct, IComparable<T>
{
    public DfIntegerTextBox(string property, string header, int headerWidth, int editorWidth) :
        base(property, header, headerWidth, editorWidth)
    {
        InitializeComponent();

        TextBox.Style = TextBoxExt.theme.Office2016Colorful;
        TextBox.IntegerValueChanged += IntegerTextBox_IntegerValueChanged;
    }

    public bool ReadOnly
    {
        get => TextBox.ReadOnly;
        set => TextBox.ReadOnly = value;
    }

    public long MinValue { get => TextBox.MinValue; set => TextBox.MinValue = value; }

    public long MaxValue { get => TextBox.MaxValue; set => TextBox.MaxValue = value; }

    public bool AllowLeadingZeros { get => TextBox.AllowLeadingZeros; set => TextBox.AllowLeadingZeros = value; }

    public string NumberGroupSeparator { get => TextBox.NumberGroupSeparator; set => TextBox.NumberGroupSeparator = value; }

    public int[] NumberGroupSizes { get => TextBox.NumberGroupSizes; set => TextBox.NumberGroupSizes = value; }

    public override void ClearValue() => TextBox.Text = string.Empty;

    protected override T GetValueTextBox() => (T)Convert.ChangeType(TextBox.IntegerValue, typeof(T));

    protected override void UpdateTextControl(T value) => TextBox.IntegerValue = (long)Convert.ChangeType(value, typeof(long));

    private void IntegerTextBox_IntegerValueChanged(object? sender, EventArgs e) => UpdateNumericValue();
}
