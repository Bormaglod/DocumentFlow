//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Infrastructure.Controls;

public interface IContainer<T>
    where T : class, new()
{
    string? Name { get; }
    IControls<T> Controls { get; }
    IContainer<T> AddControls(Action<IControls<T>> controls);
    IContainer<T> SetDock(DockStyle dockStyle);
    IContainer<T> SetHeight(int height);
    IContainer<T> HideHeader();
    IContainer<T> ShowHeader(string title);
    IContainer<T> SetVisible(bool visible);
    IContainer<T> SetName(string name);
}
