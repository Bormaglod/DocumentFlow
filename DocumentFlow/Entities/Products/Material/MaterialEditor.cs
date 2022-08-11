//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Balances;
using DocumentFlow.Entities.Measurements;
using DocumentFlow.Infrastructure;
using DocumentFlow.Properties;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Products;

public class MaterialEditor : Editor<Material>, IMaterialEditor
{
    private const int headerWidth = 190;
    private readonly ToolStripButton button;
    private readonly IMaterialRepository repository;

    public MaterialEditor(IMaterialRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        this.repository = repository;

        button = Toolbar.Add("Кросс", Resources.icons8_goods_16, Resources.icons8_goods_30, () =>
        {
            if (Document.owner_id != null)
            {
                var editor = Services.Provider.GetService<IMaterialEditor>();
                if (editor != null)
                {
                    pageManager.ShowEditor(editor, Document.owner_id.Value);
                }
            }
        });

        var code = new DfTextBox("code", "Код", headerWidth, 180) { DefaultAsNull = false };
        var name = new DfTextBox("item_name", "Наименование", headerWidth, 400);
        var parent = new DfDirectorySelectBox<Material>("parent_id", "Группа", headerWidth, 400) { ShowOnlyFolder = true };
        var wire = new DfComboBox<Wire>("wire_id", "Тип провода", headerWidth, 200);
        var ext_article = new DfTextBox("ext_article", "Доп. артикул", headerWidth, 250);
        var owner = new DfDirectorySelectBox<Material>("owner_id", "Кросс-артикул", headerWidth, 400);
        var measurement = new DfComboBox<Measurement>("measurement_id", "Единица измерения", headerWidth, 250);
        var weight = new DfNumericTextBox("weight", "Вес, г", headerWidth, 100) { NumberDecimalDigits = 3 };
        var price = new DfCurrencyTextBox("price", "Цена без НДС", headerWidth, 150) { DefaultAsNull = false };
        var vat = new DfChoice<int>("vat", "НДС", headerWidth, 150);
        var min_order = new DfNumericTextBox("min_order", "Мин. заказ", headerWidth, 150) { NumberDecimalDigits = 3, DefaultAsNull = false };

        parent.SetDataSource(() => repository.GetOnlyFolders());
        parent.ValueChanged += (sender, e) =>
        {
            if (e.NewValue != null)
            {
                var parents = repository.GetParentFolders(e.NewValue.id);
                wire.Visible = parents.FirstOrDefault(x => x.id == Material.WireGroup) != null;
            }
            else
            {
                wire.Visible = false;
            }
        };

        wire.SetDataSource(() => Services.Provider.GetService<IWireRepository>()?.GetAllValid());

        owner.ValueChanged += (sender, e) => button.Enabled = e.NewValue != null;
        owner.SetDataSource(() =>
        {
            var repo = Services.Provider.GetService<IMaterialRepository>();
            return repo!.GetAllValid(callback: query => query
                .OrderBy("item_name")
                .WhereNull("owner_id")
                .When(Document.id != Guid.Empty, q => q
                    .WhereNot("id", Document.id)));
        });
        measurement.SetDataSource(() => Services.Provider.GetService<IMeasurementRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name")));
        vat.SetChoiceValues(new Dictionary<int, string>
        {
            [0] = "Без НДС",
            [10] = "10%",
            [20] = "20%"
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
}