//-----------------------------------------------------------------------
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
        AddControls(new Control[]
        {
            CreateDirectorySelectBox<Goods, IGoodsEditor>(x => x.ReferenceId, "Продукция", 100, 400, data: GetGoods),
            CreateNumericTextBox(x => x.Amount, "Количество", 100, 199, digits: 3),
            CreateCurrencyTextBox(x => x.OperationSumma, "Сумма", 100, 100)
        });
    }

    private IEnumerable<Goods> GetGoods() => Services.Provider.GetService<IGoodsRepository>()!.GetAllValid(callback: query =>
    {
        query
            .Select("goods.id")
            .Select("goods.code")
            .SelectRaw("goods.code || ', ' || goods.item_name as item_name")
            .OrderBy("goods.code");

        return query;
    });
}
