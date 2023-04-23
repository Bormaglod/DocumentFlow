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

        EditorControls
            .AddTextBox(x => x.Code, "Код", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(180)
                    .DefaultAsValue())
            .AddTextBox(x => x.ItemName, "Наименование", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .AddDirectorySelectBox<Material>(x => x.ParentId, "Группа", select =>
                select
                    .SetDataSource(repository.GetOnlyFolders)
                    .ShowOnlyFolder()
                    .DirectoryChanged(ParentMaterialChanged)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .AddComboBox<Wire>(x => x.WireId, "Тип провода", combo =>
                combo
                    .SetDataSource(GetWires)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(200))
            .AddTextBox(x => x.ExtArticle, "Доп. артикул", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(250))
            .AddDirectorySelectBox<Material>(x => x.OwnerId, "Кросс-артикул", select =>
                select
                    .EnableEditor<IMaterialEditor>()
                    .SetDataSource(GetCrossMaterials)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .AddComboBox<Measurement>(x => x.MeasurementId, "Единица измерения", combo =>
                combo
                    .EnableEditor<IMeasurementEditor>()
                    .SetDataSource(GetMeasurements)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(250))
            .AddNumericTextBox(x => x.Weight, "Вес, г", text =>
                text
                    .SetNumberDecimalDigits(3)
                    .SetHeaderWidth(headerWidth))
            .AddCurrencyTextBox(x => x.Price, "Цена без НДС", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150)
                    .DefaultAsValue())
            .AddChoice<int>(x => x.Vat, "НДС", choice =>
                choice
                    .SetChoiceValues(Product.Taxes)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150))
            .AddNumericTextBox(x => x.MinOrder, "Мин. заказ", text =>
                text
                    .SetNumberDecimalDigits(3)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150));
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

    private void ParentMaterialChanged(Material? newValue)
    {
        var wire = EditorControls.GetControl(x => x.WireId);
        if (newValue != null)
        {
            var parents = repository.GetParentFolders(newValue.Id);
            wire.SetVisible(parents.FirstOrDefault(x => x.Id == Material.WireGroup) != null);
        }
        else
        {
            wire.Disable();
        }
    }

    private IEnumerable<Material> GetCrossMaterials()
    {
        var repo = Services.Provider.GetService<IMaterialRepository>();
        return repo!.GetAllValid(callback: query => query
            .OrderBy("item_name")
            .WhereNull("owner_id")
            .When(Document.Id != Guid.Empty, q => q
                .WhereNot("id", Document.Id)));
    }

    private IEnumerable<Wire> GetWires() => Services.Provider.GetService<IWireRepository>()!.GetAllValid();

    private IEnumerable<Measurement> GetMeasurements() => Services.Provider.GetService<IMeasurementRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name"));
}