//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.04.2023
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Productions.Order;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

using Humanizer;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Dialogs;

public partial class ProductionOrderPriceDialog : Form
{
    private readonly IDirectorySelectBoxControl<Product> product;
    private readonly IDirectorySelectBoxControl<Calculation> calc;
    private readonly INumericTextBoxControl amount;
    private readonly ICurrencyTextBoxControl price;
    private readonly ICurrencyTextBoxControl cost;
    private readonly IChoiceControl<int> tax;
    private readonly ICurrencyTextBoxControl taxValue;
    private readonly ICurrencyTextBoxControl fullCost;

    private readonly IControls<ProductionOrderPrice> controls;

    private readonly Contract? contract;

    private readonly IReadOnlyDictionary<string, string> columns = new Dictionary<string, string>
    {
        ["Code"] = "Артикул",
        ["ItemName"] = "Наименование"
    };

    public ProductionOrderPriceDialog(Contract? contract)
    {
        InitializeComponent();

        controls = Services.Provider.GetService<IControls<ProductionOrderPrice>>()!;
        controls.Container = Controls;

        this.contract = contract;

        product = controls.CreateDirectorySelectBox<Product>(x => x.ReferenceId, "Изделие", select =>
            select
                .SetDataSource(GetProducts, DataRefreshMethod.Immediately)
                .DirectoryChanged(ProductChanged)
                .Required()
                .SetColumns(x => x.Code, columns)
                .EditorFitToSize()
                .SetHeaderWidth(120));

        calc = controls.CreateDirectorySelectBox<Calculation>(x => x.CalculationId, "Калькуляция", select =>
            select
                .SetDataSource(GetCalculations, DataRefreshMethod.OnOpen)
                .Required()
                .SetHeaderWidth(120)
                .EditorFitToSize());

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
                .EditorFitToSize());
        cost = controls.CreateCurrencyTextBox(x => x.ProductCost, "Сумма", text =>
            text
                .ValueChanged(CostValueChanged)
                .SetHeaderWidth(120)
                .DefaultAsValue()
                .EditorFitToSize());
        tax = controls.CreateChoice<int>(x => x.Tax, "НДС%", choice =>
            choice
                .SetChoiceValues(Product.Taxes, true)
                .ChoiceChanged(TaxChanged)
                .SetHeaderWidth(120)
                .EditorFitToSize());
        taxValue = controls.CreateCurrencyTextBox(x => x.TaxValue, "НДС", text =>
            text
                .ValueChanged(TaxValueChanged)
                .SetHeaderWidth(120)
                .DefaultAsValue()
                .EditorFitToSize());
        fullCost = controls.CreateCurrencyTextBox(x => x.FullCost, "Всего с НДС", text =>
            text
                .SetHeaderWidth(120)
                .DefaultAsValue()
                .EditorFitToSize());

        Height = 362;
    }

    public bool Create(ProductionOrderPrice productPrice)
    {
        UpdateControls();
        if (ShowDialog() == DialogResult.OK)
        {
            SaveControlData(productPrice);
            return true;
        }

        return false;
    }

    public bool Edit(ProductionOrderPrice productPrice)
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

    private void SaveControlData(ProductionOrderPrice dest)
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

        if (calc.SelectedItem != null)
        {
            dest.SetCalculation(calc.SelectedItem);
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

        /*if (calc is IDataSourceControl dataSource)
        {
            dataSource.RefreshDataSource();
        }*/
    }

    private IEnumerable<Product> GetProducts()
    {
        var goods = Services.Provider.GetService<IGoodsRepository>();
        return goods!.GetListExisting(callback: query => query.OrderByDesc("is_folder").OrderBy("code"));
    }

    private IEnumerable<Calculation>? GetCalculations()
    {
        if (product.SelectedItem != null)
        {
            var calcRepo = Services.Provider.GetService<ICalculationRepository>();
            return calcRepo!.GetByOwner(
                calc.SelectedItem?.Id,
                product.SelectedItem.Id,
                useBaseQuery: true,
                callback: q => q
                    .Select("calculation.{id, code}")
                    .SelectRaw("calculation.code || ' от ' || calculation.date_approval as item_name")
                    .WhereRaw("calculation.state = 'approved'::calculation_state")
                    .OrderBy("calculation.code"));
        }

        return null;
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (product.SelectedItem == null)
        {
            MessageBox.Show("Выберите изделие/материал", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
        else if (calc != null && calc.SelectedItem == null)
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
