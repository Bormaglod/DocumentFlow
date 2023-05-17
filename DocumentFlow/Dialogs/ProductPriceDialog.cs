//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.11.2021
//
// Версия 2022.8.17
//  - из-за изменения видимости методов set для полей product_name и code
//    класса ProductPrice произведена замена инициализации этих полей
//    с помощью метода SetProductInfo
// Версия 2022.8.18
//  - диалоговое окно "Изделие" видно в панели задач #49. Исправлено.
// Версия 2022.8.19
//  - выбор изделий дополнен столбцом с артикулом
//  - добавлена возможность выбора цены из договора
// Версия 2022.8.20
//  - скорректирован порядок обхода элементов управления
// Версия 2022.8.29
//  - скорректирован порядок обхода элементов управления
//  - рефакторинг
// Версия 2022.11.26
//  - параметр autoRefresh метода SetDataSource в классе
//    DataSourceControl был удален. Вместо него используется свойство
//    RefreshMethod этого класса в значении DataRefreshMethod.Immediately
// Версия 2023.1.15
//  - добавлена обработка атрибута ProductExcludingPriceAttribute
// Версия 2023.1.21
//  - для установки значения калькуляции в классе ProductionOrderPrice
//    необходимо использовать метод SetCalculation
//  - в выборку калькуляций добавлено поле code
//  - добавлено использование Product.Taxes
//  - ArgumentNullException заменен на Exception с текстом
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.3.14
//  - GetAllMaterials заменен на GetAllValid
// Версия 2023.5.17
//  - элементы, которые исключаются из редактирование сделаны невидимыми
//
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Products;
using DocumentFlow.Entities.Products.Core;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

using Humanizer;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace DocumentFlow.Dialogs;

