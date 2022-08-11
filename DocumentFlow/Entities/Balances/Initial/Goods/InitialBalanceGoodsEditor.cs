//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
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
            new DfDirectorySelectBox<Goods>("reference_id", "Продукция", 100, 400)
            {
                OpenAction = (t) => pageManager.ShowEditor<IGoodsEditor, Goods>(t),
                DataSourceFunc = () => Services.Provider.GetService<IGoodsRepository>()?.GetAllValid(callback: query =>
                {
                    query
                        .Select("goods.id")
                        .Select("goods.code")
                        .SelectRaw("goods.code || ', ' || goods.item_name as item_name")
                        .OrderBy("goods.code");

                    return query;
                })
            },
            new DfNumericTextBox("amount", "Количество", 100, 199)
            {
                NumberDecimalDigits = 3
            },
            new DfCurrencyTextBox("operation_summa", "Сумма", 100, 100)
        }); ; ;
    }
}
