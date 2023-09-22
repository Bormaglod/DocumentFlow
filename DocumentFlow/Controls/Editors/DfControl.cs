//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.11.2021
//-----------------------------------------------------------------------

using System.ComponentModel;

namespace DocumentFlow.Controls;

[ToolboxItem(false)]
public partial class DfControl : UserControl
{
    private string header = string.Empty;
    private bool headerAutoSize = false;
    private ContentAlignment headerTextAlign = ContentAlignment.TopLeft;
    private bool headerVisible = true;
    private int headerWidth = 100;
    private DockStyle headerDock = DockStyle.Left;
    private Control? control;

    public DfControl()
    {
        InitializeComponent();
    }

    public string Header
    {
        get
        {
            string? name = header;
            if (string.IsNullOrEmpty(name))
            {
                if (Site is not null)
                {
                    name = Site.Name;
                }

                name ??= "Заголовок";

                labelHeader.Text = name;
            }

            return name;
        }

        set
        {
            if (header != value)
            {
                header = value;
                labelHeader.Text = value;
            }
        }
    }

    public bool HeaderAutoSize
    {
        get => headerAutoSize;
        set
        {
            if (headerAutoSize != value) 
            { 
                headerAutoSize = value;
                labelHeader.AutoSize = value;
            }
        }
    }

    public ContentAlignment HeaderTextAlign
    {
        get => headerTextAlign;
        set
        {
            if (headerTextAlign != value)
            {
                headerTextAlign = value;
                labelHeader.TextAlign = value;
            }
        }
    }

    public bool HeaderVisible
    {
        get => headerVisible;
        set
        {
            if (headerVisible != value)
            {
                headerVisible = value;
                labelHeader.Visible = value;
            }
        }
    }

    public int HeaderWidth
    {
        get => headerWidth;
        set
        {
            if (headerWidth != value) 
            { 
                headerWidth = value;
                labelHeader.Width = value;
            }
        }
    }

    public DockStyle HeaderDock
    {
        get => headerDock;
        set
        {
            if (headerDock != value)
            {
                headerDock = value;
                labelHeader.Dock = value;
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

    protected void SetNestedControl(Control control) => this.control = control;
}