public partial class ProductPriceDialog<P> : Form
    where P : ProductPrice, new()
{
    private readonly IDirectorySelectBoxControl<Product> product;
    private readonly INumericTextBoxControl amount;
    private readonly ICurrencyTextBoxControl price;
    private readonly ICurrencyTextBoxControl cost;
    private readonly IChoiceControl<int> tax;
    private readonly ICurrencyTextBoxControl taxValue;
    private readonly ICurrencyTextBoxControl fullCost;

    private readonly IControls<P> controls;

    private readonly Contract? contract;
    private readonly bool excludePrice;
    private readonly ProductContent content;

    private readonly IReadOnlyDictionary<string, string> columns = new Dictionary<string, string>
    {
        ["Code"] = "Артикул",
        ["ItemName"] = "Наименование"
    };

    public ProductPriceDialog(Contract? contract)
    {
        InitializeComponent();

        controls = Services.Provider.GetService<IControls<P>>()!;
        controls.Container = Controls;

        this.contract = contract;

        content = typeof(P).GetCustomAttribute<ProductContentAttribute>()?.Content ?? throw new Exception($"Использование FormProductPrice с типом {typeof(P).Name} возиожно только при условии наличия у этого типа атрибута ProductContentAttribute");
        excludePrice = typeof(P).GetCustomAttribute<ProductExcludingPriceAttribute>() != null;

        var productHeader = content switch
        {
            ProductContent.Materials => "Материал",
            ProductContent.Goods => "Товар",
            ProductContent.All => "Товар/материал",
            _ => throw new NotImplementedException()
        };

        product = controls.CreateDirectorySelectBox<Product>(x => x.ReferenceId, productHeader, select =>
            select
                .SetDataSource(GetProducts, DataRefreshMethod.Immediately)
                .DirectoryChanged(ProductChanged)
                .Required()
                .SetColumns(x => x.Code, columns)
                .EditorFitToSize()
                .SetHeaderWidth(120));

        amount = controls.CreateNumericTextBox(x => x.Amount, "Количество", text =>
            text
                .SetNumberDecimalDigits(3)
                .ValueChanged(AmountOrPriceChanged)
                .SetHeaderWidth(120)
                .DefaultAsValue()
                .EditorFitToSize());
        price = controls.CreateCurrencyTextBox(x => x.Price, "Цена", text =>
            text
                .ValueChanged(AmountOrPriceChanged)
                .SetHeaderWidth(120)
                .DefaultAsValue()
                .EditorFitToSize()
                .If(excludePrice, c => c.SetVisible(false)));
        cost = controls.CreateCurrencyTextBox(x => x.ProductCost, "Сумма", text =>
            text
                .ValueChanged(CostValueChanged)
                .SetHeaderWidth(120)
                .DefaultAsValue()
                .EditorFitToSize()
                .If(excludePrice, c => c.SetVisible(false)));
        tax = controls.CreateChoice<int>(x => x.Tax, "НДС%", choice =>
            choice
                .SetChoiceValues(Product.Taxes, true)
                .ChoiceChanged(TaxChanged)
                .SetHeaderWidth(120)
                .EditorFitToSize()
                .If(excludePrice, c => c.SetVisible(false)));
        taxValue = controls.CreateCurrencyTextBox(x => x.TaxValue, "НДС", text =>
            text
                .ValueChanged(TaxValueChanged)
                .SetHeaderWidth(120)
                .DefaultAsValue()
                .EditorFitToSize()
                .If(excludePrice, c => c.SetVisible(false)));
        fullCost = controls.CreateCurrencyTextBox(x => x.FullCost, "Всего с НДС", text =>
            text
                .SetHeaderWidth(120)
                .DefaultAsValue()
                .EditorFitToSize()
                .If(excludePrice, c => c.SetVisible(false)));

        Height = 330;

        if (excludePrice) 
        {
            Height -= 160;
        }
    }

    public bool Create(P productPrice)
    {
        UpdateControls();
        if (ShowDialog() == DialogResult.OK)
        {
            SaveControlData(productPrice);
            return true;
        }

        return false;
    }

    public bool Edit(P productPrice)
    {
        UpdateControls();
        controls.Initialize(productPrice);

        if (ShowDialog() == DialogResult.OK)
        {
            SaveControlData(productPrice);
            return true;
        }

        return false;
    }

    private void SaveControlData(P dest)
    {
        dest.ReferenceId = product.SelectedItem?.Id ?? Guid.Empty;
        dest.Amount = amount.NumericValue.GetValueOrDefault();
        dest.Price = price.NumericValue.GetValueOrDefault();
        dest.ProductCost = cost.NumericValue.GetValueOrDefault();
        dest.Tax = tax.SelectedValue.GetValueOrDefault();
        dest.TaxValue = taxValue.NumericValue.GetValueOrDefault();
        dest.FullCost = fullCost.NumericValue.GetValueOrDefault();
        dest.SetProductInfo(product.SelectedItem);
        if (dest is IDiscriminator discriminator && product.SelectedItem != null)
        {
            discriminator.TableName = product.SelectedItem.GetType().Name.Underscore();
        }
    }

    private void UpdateControls()
    {
        bool isTaxPayer = contract != null && contract.TaxPayer;
        tax.SetEnabled(isTaxPayer);
        taxValue.SetEnabled(isTaxPayer);
        if (isTaxPayer)
        {
            tax.SelectedValue = 20;
        }
    }

    private void AmountOrPriceChanged(decimal _) => cost.NumericValue = price.NumericValue * amount.NumericValue;

    private void CostValueChanged(decimal _) => TaxChanged();

    private void TaxChanged(int? _) => TaxChanged();

    private void TaxValueChanged(decimal value) => fullCost.NumericValue = cost.NumericValue + taxValue.NumericValue;

    private void TaxChanged() => taxValue.NumericValue = cost.NumericValue * tax.SelectedValue / 100;

    private void ProductChanged(Product? product)
    {
        if (excludePrice)
        {
            return;
        }

        decimal avgPrice = 0;

        if (product is Material material)
        {
            var repo = Services.Provider.GetService<IMaterialRepository>();
            if (repo != null)
            {
                avgPrice = repo.GetAveragePrice(material);
            }
        }
        else if (product is Goods goods && contract != null)
        {
            var repo = Services.Provider.GetService<IContractApplicationRepository>();
            var priceRepo = Services.Provider.GetService<IPriceApprovalRepository>();
            if (repo != null && priceRepo != null)
            {
                var apps = repo.GetCurrents(contract);
                foreach (var app in apps)
                {
                    var p = priceRepo.GetPrice(app, goods);
                    if (p != null)
                    {
                        avgPrice = p.Price;
                        break;
                    }
                }
            }
        }

        price.NumericValue = avgPrice == 0 ? (product?.Price ?? 0) : avgPrice;
    }

    private IEnumerable<Product> GetProducts()
    {
        IMaterialRepository? materials = null;
        IGoodsRepository? goods = null;

        if (content == ProductContent.Materials || content == ProductContent.All)
        {
            materials = Services.Provider.GetService<IMaterialRepository>();
        }

        if (content == ProductContent.Goods || content == ProductContent.All)
        {
            goods = Services.Provider.GetService<IGoodsRepository>();
        }

        if (materials == null)
        {
            return goods!.GetAllValid(callback: query => query.OrderByDesc("is_folder").OrderBy("code"));
        }
        else if (goods == null)
        {
            return materials!.GetAllValid();
        }
        else
        {
            return materials!.GetAllValid().OfType<Product>().Union(goods!.GetAllValid());
        }
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (product.SelectedItem == null)
        {
            MessageBox.Show("Выберите изделие/материал", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
        else if (amount.NumericValue <= 0)
        {
            MessageBox.Show("Количество должно быть больше 0", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
