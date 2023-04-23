//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.1.25
//  - изменены функции ClearValue и GetValueTextBox вызов которых (при
//    значении Value == null) приводил к ошибке
// Версия 2023.4.2
//  - добавлено наследование от IMaskedTextBoxControl
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Editors;

public partial class DfMaskedTextBox<T> : BaseNumericTextBox<T, MaskedEditBox>, IAccess, IMaskedTextBoxControl<T>
    where T : struct, IComparable<T>
{
    private bool lockText = false;

    public DfMaskedTextBox(string property, string header, int headerWidth = default, int editorWidth = default, string? mask = null) :
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

    public override void ClearSelectedValue() => TextBox.Text = default(T).ToString();

    protected override T GetValueTextBox() => (T)Convert.ChangeType(TextBox.Text.Trim(), typeof(T));

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

    #region IMaskedTextBoxControl interface

    IMaskedTextBoxControl<T> IMaskedTextBoxControl<T>.SetMask(string mask)
    {
        Mask = mask;
        return this;
    }

    #endregion
}
