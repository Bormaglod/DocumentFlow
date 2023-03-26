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

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Measurements;
using DocumentFlow.Entities.Productions.Lot;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Productions.Finished;

public class BaseFinishedGoodsEditor : DocumentEditor<FinishedGoods>
{
    private readonly IPageManager pageManager;
    private readonly int headerWidth = 160;

    public BaseFinishedGoodsEditor(IFinishedGoodsRepository repository, IPageManager pageManager, bool nested)
        : base(repository, pageManager, true)
    {
        this.pageManager = pageManager;

        var lot = CreateDocumentSelectBox<ProductionLot, IProductionLotEditor>(x => x.OwnerId, "Партия", headerWidth, 300, readOnly: nested, data: GetLots);
        var goods = CreateDirectorySelectBox<Goods, IGoodsEditor>(x => x.GoodsId, "Изделие", headerWidth, 500, data: GetGoods);
        var quantity = CreateNumericTextBox(x => x.Quantity, "Количество", headerWidth, 100, digits: 3);
        var price = CreateCurrencyTextBox(x => x.Price, "Себестоимость 1 ед. изм.", headerWidth, 100);
        var cost = CreateCurrencyTextBox(x => x.ProductCost, "Себестоимость (всего)", headerWidth, 100);

        lot.Columns += (sender, e) => ProductionLot.CreateGridColumns(e.Columns);
        lot.ValueChanged += (sender, e) =>
        {
            if (e.NewValue != null)
            {
                goods.Value = e.NewValue.GoodsId;
                goods.ReadOnly = true;
            }
            else
            {
                goods.ReadOnly = false;
            }
        };

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
                if (goods.MeasurementId != null)
                {
                    var repoMeas = Services.Provider.GetService<IMeasurementRepository>();
                    var meas = repoMeas!.GetById(goods.MeasurementId.Value);

                    quantity.ShowSuffix = true;
                    quantity.SuffixText = meas.Abbreviation ?? meas.ItemName ?? meas.Code;
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

    private IEnumerable<ProductionLot> GetLots()
    {
        var repo = Services.Provider.GetService<IProductionLotRepository>();
        return repo!.GetAllDefault(callback: q => q
            .WhereFalse("production_lot.deleted")
            .WhereTrue("production_lot.carried_out"));
    }

    private IEnumerable<Goods> GetGoods() => Services.Provider.GetService<IGoodsRepository>()!.GetAllValid();
}
