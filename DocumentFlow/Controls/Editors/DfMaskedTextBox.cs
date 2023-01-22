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

public partial class DfMaskedTextBox<T> : BaseNumericTextBox<T, MaskedEditBox>, IAccess
    where T : struct, IComparable<T>
{
    private bool lockText = false;

    public DfMaskedTextBox(string property, string header, int headerWidth, int editorWidth, string? mask = null) :
        base(property, header, headerWidth, editorWidth)
    {
        InitializeComponent();

        Mask = mask ?? string.Empty;

        TextBox.Style = TextBoxExt.theme.Office2016Colorful;
        TextBox.TextChanged += MaskedEditBox_TextChanged;
    }

    public string Mask
    {
        get => TextBox.Mask;
        set
        {
            T? saved = (T?)Value;
            lockText = true;
            try
            {
                TextBox.Mask = value;
                Value = saved;
            }
            finally
            {
                lockText = false;
            }

            MaxLength = TextBox.Mask.Count(x => x != ' ');
        }
    }

    public char PromptCharacter
    {
        get => TextBox.PromptCharacter;
        set => TextBox.PromptCharacter = value;
    }

    public int MaxLength { get; set; }

    public bool ReadOnly
    {
        get => TextBox.ReadOnly;
        set => TextBox.ReadOnly = value;
    }

    public override void ClearValue() => TextBox.Text = string.Empty;

    protected override T GetValueTextBox() => (T)Convert.ChangeType(TextBox.Text, typeof(T));

    protected override void UpdateTextControl(T value)
    {
        string res = value.ToString() ?? string.Empty;
        if (MaxLength > 0)
        {
            res = res.PadLeft(MaxLength, '0');
        }

        TextBox.Text = res;
    }

    private void MaskedEditBox_TextChanged(object? sender, EventArgs e)
    {
        if (!lockText)
        {
            UpdateNumericValue();
        }
    }
}
