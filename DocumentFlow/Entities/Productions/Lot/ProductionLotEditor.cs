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
    private readonly int headerWidth = 100;

    public ProductionLotEditor(IProductionLotRepository repository, IPageManager pageManager) 
        : base(repository, pageManager, true)
    {
        EditorControls
            .AddDocumentSelectBox<ProductionOrder>(x => x.OwnerId, "Заказ", select =>
                select
                    .EnableEditor<IProductionOrderEditor>()
                    .SetDataSource(GetOrders)
                    .CreateColumns(ProductionOrder.CreateGridColumns)
                    .DocumentChanged(OrderChanged)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(300))
            .AddChoice<Guid>(x => x.CalculationId, "Изделие", choice =>
                choice
                    .ChoiceChanged(CalculationChanged)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(500))
            .AddNumericTextBox(x => x.Quantity, "Количество", text =>
                text
                    .SetNumberDecimalDigits(3)
                    .SetHeaderWidth(headerWidth))
            .AddProductionLot(lot => lot.SetDock(DockStyle.Fill));

        Toolbar.Add("Изделие", Resources.icons8_goods_16, Resources.icons8_goods_30, () =>
        {
            var calc = EditorControls.GetControl<IChoiceControl<Guid>>(x => x.CalculationId);
            if (calc.SelectedValue != null)
            {
                var repo = Services.Provider.GetService<ICalculationRepository>();
                if (repo != null)
                {
                    var c = repo.Get(calc.SelectedValue.Value, false);
                    if (c.OwnerId != null)
                    {
                        pageManager.ShowEditor<IGoodsEditor>(c.OwnerId.Value);
                    }
                }
            }
        });

        Toolbar.Add("Калькуляция", Resources.icons8_calculation_16, Resources.icons8_calculation_30, () =>
        {
            var calc = EditorControls.GetControl<IChoiceControl<Guid>>(x => x.CalculationId);
            if (calc.SelectedValue != null)
            {
                pageManager.ShowEditor<ICalculationEditor>(calc.SelectedValue.Value);
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
        return repo!.GetListUserDefined(callback: q => q
            .WhereFalse("production_order.deleted")
            .WhereTrue("production_order.carried_out")
            .WhereFalse("production_order.closed"));
    }

    private void OrderChanged(ProductionOrder? newValue)
    {
        var calc = EditorControls.GetControl<IChoiceControl<Guid>>(x => x.CalculationId);
        if (newValue != null)
        {
            var repo = Services.Provider.GetService<IProductionOrderRepository>();
            var list = repo!.GetList(newValue);
            calc.SetDataSource(() =>
            {
                return list.Select(x => new Choice<Guid>(x.CalculationId, x.ProductName));
            }, DataRefreshMethod.Immediately);

            calc.SelectedValue = Document.CalculationId;
        }
        else
        {
            calc.ClearSelectedValue();
        }
    }

    private void CalculationChanged(Guid? value)
    {
        var quantity = EditorControls.GetControl<INumericTextBoxControl>(x => x.Quantity);
        if (value == null)
        {
            quantity.HideSuffix();
        }
        else
        {
            var repoCalc = Services.Provider.GetService<ICalculationRepository>();
            var calculation = repoCalc!.Get(value.Value, fullInformation: false);

            var repoGoods = Services.Provider.GetService<IGoodsRepository>();
            if (calculation.OwnerId != null)
            {
                var goods = repoGoods!.Get(calculation.OwnerId.Value, fullInformation: false);
                if (goods.MeasurementId != null)
                {
                    var repoMeas = Services.Provider.GetService<IMeasurementRepository>();
                    var meas = repoMeas!.Get(goods.MeasurementId.Value);

                    quantity.ShowSuffix(meas.Abbreviation ?? meas.ItemName ?? meas.Code);
                }
            }
        }
    }
}
