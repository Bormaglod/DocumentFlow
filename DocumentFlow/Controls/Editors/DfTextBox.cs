﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.12.2019
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.4.2
//  - добавлено наследование от ITextBoxControl
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Core;
using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Controls.Editors;

public partial class DfTextBox : BaseControl, IBindingControl, IAccess, ITextBoxControl
{
    public DfTextBox(string property, string header, int headerWidth = default, int editorWidth = default) 
        : base(property)
    {
        InitializeComponent();
        SetLabelControl(label1, header, headerWidth);
        SetNestedControl(textBoxExt, editorWidth);

        Dock = DockStyle.Top;
    }

    public event EventHandler? ValueChanged;

    public bool ReadOnly
    {
        get => textBoxExt.ReadOnly;
        set => textBoxExt.ReadOnly = value;
    }

    public object? Value
    {
        get => DefaultAsNull ? textBoxExt.Text.NullIfEmpty() : textBoxExt.Text;
        set => textBoxExt.Text = value == null ? string.Empty : value.ToString();
    }

    public bool Multiline 
    { 
        get => textBoxExt.Multiline;
        set
        { 
            textBoxExt.Multiline = value;
            textBoxExt.ScrollBars = value ? ScrollBars.Vertical : ScrollBars.None;
        }
    }

    public void ClearValue() => textBoxExt.Text = string.Empty;

    private void TextBoxExt_TextChanged(object sender, EventArgs e) => ValueChanged?.Invoke(this, e);

    #region ITextBoxControl interface

    ITextBoxControl ITextBoxControl.ReadOnly()
    {
        ReadOnly = true;
        return this;
    }

    ITextBoxControl ITextBoxControl.Multiline(int height)
    {
        Multiline = true;
        Height = height;
        return this;
    }

    #endregion
}
