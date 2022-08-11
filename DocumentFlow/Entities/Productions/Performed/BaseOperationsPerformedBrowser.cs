//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Productions.Performed;

public class BaseOperationsPerformedBrowser : Browser<OperationsPerformed>
{
    public BaseOperationsPerformedBrowser(IOperationsPerformedRepository repository, IPageManager pageManager, IDocumentFilter? filter = null)
        : base(repository, pageManager, filter: filter)
    {
    }

    protected override string HeaderText => "Выполнение работ";
}
