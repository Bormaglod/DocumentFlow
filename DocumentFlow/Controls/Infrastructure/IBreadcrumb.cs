//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Controls.Infrastructure;

public interface IBreadcrumb
{
    bool ShowButtonRefresh { get; set; }

    event EventHandler<CrumbClickEventArgs>? CrumbClick;

    void Push(IDirectory directory);
    Guid? Peek();
    Guid? Pop();
    void Clear();
}
