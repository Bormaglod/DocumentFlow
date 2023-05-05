//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.11.2021
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.5.5
//  - методы SetNestedControl и SetLabelControl лишились параметров
//    editorWidth и headerWidth соответственно.
//  - у свойств HeaderWidth и EditorWidth удалён метод set и изменён тип
//    на int? - свойства будет возвращать null, если не установлено
//    значение с помощью метода SetHeaderWidth или SetEditorWidth 
//    соответственно
// - добавлен метод GetDefaultEditorWidth
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Controls.Core;

public abstract partial class BaseControl : UserControl, IControl
{
    private Control? control;
    private Label? label;

    private bool raise = false;
    private int groupRaise = 0;
    private bool inheritedHeaderWidth = true;
    private bool inheritedEditorWidth = true;

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

    protected virtual int GetDefaultEditorWidth() => 100;

    protected void SetNestedControl(Control control) => this.control = control;

    protected void SetLabelControl(Label label, string header)
    {
        this.label = label;

        Header = header;
    }

    protected override void SetVisibleCore(bool value)
    {
        base.SetVisibleCore(value);
        AllowSaving = value;
    }

    protected virtual void OnHeaderChanged() { }

    #region IControl interface

    int? IControl.HeaderWidth
    {
        get
        {
            if (inheritedHeaderWidth)
            {
                return null;
            }

            return label?.Width ?? 0;
        }
    }

    int? IControl.EditorWidth
    {
        get
        {
            if (inheritedEditorWidth)
            {
                return null;
            }

            return control?.Width ?? 0;
        }
    }

    bool IControl.IsRaised => raise;

    int IControl.GetRaisedGroup() => groupRaise;

    IControl IControl.Raise()
    {
        raise = true;
        return this;
    }

    IControl IControl.Raise(int group)
    {
        raise = true;
        groupRaise = group;
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

    IControl IControl.SetHeaderTextAlign(ContentAlignment alignment)
    {
        HeaderTextAlign = alignment;
        return this;
    }

    IControl IControl.SetDefaultEditorWidth()
    {
        IControl c = this;
        if (control != null)
        {
            c.SetEditorWidth(GetDefaultEditorWidth());
        }

        return c;
    }

    IControl IControl.SetHeaderWidth(int width)
    {
        if (label != null)
        {
            label.Width = width;
            inheritedHeaderWidth = false;
        }

        return this;
    }

    IControl IControl.SetEditorWidth(int width)
    {
        if (control != null)
        {
            control.Width = width;
            inheritedEditorWidth = false;
        }

        return this;
    }

    IControl IControl.SetDock(DockStyle dockStyle)
    {
        Dock = dockStyle;
        return this;
    }

    IControl IControl.SetPadding(int left, int top, int right, int bottom)
    {
        Padding = new(left, top, right, bottom);
        return this;
    }

    IControl IControl.SetWidth(int width)
    {
        Width = width;
        return this;
    }

    IControl IControl.EditorFitToSize()
    {
        EditorFitToSize = true;
        return this;
    }

    IControl IControl.If(bool condition, Action<IControl> trueAction, Action<IControl>? falseAction)
    {
        if (condition) 
        {
            trueAction(this);
        }
        else
        {
            falseAction?.Invoke(this);
        }

        return this;
    }

    #endregion
}
