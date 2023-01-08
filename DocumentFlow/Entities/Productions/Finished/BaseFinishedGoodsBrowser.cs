//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.08.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
//  - класс стал абстрактным
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;
using DocumentFlow.Settings.Infrastructure;

namespace DocumentFlow.Entities.Productions.Finished;

public abstract class BaseFinishedGoodsBrowser : Browser<FinishedGoods>
{
    public BaseFinishedGoodsBrowser(IFinishedGoodsRepository repository, IPageManager pageManager, IDocumentFilter? filter = null, IStandaloneSettings? settings = null) 
        : base(repository, pageManager, filter: filter, settings: settings)
    {
    }

    protected override string HeaderText => "Готовая продукция";
}
