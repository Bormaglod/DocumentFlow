//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.01.2022
//
// Версия 2022.8.29
//  - расширены возможности полей measurement и calculation (добавлены
//    кнопки для редактирования выбранных значений)
// Версия 2023.1.21
//  - в вызове SetChoiceValues использовано свойство Product.Taxes
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Balances;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Measurements;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Products;

public class GoodsEditor : Editor<Goods>, IGoodsEditor
{
    private const int headerWidth = 190;
    private readonly IGoodsRepository repository;

    public GoodsEditor(IGoodsRepository repository, IPageManager pageManager) : base(repository, pageManager)
    {
        this.repository = repository;

        var code = CreateTextBox(x => x.Code, "Код", headerWidth, 180, defaultAsNull: false);
        var name = CreateTextBox(x => x.ItemName, "Наименование", headerWidth, 600);
        var parent = CreateDirectorySelectBox(x => x.ParentId, "Группа", headerWidth, 400, showOnlyFolder: true, data: repository.GetOnlyFolders);
        var measurement = CreateComboBox<Measurement, IMeasurementEditor>(x => x.MeasurementId, "Единица измерения", headerWidth, 250);
        var weight = CreateNumericTextBox(x => x.Weight, "Вес, г", headerWidth, 100, digits: 3);
        var price = CreateCurrencyTextBox(x => x.Price, "Цена без НДС", headerWidth, 150, defaultAsNull: false);
        var vat = CreateChoice(x => x.Vat, "НДС", headerWidth, 150, choices: Product.Taxes);
        var is_service = CreateToggleButton(x => x.IsService, "Услуга", headerWidth);
        var note = CreateMultilineTextBox(x => x.Note, "Описание", headerWidth, 500);
        note.Dock = DockStyle.Fill;

        measurement.SetDataSource(() => Services.Provider.GetService<IMeasurementRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name")));

        var controls = new List<Control>() { code, name, parent, measurement, weight, price, vat, is_service, note };

        if (repository.HasPrivilege(Privilege.Update))
        {
            var calculation = CreateComboBox<Calculation, ICalculationEditor>(x => x.CalculationId, "Калькуляция", headerWidth, 400);

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