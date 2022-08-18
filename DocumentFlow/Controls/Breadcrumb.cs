//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.06.2018
//
// Версия 2022.8.18
//  - Нажатие на кнопку "Домой" в строке выбора приводит к ошибке.
//    Исправлено.
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Data.Infrastructure;

using System.ComponentModel;

namespace DocumentFlow.Controls;

public partial class Breadcrumb : UserControl, IBreadcrumb
{
    private readonly Stack<SplitButton> dirs = new();

    public Breadcrumb()
    {
        InitializeComponent();
    }

    public event EventHandler<CrumbClickEventArgs>? CrumbClick;

    [Browsable(false)]
    public int Count => dirs.Count;

    [Category("Внешний вид")]
    [DefaultValue(true)]
    public bool ShowButtonRefresh
    {
        get => buttonRefresh.Visible;
        set => buttonRefresh.Visible = value;
    }

    public void Push(IDirectory directory)
    {
        if (directory is not IIdentifier<Guid> entity)
        {
            return;
        }

        SplitButton crumb = new()
        {
            Text = directory.item_name,
            BorderColor = Color.White,
            Dock = DockStyle.Left,
            Identifier = entity.id
        };

        panelCrumbs.Controls.Add(crumb);
        crumb.Click += Crumb_Click;
        crumb.BringToFront();

        dirs.Push(crumb);

        UpdateButtons();
    }

    public Guid? Peek()
    {
        if (Count == 0)
        {
            return null;
        }

        return dirs.Peek().Identifier;
    }

    public Guid? Pop()
    {
        if (Count == 0)
        {
            return null;
        }

        Guid directory = dirs.Pop().Identifier;
        SplitButton? button = panelCrumbs.Controls.Cast<SplitButton>().FirstOrDefault(c => c.Identifier == directory);
        if (button != null)
        {
            panelCrumbs.Controls.Remove(button);
        }

        UpdateButtons();
        return directory;
    }

    public void Clear()
    {
        while (dirs.Count > 0)
        {
            panelCrumbs.Controls.Remove(dirs.Pop());
        }

        UpdateButtons();
    }

    private void UpdateButtons() => buttonUp.Enabled = Count > 0;

    private void Crumb_Click(object? sender, EventArgs e)
    {
        if (sender is not SplitButton button)
        {
            return;
        }

        Guid? d;
        while (true)
        {
            d = Peek();
            if (d == null || d == button.Identifier)
                break;

            Pop();
        }

        if (CrumbClick != null)
        {
            CrumbClick(this, new CrumbClickEventArgs(ToolButtonKind.Refresh));
            UpdateButtons();
        }
    }

    private void ButtonUp_Click(object sender, EventArgs e)
    {
        if (CrumbClick != null)
        {
            CrumbClick(this, new CrumbClickEventArgs(ToolButtonKind.Up));
            UpdateButtons();
        }
    }

    private void ButtonHome_Click(object sender, EventArgs e)
    {
        if (CrumbClick != null)
        {
            CrumbClick(this, new CrumbClickEventArgs(ToolButtonKind.Home));
            UpdateButtons();
        }
    }

    private void ButtonRefresh_Click(object sender, EventArgs e)
    {
        if (CrumbClick != null)
        {
            CrumbClick(this, new CrumbClickEventArgs(ToolButtonKind.Refresh));
            UpdateButtons();
        }
    }
}
