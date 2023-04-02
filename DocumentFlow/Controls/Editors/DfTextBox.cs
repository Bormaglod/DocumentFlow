//-----------------------------------------------------------------------
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
    public DfTextBox(string property, string header, int headerWidth = 100, int editorWidth = 100) : base(property)
    {
        InitializeComponent();

        Dock = DockStyle.Top;
        Header = header;
        HeaderWidth = headerWidth;
        EditorWidth = editorWidth;
    }

    public event EventHandler? ValueChanged;

    public string Header { get => label1.Text; set => label1.Text = value; }

    public int HeaderWidth { get => label1.Width; set => label1.Width = value; }

    public bool HeaderAutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

    public ContentAlignment HeaderTextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

    public bool HeaderVisible { get => label1.Visible; set => label1.Visible = value; }

    public int EditorWidth { get => textBoxExt.Width; set => textBoxExt.Width = value; }

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

    public bool EditorFitToSize
    {
        get => textBoxExt.Dock == DockStyle.Fill;
        set => textBoxExt.Dock = value ? DockStyle.Fill : textBoxExt.Dock = DockStyle.Left;
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

    #region ITextBoxControl interface

    ITextBoxControl ITextBoxControl.Multiline(int height)
    {
        Multiline = true;
        Height = height;
        return this;
    }

    #endregion
}
