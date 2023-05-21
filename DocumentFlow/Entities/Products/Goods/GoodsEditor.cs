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
// Версия 2023.5.21
//  - добавлено поле DocName
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Balances;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Measurements;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.Linq;

namespace DocumentFlow.Entities.Products;

public class GoodsEditor : Editor<Goods>, IGoodsEditor
{
    private readonly IGoodsRepository repository;

    public GoodsEditor(IGoodsRepository repository, IPageManager pageManager) : base(repository, pageManager)
    {
        this.repository = repository;

        EditorControls
            .SetHeaderWidth(200)
            .AddTextBox(x => x.Code, "Код", text => text
                .SetEditorWidth(180)
                .DefaultAsValue())
            .AddTextBox(x => x.ItemName, "Наименование", text => text
                .SetEditorWidth(600))
            .AddTextBox(x => x.DocName, "Наименование для документов", text => text
                .SetEditorWidth(600))
            .AddDirectorySelectBox<Goods>(x => x.ParentId, "Группа", select => select
                .ShowOnlyFolder()
                .SetDataSource(repository.GetOnlyFolders)
                .SetEditorWidth(400))
            .AddComboBox<Measurement>(x => x.MeasurementId, "Единица измерения", combo => combo
                .EnableEditor<IMeasurementEditor>()
                .SetDataSource(GetMeasurements)
                .SetEditorWidth(250)
                .Raise())
            .AddNumericTextBox(x => x.Weight, "Вес, г", text => text
                .SetNumberDecimalDigits(3)
                .Raise())
            .AddCurrencyTextBox(x => x.Price, "Цена без НДС", text => text
                .SetEditorWidth(150)
                .DefaultAsValue())
            .AddChoice<int>(x => x.Vat, "НДС", choice => choice
                .SetChoiceValues(Product.Taxes)
                .SetEditorWidth(150))
            .If(repository.HasPrivilege(Privilege.Update), controls => controls
                .AddComboBox<Calculation>(x => x.CalculationId, "Калькуляция", combo => combo
                    .EnableEditor<ICalculationEditor>()
                    .SetDataSource(GetCalculations)
                    .SetEditorWidth(400)))
            .AddToggleButton(x => x.IsService, "Услуга", toggle => toggle
                .ToggleChanged(IsServiceChanged))
            .AddTextBox(x => x.Note, "Описание", text => text
                .Multiline()
                .SetEditorWidth(500)
                .SetDock(DockStyle.Fill));
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

    private void IsServiceChanged(bool value)
    {
        EditorControls.GetControls<IControl>()
                      .Where(x => x.IsRaised)
                      .ForEach(x => x.SetVisible(!value));
    }

    private IEnumerable<Calculation> GetCalculations() => Services.Provider.GetService<ICalculationRepository>()!.GetApproved(Document);

    private IEnumerable<Measurement> GetMeasurements() => Services.Provider.GetService<IMeasurementRepository>()!.GetListExisting(callback: q => q.OrderBy("item_name"));
}