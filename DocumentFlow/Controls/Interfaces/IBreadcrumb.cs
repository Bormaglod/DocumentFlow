//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Tools;
using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Controls.Interfaces;

public interface IBreadcrumb
{
    bool ShowButtonRefresh { get; set; }

    event EventHandler<CrumbClickEventArgs>? CrumbClick;

    void Push(IDirectory directory);
    Guid? Peek();
    Guid? Pop();
    void Clear();
}
