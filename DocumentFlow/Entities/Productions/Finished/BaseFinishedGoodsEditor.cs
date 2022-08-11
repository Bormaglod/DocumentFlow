//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Productions.Lot;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Productions.Finished;

public class BaseFinishedGoodsEditor : DocumentEditor<FinishedGoods>
{
    private readonly int headerWidth = 150;

    public BaseFinishedGoodsEditor(IFinishedGoodsRepository repository, IPageManager pageManager, bool nested) 
        : base(repository, pageManager, true) 
    {
        var lot = new DfDocumentSelectBox<ProductionLot>("owner_id", "Партия", headerWidth, 300)
        {
            OpenAction = (t) => pageManager.ShowEditor<IProductionLotEditor, ProductionLot>(t),
            ReadOnly = nested
        };

        var goods = new DfDirectorySelectBox<Goods>("goods_id", "Изделие", headerWidth, 500)
        {
            OpenAction = (t) => pageManager.ShowEditor<IGoodsEditor, Goods>(t)
        };

        var quantity = new DfIntegerTextBox<int>("quantity", "Количество", headerWidth, 100);
        var price = new DfCurrencyTextBox("price", "Себестоимость 1 изд.", headerWidth, 100);
        var cost = new DfCurrencyTextBox("product_cost", "Себестоимость (всего)", headerWidth, 100);

        lot.SetDataSource(() =>
        {
            var repo = Services.Provider.GetService<IProductionLotRepository>();
            return repo!.GetAllDefault(callback: q => q
                .WhereFalse("production_lot.deleted")
                .WhereTrue("production_lot.carried_out"));
        });

        lot.Columns += (sender, e) => ProductionLot.CreateGridColumns(e.Columns);

        lot.ValueChanged += (sender, e) =>
        {
            if (e.NewValue != null)
            {
                goods.Value = e.NewValue.goods_id;
                goods.ReadOnly = true;
            }
            else
            {
                goods.ReadOnly = false;
            }
        };

        goods.SetDataSource(() => Services.Provider.GetService<IGoodsRepository>()!.GetAllValid());

        AddControls(new Control[]
        {
            lot,
            goods,
            quantity,
            price,
            cost
        });
    }
}
