//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.08.2022
//
// Версия 2023.2.23
//  - свойство quantity класса FinishedGoods стало decimal, поэтому
//    тип поля "Количество" изменился на DfNumericTextBox
//  - при выборе изделия/калькуляции устанавливается значение еденицы измерения
//    в поле "Количество"
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Measurements;
using DocumentFlow.Entities.Productions.Lot;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Productions.Finished;

public class BaseFinishedGoodsEditor : DocumentEditor<FinishedGoods>
{
    private readonly int headerWidth = 160;

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

        var quantity = new DfNumericTextBox("quantity", "Количество", headerWidth, 100) { NumberDecimalDigits = 3 };
        var price = new DfCurrencyTextBox("price", "Себестоимость 1 ед. изм.", headerWidth, 100);
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

        goods.ValueChanged += (sender, e) =>
        {
            if (e.NewValue == null)
            {
                quantity.ShowSuffix = false;
            }
            else
            {
                var repoGoods = Services.Provider.GetService<IGoodsRepository>();
                var goods = repoGoods!.GetById(e.NewValue.Id, fullInformation: false);
                if (goods.measurement_id != null)
                {
                    var repoMeas = Services.Provider.GetService<IMeasurementRepository>();
                    var meas = repoMeas!.GetById(goods.measurement_id.Value);

                    quantity.ShowSuffix = true;
                    quantity.SuffixText = meas.abbreviation ?? meas.item_name ?? meas.code;
                }
            }
        };

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
