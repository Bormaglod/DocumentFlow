//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.11.2021
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Controls.Core;

public abstract partial class BaseControl : UserControl, IControl
{
    private Control? control;
    private Label? label;

    private string tag = string.Empty;

    public BaseControl(string property)
    {
        InitializeComponent();

        AllowSaving = true;
        Dock = DockStyle.Top;
        PropertyName = property;
        DefaultAsNull = true;
    }

    public bool AllowSaving { get; set; }

    public string PropertyName { get; set; }

    public bool DefaultAsNull { get; set; }

    public IEditorPage? EditorPage { get; set; }

    public string Header
    {
        get => label?.Text ?? string.Empty;
        set
        {
            if (label != null)
            {
                label.Text = value;
                OnHeaderChanged();
            }
        }
    }

    public int HeaderWidth
    {
        get => label?.Width ?? 0;
        set
        {
            if (label != null)
            {
                label.Width = value;
            }
        }
    }

    public bool HeaderAutoSize
    {
        get => label?.AutoSize ?? false;
        set
        {
            if (label != null)
            {
                label.AutoSize = value;
            }
        }
    }

    public ContentAlignment HeaderTextAlign
    {
        get => label?.TextAlign ?? ContentAlignment.TopLeft;
        set
        {
            if (label != null)
            {
                label.TextAlign = value;
            }
        }
    }

    public bool HeaderVisible
    {
        get => label?.Visible ?? false;
        set
        {
            if (label != null)
            {
                label.Visible = value;
            }
        }
    }

    public int EditorWidth 
    { 
        get => control?.Width ?? 0;
        set
        {
            if (control != null)
            {
                control.Width = value;
            }
        }
    }

    public bool EditorFitToSize
    {
        get => control != null && control.Dock == DockStyle.Fill;
        set
        {
            if (control != null)
            {
                control.Dock = value ? DockStyle.Fill : control.Dock = DockStyle.Left;
            }
        }
    }

    protected void SetNestedControl(Control control, int editorWidth = default)
    {
        this.control = control;

        EditorWidth = editorWidth == default ? 100 : editorWidth;
    }

    protected void SetLabelControl(Label label, string header, int headerWidth = default)
    {
        this.label = label;

        Header = header;
        HeaderWidth = headerWidth == default ? 100 : headerWidth;
    }

    protected override void SetVisibleCore(bool value)
    {
        base.SetVisibleCore(value);
        AllowSaving = value;
    }

    protected virtual void OnHeaderChanged() { }

    #region IControl interface

    string IControl.Tag => tag;

    IControl IControl.SetTag(string value)
    {
        tag = value;
        return this;
    }

    IControl IControl.Disable()
    {
        Enabled = false;
        return this;
    }

    IControl IControl.SetVisible(bool visible)
    {
        Visible = visible;
        return this;
    }

    IControl IControl.SetEnabled(bool enabled)
    {
        Enabled = enabled;
        return this;
    }

    IControl IControl.DefaultAsValue()
    {
        DefaultAsNull = false;
        return this;
    }

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

    #endregion
}
