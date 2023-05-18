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
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Productions.Finished;

public class BaseFinishedGoodsEditor : DocumentEditor<FinishedGoods>
{
   private readonly int headerWidth = 160;

    public BaseFinishedGoodsEditor(IFinishedGoodsRepository repository, IPageManager pageManager, bool nested)
        : base(repository, pageManager, true)
    {
        EditorControls
            .AddDocumentSelectBox<ProductionLot>(x => x.OwnerId, "Партия", select =>
                select
                    .SetDataSource(GetLots)
                    .ReadOnly(nested)
                    .CreateColumns(ProductionLot.CreateGridColumns)
                    .DocumentChanged(LotChanged)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(300))
            .AddDirectorySelectBox<Goods>(x => x.GoodsId, "Изделие", select =>
                select
                    .SetDataSource(GetGoods)
                    .DirectoryChanged(GoodsChanged)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(500))
            .AddNumericTextBox(x => x.Quantity, "Количество", text => 
                text
                    .SetNumberDecimalDigits(3)
                    .SetHeaderWidth(headerWidth))
            .AddCurrencyTextBox(x => x.Price, "Себестоимость 1 ед. изм.", text =>
                text
                    .SetHeaderWidth(headerWidth))
            .AddCurrencyTextBox(x => x.ProductCost, "Себестоимость (всего)", text =>
                text
                    .SetHeaderWidth(headerWidth));
    }

    private void GoodsChanged(Goods? newValue)
    {
        var quantity = EditorControls.GetControl<INumericTextBoxControl>(x => x.Quantity);
        if (newValue == null)
        {
            quantity.HideSuffix();
        }
        else
        {
            var repoGoods = Services.Provider.GetService<IGoodsRepository>();
            var goods = repoGoods!.Get(newValue.Id, fullInformation: false);
            if (goods.MeasurementId != null)
            {
                var repoMeas = Services.Provider.GetService<IMeasurementRepository>();
                var meas = repoMeas!.Get(goods.MeasurementId.Value);

                quantity.ShowSuffix(meas.Abbreviation ?? meas.ItemName ?? meas.Code);
            }
        }
    }

    private void LotChanged(ProductionLot? newValue)
    {
        var goods = EditorControls.GetControl<IDirectorySelectBoxControl<Goods>>(x => x.GoodsId);
        if (newValue != null)
        {
            if (goods is IDataSourceControl<Guid, Goods> source)
            {
                source.Select(newValue.GoodsId);
            }

            goods.ReadOnly(true);
        }
        else
        {
            goods.ReadOnly(false);
        }
    }

    private IEnumerable<ProductionLot> GetLots()
    {
        var repo = Services.Provider.GetService<IProductionLotRepository>();
        return repo!.GetListUserDefined(callback: q => q
            .WhereFalse("production_lot.deleted")
            .WhereTrue("production_lot.carried_out"));
    }

    private IEnumerable<Goods> GetGoods() => Services.Provider.GetService<IGoodsRepository>()!.GetListExisting();
}
