//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.01.2022
//
// Версия 2022.8.29
//  - расширены возможности полей owner и measurement (добавлены
//    кнопки для редактирования выбранных значений)
//  - удалена кнопка "Кросс"
// Версия 2023.1.21
//  - в вызове SetChoiceValues использовано свойство Product.Taxes
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Balances;
using DocumentFlow.Entities.Measurements;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Products;

public class MaterialEditor : Editor<Material>, IMaterialEditor
{
    private const int headerWidth = 190;
    private readonly IMaterialRepository repository;

    public MaterialEditor(IMaterialRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        this.repository = repository;

        var code = CreateTextBox(x => x.Code, "Код", headerWidth, 180, defaultAsNull: false);
        var name = CreateTextBox(x => x.ItemName, "Наименование", headerWidth, 400);
        var parent = CreateDirectorySelectBox(x => x.ParentId, "Группа", headerWidth, 400, showOnlyFolder: true, data: repository.GetOnlyFolders);
        var wire = CreateComboBox(x => x.WireId, "Тип провода", headerWidth, 200, data: GetWires);
        var ext_article = CreateTextBox(x => x.ExtArticle, "Доп. артикул", headerWidth, 250);
        var owner = CreateDirectorySelectBox<Material, IMaterialEditor>(x => x.OwnerId, "Кросс-артикул", headerWidth, 400);
        var measurement = CreateComboBox<Measurement, IMeasurementEditor>(x => x.MeasurementId, "Единица измерения", headerWidth, 250, data: GetMeasurements);
        var weight = CreateNumericTextBox(x => x.Weight, "Вес, г", headerWidth, 100, digits: 3);
        var price = CreateCurrencyTextBox(x => x.Price, "Цена без НДС", headerWidth, 150, defaultAsNull: false);
        var vat = CreateChoice(x => x.Vat, "НДС", headerWidth, 150, choices: Product.Taxes);
        var min_order = CreateNumericTextBox(x => x.MinOrder, "Мин. заказ", headerWidth, 150, digits: 3);

        parent.ValueChanged += (sender, e) =>
        {
            if (e.NewValue != null)
            {
                var parents = repository.GetParentFolders(e.NewValue.Id);
                wire.Visible = parents.FirstOrDefault(x => x.Id == Material.WireGroup) != null;
            }
            else
            {
                wire.Visible = false;
            }
        };

        owner.SetDataSource(() =>
        {
            var repo = Services.Provider.GetService<IMaterialRepository>();
            return repo!.GetAllValid(callback: query => query
                .OrderBy("item_name")
                .WhereNull("owner_id")
                .When(Document.Id != Guid.Empty, q => q
                    .WhereNot("id", Document.Id)));
        });

        AddControls(new Control[]
        {
            code,
            name,
            parent,
            wire,
            ext_article,
            owner,
            measurement,
            weight,
            price,
            vat,
            min_order
        });
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        if (repository.HasPrivilege(Privilege.Update))
        {
            RegisterNestedBrowser<IСustomerBrowser, Сustomer>();
            RegisterNestedBrowser<IBalanceMaterialBrowser, BalanceMaterial>();
            RegisterNestedBrowser<IMaterialUsageBrowser, MaterialUsage>();
        }
    }

    private IEnumerable<Wire> GetWires() => Services.Provider.GetService<IWireRepository>()!.GetAllValid();

    private IEnumerable<Measurement> GetMeasurements() => Services.Provider.GetService<IMeasurementRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name"));
}