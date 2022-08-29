//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.01.2022
//
// Версия 2022.8.29
//  - расширены возможности полей measurement и calculation (добавлены
//    кнопки для редактирования выбранных значений)
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Balances;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Measurements;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Products;

public class GoodsEditor : Editor<Goods>, IGoodsEditor
{
    private const int headerWidth = 190;
    private readonly IGoodsRepository repository;

    public GoodsEditor(IGoodsRepository repository, IPageManager pageManager) : base(repository, pageManager)
    {
        this.repository = repository;

        var code = new DfTextBox("code", "Код", headerWidth, 180) { DefaultAsNull = false };
        var name = new DfTextBox("item_name", "Наименование", headerWidth, 600);
        var parent = new DfDirectorySelectBox<Goods>("parent_id", "Группа", headerWidth, 400) { ShowOnlyFolder = true };
        var measurement = new DfComboBox<Measurement>("measurement_id", "Единица измерения", headerWidth, 250)
        {
            OpenAction = (p) => pageManager.ShowEditor<IMeasurementEditor, Measurement>(p)
        };

        var weight = new DfNumericTextBox("weight", "Вес, г", headerWidth, 100) { NumberDecimalDigits = 3 };
        var price = new DfCurrencyTextBox("price", "Цена без НДС", headerWidth, 150) { DefaultAsNull = false };
        var vat = new DfChoice<int>("vat", "НДС", headerWidth, 150);
        
        var is_service = new DfToggleButton("is_service", "Услуга", headerWidth);
        var note = new DfTextBox("note", "Описание", headerWidth, 500) { Multiline = true, Dock = DockStyle.Fill };

        parent.SetDataSource(() => repository.GetOnlyFolders());
        measurement.SetDataSource(() => Services.Provider.GetService<IMeasurementRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name")));
        vat.SetChoiceValues(new Dictionary<int, string>
        {
            [0] = "Без НДС",
            [10] = "10%",
            [20] = "20%"
        });

        var controls = new List<Control>() { code, name, parent, measurement, weight, price, vat, is_service, note };

        if (repository.HasPrivilege(Privilege.Update))
        {
            var calculation = new DfComboBox<Calculation>("calculation_id", "Калькуляция", headerWidth, 400)
            {
                OpenAction = (p) => pageManager.ShowEditor<ICalculationEditor, Calculation>(p)
            };

            calculation.SetDataSource(() => Services.Provider.GetService<ICalculationRepository>()!.GetApproved(Document));

            controls.Insert(7, calculation);
        }

        is_service.ValueChanged += (sender, e) =>
        {
            weight.Visible = !is_service.ToggleValue;
            measurement.Visible = !is_service.ToggleValue;
        };

        AddControls(controls);
    }

    protected override void DoAfterRefreshData()
    {
        base.DoAfterRefreshData();
        if (repository.HasPrivilege(Privilege.Update))
        {
            RegisterNestedBrowser<ICalculationBrowser, Calculation>();
            RegisterNestedBrowser<IBalanceGoodsBrowser, BalanceGoods>();
        }
    }
}