﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Balances.Initial;

internal class InitialBalanceGoodsEditor : DocumentEditor<InitialBalanceGoods>, IInitialBalanceGoodsEditor
{
    public InitialBalanceGoodsEditor(IInitialBalanceGoodsRepository repository, IPageManager pageManager) 
        : base(repository, pageManager, true) 
    {
        EditorControls
            .AddDirectorySelectBox<Goods>(x => x.ReferenceId, "Продукция", (select) =>
                select
                    .SetDataSource(GetGoods)
                    .EnableEditor<IGoodsEditor>()
                    .SetEditorWidth(400))
            .AddNumericTextBox(x => x.Amount, "Количество", (text) =>
                text
                    .SetNumberDecimalDigits(3)
                    .SetEditorWidth(200))
            .AddCurrencyTextBox(x => x.OperationSumma, "Сумма");
    }

    private IEnumerable<Goods> GetGoods() => Services.Provider.GetService<IGoodsRepository>()!.GetListExisting(callback: query =>
    {
        query
            .Select("goods.id")
            .Select("goods.code")
            .SelectRaw("goods.code || ', ' || goods.item_name as item_name")
            .OrderBy("goods.code");

        return query;
    });
}
