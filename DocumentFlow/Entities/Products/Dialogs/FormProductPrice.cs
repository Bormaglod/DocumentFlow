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
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Products.Core;
using DocumentFlow.Entities.Productions.Order;

using Humanizer;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace DocumentFlow.Entities.Products.Dialogs;

public partial class FormProductPrice<P> : Form
    where P : ProductPrice, new()
{
    private readonly DfDirectorySelectBox<Product> product;
    private readonly DfDirectorySelectBox<Calculation>? calc;
    private readonly DfNumericTextBox amount;
    private readonly DfCurrencyTextBox price;
    private readonly DfCurrencyTextBox cost;
    private readonly DfChoice<int> tax;
    private readonly DfCurrencyTextBox tax_value;
    private readonly DfCurrencyTextBox full_cost;

    private readonly Contract? contract;
    private readonly bool excludePrice;

    protected FormProductPrice(Contract? contract, bool calculationSelect)
    {
        InitializeComponent();

        this.contract = contract;

        var attr = typeof(P).GetCustomAttribute<ProductContentAttribute>();
        if (attr == null)
        {
            throw new Exception($"Использование FormProductPrice с типом {typeof(P).Name} возиожно только при условии наличия у этого типа атрибута ProductContentAttribute");
        }

        excludePrice = typeof(P).GetCustomAttribute<ProductExcludingPriceAttribute>() != null;

        var productHeader = attr.Content switch
        {
            ProductContent.Materials => "Материал",
            ProductContent.Goods => "Товар",
            ProductContent.All => "Товар/материал",
            _ => throw new NotImplementedException()
        };

        product = new("reference_id", productHeader, 120) 
        { 
            Required = true,
            NameColumn = "code",
            Columns = new Dictionary<string, string> 
            {
                ["code"] = "Артикул",
                ["item_name"] = "Наименование"
            },
            TabIndex = 1,
            RefreshMethod = DataRefreshMethod.Immediately
        };

        amount = new("amount", "Количество", 120) { DefaultAsNull = false, NumberDecimalDigits = 3, TabIndex = 3 };
        price = new("price", "Цена", 120) { DefaultAsNull = false, TabIndex = 4, Visible = !excludePrice };
        cost = new("product_cost", "Сумма", 120) { DefaultAsNull = false, TabIndex = 5, Visible = !excludePrice };
        tax = new("tax", "НДС%", 120) { TabIndex = 6, Visible = !excludePrice };
        tax_value = new("tax_value", "НДС", 120) { DefaultAsNull = false, TabIndex = 7, Visible = !excludePrice };
        full_cost = new("full_cost", "Всего с НДС", 120) { DefaultAsNull = false, TabIndex = 8, Visible = !excludePrice };

        var controls = new List<Control>() 
        { 
            full_cost,
            tax_value,
            tax,
            cost,
            price,
            amount,
            product
        };

        if (calculationSelect)
        {
            calc = new DfDirectorySelectBox<Calculation>("calculation_id", "Калькуляция", 120) { Required = true, TabIndex = 2 };
            calc.SetDataSource(() =>
            {
                if (product.SelectedItem != null)
                {
                    var calcRepo = Services.Provider.GetService<ICalculationRepository>();
                    return calcRepo!.GetByOwner(
                        calc.SelectedItem?.id, 
                        product.SelectedItem.id,
                        useBaseQuery: true,
                        callback: q => q
                            .Select("calculation.{id, code}")
                            .SelectRaw("calculation.code || ' от ' || calculation.date_approval as item_name")
                            .WhereRaw("calculation.state = 'approved'::calculation_state")
                            .OrderBy("calculation.code"));
                }

                return Array.Empty<Calculation>();
            });

            controls.Insert(6, calc);
        }

        tax.SetChoiceValues(Product.Taxes, true);

        product.SetDataSource(() =>
        {
            IMaterialRepository? materials = null;
            IGoodsRepository? goods = null;

            if (attr.Content == ProductContent.Materials || attr.Content == ProductContent.All)
            {
                materials = Services.Provider.GetService<IMaterialRepository>();
            }

            if (attr.Content == ProductContent.Goods || attr.Content == ProductContent.All)
            {
                goods = Services.Provider.GetService<IGoodsRepository>();
            }

            if (materials == null)
            {
                return goods!.GetAllValid(callback: query => query.OrderByDesc("is_folder").OrderBy("code"));
            }
            else if (goods == null)
            {
                return materials!.GetAllMaterials();
            }
            else
            {
                return materials!.GetAllMaterials().OfType<Product>().Union(goods!.GetAllValid());
            }
        });

        product.ValueChanged += (sender, arg) =>
        {
            if (excludePrice)
            {
                return;
            }

            decimal avgPrice = 0;

            if (product.SelectedItem is Material material)
            {
                var repo = Services.Provider.GetService<IMaterialRepository>();
                if (repo != null)
                {
                    avgPrice = repo.GetAveragePrice(material);
                }
            }
            else if (product.SelectedItem is Goods goods && contract != null)
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
                            avgPrice = p.price;
                            break;
                        }
                    }
                }
            }

            price.Value = avgPrice == 0 ? (product.SelectedItem?.price ?? 0) : avgPrice;

            calc?.RefreshDataSource();
        };

        amount.ValueChanged += (sender, arg) => cost.Value = price.NumericValue * amount.NumericValue;
        price.ValueChanged += (sender, arg) => cost.Value = price.NumericValue * amount.NumericValue;
        cost.ValueChanged += (sender, arg) => tax_value.Value = cost.NumericValue * tax.ChoiceValue / 100;
        tax.ValueChanged += (sender, arg) => tax_value.Value = cost.NumericValue * tax.ChoiceValue / 100;
        tax_value.ValueChanged += (sender, arg) => full_cost.Value = cost.NumericValue + tax_value.NumericValue;

        Controls.AddRange(controls.ToArray());

        Height = 362;
        if (!calculationSelect)
        {
            Height -= 32;
        }

        if (excludePrice) 
        {
            Height -= 160;
        }
    }

    public static DialogResult Create(P productPrice, Contract? contract, bool calculationSelect = false)
    {
        FormProductPrice<P> form = new(contract, calculationSelect);
        form.UpdateControls();
        if (form.ShowDialog() == DialogResult.OK)
        {
            form.SaveControlData(productPrice);
            return DialogResult.OK;
        }

        return DialogResult.Cancel;
    }

    public static DialogResult Edit(P productPrice, Contract? contract, bool calculationSelect = false)
    {
        FormProductPrice<P> form = new(contract, calculationSelect);
        form.UpdateControls();
        form.product.Value = productPrice.reference_id;
        form.amount.Value = productPrice.amount;
        form.price.Value = productPrice.price;
        form.cost.Value = productPrice.product_cost;
        form.tax_value.Value = productPrice.tax_value;
        form.full_cost.Value = productPrice.full_cost;
        if (calculationSelect && form.calc != null && productPrice is ProductionOrderPrice pop)
        {
            form.calc.Value = pop.calculation_id;
        }

        if (form.ShowDialog() == DialogResult.OK)
        {
            form.SaveControlData(productPrice);
            return DialogResult.OK;
        }

        return DialogResult.Cancel;
    }

    private void SaveControlData(P dest)
    {
        dest.reference_id = product.SelectedItem?.id ?? Guid.Empty;
        dest.amount = amount.NumericValue.GetValueOrDefault();
        dest.price = price.NumericValue.GetValueOrDefault();
        dest.product_cost = cost.NumericValue.GetValueOrDefault();
        dest.tax = tax.ChoiceValue.GetValueOrDefault();
        dest.tax_value = tax_value.NumericValue.GetValueOrDefault();
        dest.full_cost = full_cost.NumericValue.GetValueOrDefault();
        dest.SetProductInfo(product.SelectedItem);
        if (dest is IDiscriminator discriminator && product.SelectedItem != null)
        {
            discriminator.TableName = product.SelectedItem.GetType().Name.Underscore();
        }

        if (calc != null && calc.SelectedItem != null && dest is ProductionOrderPrice pop)
        {
            pop.SetCalculation(calc.SelectedItem);
        }
    }

    private void UpdateControls()
    {
        bool isTaxPayer = contract != null && contract.tax_payer;
        tax.Enabled = isTaxPayer;
        tax_value.Enabled = isTaxPayer;
        if (isTaxPayer)
        {
            tax.Value = 20;
        }
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (product.Value == null)
        {
            MessageBox.Show("Выберите изделие/материал", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
        else if (calc != null && calc.Value == null)
        {
            MessageBox.Show("Выберите калькуляцию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
        else if (amount.NumericValue <= 0)
        {
            MessageBox.Show("Количество должно быть больше 0", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }
}
