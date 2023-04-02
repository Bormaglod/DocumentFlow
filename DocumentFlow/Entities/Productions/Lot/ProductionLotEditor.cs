//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//
// Версия 2022.11.26
//  - параметр autoRefresh метода SetDataSource в классе
//    DataSourceControl был удален. Вместо него используется свойство
//    RefreshMethod этого класса в значении DataRefreshMethod.Immediately
// Версия 2022.2.23
//  - для поля "Количество" свойство NumberDecimalDigits изменено на значение 3
//  - при выборе изделия/калькуляции устанавливается значение еденицы измерения
//    в поле "Количество"
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Core;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Measurements;
using DocumentFlow.Entities.Productions.Finished;
using DocumentFlow.Entities.Productions.Order;
using DocumentFlow.Entities.Productions.Performed;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Properties;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Productions.Lot;

public class ProductionLotEditor : DocumentEditor<ProductionLot>, IProductionLotEditor
{
    private readonly IPageManager pageManager;
    private readonly int headerWidth = 100;

    public ProductionLotEditor(IProductionLotRepository repository, IPageManager pageManager) : base(repository, pageManager, true)
    {
        this.pageManager = pageManager;

        var order = CreateDocumentSelectBox<ProductionOrder, IProductionOrderEditor>(x => x.OwnerId, "Заказ", headerWidth, 300, data: GetOrders);
        var calc = CreateChoice<Guid>(x => x.CalculationId, "Изделие", headerWidth, 500, refreshMethod: DataRefreshMethod.Immediately);
        var quantity = CreateNumericTextBox(x => x.Quantity, "Количество", headerWidth, 100, digits: 3);
        var performed = new DfProductionLot() { Dock = DockStyle.Fill };

        order.Columns += (sender, e) => ProductionOrder.CreateGridColumns(e.Columns);
        order.ValueChanged += (sender, e) =>
        {
            if (e.NewValue != null)
            {
                var repo = Services.Provider.GetService<IProductionOrderRepository>();
                var list = repo!.GetList(e.NewValue);
                calc.SetDataSource(() =>
                {
                    return list.Select(x => new Choice<Guid>(x.CalculationId, x.ProductName));
                });

                calc.Value = Document.CalculationId;
            }
            else
            {
                calc.ClearValue();
            }
        };

        calc.ValueChanged += (sender, e) =>
        {
            if (e.Value == null)
            {
                quantity.ShowSuffix = false;
            }
            else
            {
                var repoCalc = Services.Provider.GetService<ICalculationRepository>();
                var calculation = repoCalc!.GetById(e.Value.Value, fullInformation: false);

                var repoGoods = Services.Provider.GetService<IGoodsRepository>();
                if (calculation.OwnerId != null)
                {
                    var goods = repoGoods!.GetById(calculation.OwnerId.Value, fullInformation: false);
                    if (goods.MeasurementId != null)
                    {
                        var repoMeas = Services.Provider.GetService<IMeasurementRepository>();
                        var meas = repoMeas!.GetById(goods.MeasurementId.Value);

                        quantity.ShowSuffix = true;
                        quantity.SuffixText = meas.Abbreviation ?? meas.ItemName ?? meas.Code;
                    }
                }
            }
        };

        AddControls(new Control[]
        {
            order,
            calc,
            quantity,
            performed
        });

        Toolbar.Add("Изделие", Resources.icons8_goods_16, Resources.icons8_goods_30, () =>
        {
            if (calc.ChoiceValue != null)
            {
                var repo = Services.Provider.GetService<ICalculationRepository>();
                if (repo != null)
                {
                    var c = repo.GetById(calc.ChoiceValue.Value, false);
                    if (c.OwnerId != null)
                    {
                        pageManager.ShowEditor<IGoodsEditor>(c.OwnerId.Value);
                    }
                }
            }
        });

        Toolbar.Add("Калькуляция", Resources.icons8_calculation_16, Resources.icons8_calculation_30, () =>
        {
            if (calc.ChoiceValue != null)
            {
                pageManager.ShowEditor<ICalculationEditor>(calc.ChoiceValue.Value);
            }
        });
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IOperationsPerformedNestedBrowser, OperationsPerformed>();
        RegisterNestedBrowser<IFinishedGoodsNestedBrowser, FinishedGoods>();
    }

    private IEnumerable<ProductionOrder> GetOrders()
    {
        var repo = Services.Provider.GetService<IProductionOrderRepository>();
        return repo!.GetAllDefault(callback: q => q
            .WhereFalse("production_order.deleted")
            .WhereTrue("production_order.carried_out")
            .WhereFalse("production_order.closed"));
    }
}
