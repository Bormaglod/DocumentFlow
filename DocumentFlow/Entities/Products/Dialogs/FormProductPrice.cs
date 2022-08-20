//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
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
//
//-----------------------------------------------------------------------

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

    private Contract? contract;

    protected FormProductPrice(Contract? contract, bool calculationSelect)
    {
        InitializeComponent();

        this.contract = contract;

        var attr = typeof(P).GetCustomAttribute<ProductContentAttribute>();
        if (attr == null)
        {
            throw new ArgumentNullException(nameof(attr));
        }

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
            TabIndex = 1
        };

        amount = new("amount", "Количество", 120) { DefaultAsNull = false, NumberDecimalDigits = 3, TabIndex = 2 };
        price = new("price", "Цена", 120) { DefaultAsNull = false, TabIndex = 2 };
        cost = new("product_cost", "Сумма", 120) { DefaultAsNull = false, TabIndex = 3 };
        tax = new("tax", "НДС%", 120) { TabIndex = 4 };
        tax_value = new("tax_value", "НДС", 120) { DefaultAsNull = false, TabIndex = 5 };
        full_cost = new("full_cost", "Всего с НДС", 120) { DefaultAsNull = false, TabIndex = 6 };

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
            calc = new DfDirectorySelectBox<Calculation>("calculation_id", "Калькуляция", 120) { Required = true, TabIndex = 7 };
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
                            .Select("calculation.id")
                            .SelectRaw("calculation.code || ' от ' || calculation.date_approval as item_name")
                            .WhereRaw("calculation.state = 'approved'::calculation_state")
                            .OrderBy("calculation.code"));
                }

                return Array.Empty<Calculation>();
            });

            controls.Insert(6, calc);
        }

        tax.SetChoiceValues(new Dictionary<int, string>
        {
            [0] = "Без НДС",
            [10] = "10%",
            [20] = "20%"
        }, true);

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
        }, true);

        product.ValueChanged += (sender, arg) =>
        {
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

            if (calc != null)
            {
                calc.RefreshDataSource();
            }
        };

        amount.ValueChanged += (sender, arg) => cost.Value = price.NumericValue * amount.NumericValue;
        price.ValueChanged += (sender, arg) => cost.Value = price.NumericValue * amount.NumericValue;
        cost.ValueChanged += (sender, arg) => tax_value.Value = cost.NumericValue * tax.ChoiceValue / 100;
        tax.ValueChanged += (sender, arg) => tax_value.Value = cost.NumericValue * tax.ChoiceValue / 100;
        tax_value.ValueChanged += (sender, arg) => full_cost.Value = cost.NumericValue + tax_value.NumericValue;

        Controls.AddRange(controls.ToArray());

        Height = calculationSelect ? 362 : 330;
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
            pop.calculation_id = calc.SelectedItem.id;
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
