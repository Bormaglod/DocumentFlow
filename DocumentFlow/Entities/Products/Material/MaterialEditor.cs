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
// Версия 2023.5.20
//  - добавлено поле Kind
//  - удалён метод ParentMaterialChanged
//  - добавлен метод MaterialKindChanged
//  - добавлена возможность установки совместимых деталей
// Версия 2023.5.21
//  - добавлено поле DocName
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Dialogs.Infrastructure;
using DocumentFlow.Entities.Balances;
using DocumentFlow.Entities.Measurements;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Products;

public class MaterialEditor : Editor<Material>, IMaterialEditor
{
    private readonly IMaterialRepository repository;

    public MaterialEditor(IMaterialRepository repository, IPageManager pageManager) : base(repository, pageManager) 
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
            .AddDirectorySelectBox<Material>(x => x.ParentId, "Группа", select => select
                .SetDataSource(repository.GetOnlyFolders)
                .ShowOnlyFolder()
                .SetEditorWidth(400))
            .AddChoice<MaterialKind>(x => x.Kind, "Тип материала", choice => choice
                .Required()
                .ChoiceChanged(MaterialKindChanged)
                .SetChoiceValues(Material.Kinds)
                .SetEditorWidth(200))
            .AddComboBox<Wire>(x => x.WireId, "Тип провода", combo => combo
                .SetDataSource(GetWires)
                .SetEditorWidth(200))
            .AddTextBox(x => x.ExtArticle, "Доп. артикул", text => text
                .SetEditorWidth(250))
            .AddDirectorySelectBox<Material>(x => x.OwnerId, "Кросс-артикул", select => select
                .EnableEditor<IMaterialEditor>()
                .SetDataSource(GetCrossMaterials)
                .SetEditorWidth(400))
            .AddComboBox<Measurement>(x => x.MeasurementId, "Единица измерения", combo => combo
                .EnableEditor<IMeasurementEditor>()
                .SetDataSource(GetMeasurements)
                .SetEditorWidth(250))
            .AddNumericTextBox(x => x.Weight, "Вес, г", text => text
                .SetNumberDecimalDigits(3))
            .AddCurrencyTextBox(x => x.Price, "Цена без НДС", text => text
                .SetEditorWidth(150)
                .DefaultAsValue())
            .AddChoice<int>(x => x.Vat, "НДС", choice => choice
                .SetChoiceValues(Product.Taxes)
                .SetEditorWidth(150))
            .AddNumericTextBox(x => x.MinOrder, "Мин. заказ", text => text
                .SetNumberDecimalDigits(3)
                .SetEditorWidth(150))
            .AddDataGrid<CompatiblePart>(grid => grid
                .SetRepository<ICompatiblePartRepository>()
                .SetHeader("Совместимые детали")
                .Dialog<IDirectorySelectDialog<CompatiblePart, Material, IMaterialRepository>>()
                .SetDock(DockStyle.Fill));
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

    private void MaterialKindChanged(MaterialKind? kind)
    {
        if (kind != null) 
        {
            var wire = EditorControls.GetControl(x => x.WireId);
            wire.SetVisible(kind == MaterialKind.Wire);
        }
    }

    private IEnumerable<Material> GetCrossMaterials()
    {
        var repo = Services.Provider.GetService<IMaterialRepository>();
        return repo!.GetListExisting(callback: query => query
            .OrderBy("item_name")
            .WhereNull("owner_id")
            .When(Document.Id != Guid.Empty, q => q
                .WhereNot("id", Document.Id)));
    }

    private IEnumerable<Wire> GetWires() => Services.Provider.GetService<IWireRepository>()!.GetListExisting();

    private IEnumerable<Measurement> GetMeasurements() => Services.Provider.GetService<IMeasurementRepository>()!.GetListExisting(callback: q => q.OrderBy("item_name"));
}