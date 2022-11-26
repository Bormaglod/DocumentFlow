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
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Core;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Productions.Finished;
using DocumentFlow.Entities.Productions.Order;
using DocumentFlow.Entities.Productions.Performed;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure;
using DocumentFlow.Properties;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Productions.Lot;

public class ProductionLotEditor : DocumentEditor<ProductionLot>, IProductionLotEditor
{
    private readonly int headerWidth = 100;

    public ProductionLotEditor(IProductionLotRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        var order = new DfDocumentSelectBox<ProductionOrder>("owner_id", "Заказ", headerWidth, 300)
        {
            OpenAction = (t) => pageManager.ShowEditor<IProductionOrderEditor, ProductionOrder>(t)
        };

        var calc = new DfChoice<Guid>("calculation_id", "Изделие", headerWidth, 500) { RefreshMethod = DataRefreshMethod.Immediately };
        var quantity = new DfNumericTextBox("quantity", "Количество", headerWidth, 100);

        var performed = new DfProductionLot() { Dock = DockStyle.Fill };

        order.SetDataSource(() =>
        {
            var repo = Services.Provider.GetService<IProductionOrderRepository>();
            return repo!.GetAllDefault(callback: q => q
                .WhereFalse("production_order.deleted")
                .WhereTrue("production_order.carried_out")
                .WhereFalse("production_order.closed"));
        });

        order.Columns += (sender, e) => ProductionOrder.CreateGridColumns(e.Columns);

        order.ValueChanged += (sender, e) =>
        {
            if (e.NewValue != null)
            {
                var repo = Services.Provider.GetService<IProductionOrderRepository>();
                var list = repo!.GetList(e.NewValue);
                calc.SetDataSource(() =>
                {
                    return list.Select(x => new Choice<Guid>(x.calculation_id, x.product_name));
                });

                calc.Value = Document.calculation_id;
            }
            else
            {
                calc.ClearValue();
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
                    if (c.owner_id != null)
                    {
                        pageManager.ShowEditor<IGoodsEditor>(c.owner_id.Value);
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
}
