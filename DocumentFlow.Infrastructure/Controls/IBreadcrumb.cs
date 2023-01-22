//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2021
//
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Controls.Infrastructure в DocumentFlow.Infrastructure.Controls
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - добавлен ToolButtonKind
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls.Core;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Infrastructure.Controls;

public enum ToolButtonKind { Back, Forward, Up, Down, Refresh, Home, Delete, Select, Edit, Open }

public interface IBreadcrumb
{
    bool ShowButtonRefresh { get; set; }

    event EventHandler<CrumbClickEventArgs>? CrumbClick;

    void Push(IDirectory directory);
    Guid? Peek();
    Guid? Pop();
    void Clear();
}
