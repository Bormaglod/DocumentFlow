//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Controls.Editors;

/// <summary>
/// Эта панель состоит из 2-х частей:
///   - заголовок (высота 28px)
///   - панель контролов (с отступом в 10px, если заголовок установлен и 0px - если нет)
/// </summary>
/// <typeparam name="T"></typeparam>
public partial class DfPanel<T> : UserControl, IContainer<T>
    where T : class, new()
{
    private readonly IControls<T> controls;

    public DfPanel()
    {
        InitializeComponent();

        Dock = DockStyle.Top;
        Font = new Font("Segoe UI", 10, FontStyle.Regular, GraphicsUnit.Point);

        controls = Services.Provider.GetService<IControls<T>>()!;
        controls.Container = panelControls.Controls;

        panelHeader.Visible = false;
        panelControls.Padding = new Padding(0);
    }

    #region IContainer<T> interface

    IControls<T> IContainer<T>.Controls => controls;

    IContainer<T> IContainer<T>.HideHeader()
    {
        panelHeader.Visible = false;
        panelControls.Padding = new Padding(0);
        return this;
    }

    IContainer<T> IContainer<T>.ShowHeader(string title)
    {
        panelHeader.Visible = true;
        panelControls.Padding = new Padding(0, 10, 0, 0);
        labelHeader.Text = title;
        return this;
    }

    IContainer<T> IContainer<T>.AddControls(Action<IControls<T>> controls)
    {
        controls(this.controls);
        return this;
    }

    IContainer<T> IContainer<T>.SetDock(DockStyle dockStyle)
    {
        Dock = dockStyle;
        return this;
    }

    IContainer<T> IContainer<T>.SetHeight(int height)
    {
        Height = height;
        return this;
    }

    IContainer<T> IContainer<T>.SetVisible(bool visible)
    {
        Visible = visible;
        return this;
    }

    IContainer<T> IContainer<T>.SetName(string name)
    {
        Name = name;
        return this;
    }

    #endregion
}
