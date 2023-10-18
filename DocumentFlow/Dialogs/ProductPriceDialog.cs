//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Events;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Data.Tools;
using DocumentFlow.Dialogs.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace DocumentFlow.Dialogs;

public partial class ProductPriceDialog : Form, IProductPriceDialog
{
    private bool includePrice;
    private ProductContent content;
    private bool canCalculate = false;
    private IProductCalculation? productCalculation = null;

    private bool withCalculation;
    private bool withLot;

    private readonly IServiceProvider services;

    public ProductPriceDialog(IServiceProvider services)
    {
        InitializeComponent();

        this.services = services;

        choiceVat.From(Product.Taxes);

        selectProduct.SetColumns<Product>(nameof(Product.Code), new Dictionary<string, string>
        {
            ["Code"] = "Артикул",
            ["ItemName"] = "Наименование"
        });
    }

    public Contract? Contract { get; set; }

    public bool Create<T>([MaybeNullWhen(false)] out T product) where T : new()
    {
        UpdateControls(typeof(T));

        canCalculate = true;
        if (ShowDialog() == DialogResult.OK)
        {
            if (!int.TryParse(choiceVat.ChoiceValue, out var vat))
            {
                vat = 0;
            }

            product = new T();
            if (product is ProductPrice price)
            {
                price.ReferenceId = selectProduct.SelectedItem;
                price.Amount = textAmount.DecimalValue;
                price.Price = textPrice.DecimalValue;
                price.ProductCost = textSumma.DecimalValue;
                price.Tax = vat;
                price.TaxValue = textVat.DecimalValue;
                price.FullCost = textFullSumma.DecimalValue;

                price.SetProductInfo((Product)selectProduct.SelectedDocument);
            }

            if (product is IProductCalculation calculation)
            {
                calculation.SetCalculation((Calculation)selectCalc.SelectedDocument);
            }

            if (product is IProductionLotSupport lot && selectLot.SelectedItem != Guid.Empty)
            {
                lot.LotId = selectLot.SelectedItem;
            }

            return true;
        }

        product = default;
        return false;
    }

    public bool Edit<T>(T product)
    {
        if (product is not ProductPrice price)
        {
            return false;
        }

        UpdateControls(typeof(T));

        if (product is IProductCalculation calculation)
        {
            productCalculation = calculation;
        }

        if (product is IProductionLotSupport lot && lot.LotId != null)
        {
            selectLot.DataSource = new ProductionLot[]
            {
                services.GetRequiredService<IProductionLotRepository>().Get(lot.LotId.Value)
            };

            selectLot.SelectedItem = lot.LotId.Value;
        }

        selectProduct.SelectedItem = price.ReferenceId;
        textAmount.DecimalValue = price.Amount;
        textPrice.DecimalValue = price.Price;
        textSumma.DecimalValue = price.ProductCost;
        choiceVat.ChoiceValue = price.Tax.ToString();
        textVat.DecimalValue = price.TaxValue;
        textFullSumma.DecimalValue = price.FullCost;

        if (productCalculation != null)
        {
            selectCalc.SelectedItem = productCalculation.CalculationId;
        }

        canCalculate = true;
        if (ShowDialog() == DialogResult.OK)
        {
            price.ReferenceId = selectProduct.SelectedItem;
            price.Amount = textAmount.DecimalValue;
            price.Price = textPrice.DecimalValue;
            price.ProductCost = textSumma.DecimalValue;
            price.Tax = Convert.ToInt32(choiceVat.ChoiceValue);
            price.TaxValue = textVat.DecimalValue;
            price.FullCost = textFullSumma.DecimalValue;
            price.SetProductInfo((Product)selectProduct.SelectedDocument);

            productCalculation?.SetCalculation((Calculation)selectCalc.SelectedDocument);

            if (product is IProductionLotSupport _lot)
            {
                _lot.LotId = selectLot.SelectedItem == Guid.Empty ? null : selectLot.SelectedItem;
            }

            return true;
        }

        return false;
    }

    private void UpdateControls(Type productType)
    {
        withCalculation = productType.GetInterface(nameof(IProductCalculation)) != null;
        withLot = productType.GetInterface(nameof(IProductionLotSupport)) != null;

        content = productType.GetCustomAttribute<ProductContentAttribute>()?.Content ?? throw new Exception($"Использование ProductPriceDialog с типом {productType.Name} возможно только при условии наличия у этого типа атрибута ProductContentAttribute");
        selectProduct.Header = content switch
        {
            ProductContent.Materials => "Материал",
            ProductContent.Goods => "Товар",
            ProductContent.All => "Товар/материал",
            _ => throw new NotImplementedException()
        };

        includePrice = productType.GetCustomAttribute<ProductExcludingPriceAttribute>() == null;

        textPrice.Visible = includePrice;
        textSumma.Visible = includePrice;
        choiceVat.Visible = includePrice;
        textVat.Visible = includePrice;
        textFullSumma.Visible = includePrice;

        selectCalc.Visible = withCalculation;
        selectLot.Visible = withLot;

        int k = 0;
        if (!includePrice)
        {
            k += 5;
        }

        if (!withCalculation)
        {
            k++;
        }

        if (!withLot)
        {
            k++;
        }

        Height -= k * 32;
        MinimumSize = new Size(350, 387 - k * 32);

        selectProduct.DataSource = GetProducts();

        bool isTaxPayer = Contract != null && Contract.TaxPayer;
        choiceVat.Enabled = isTaxPayer;
        textVat.Enabled = isTaxPayer;
        if (isTaxPayer)
        {
            choiceVat.ChoiceValue = "20";
        }
    }

    private IEnumerable<Product> GetProducts()
    {
        IMaterialRepository? materials = null;
        IGoodsRepository? goods = null;

        if (content == ProductContent.Materials || content == ProductContent.All)
        {
            materials = services.GetRequiredService<IMaterialRepository>();
        }

        if (content == ProductContent.Goods || content == ProductContent.All)
        {
            goods = services.GetRequiredService<IGoodsRepository>();
        }

        IEnumerable<Product> prods;
        if (materials == null)
        {
            prods = goods!.GetListExisting();
        }
        else if (goods == null)
        {
            prods = materials!.GetListExisting();
        }
        else
        {
            prods = materials!.GetListExisting().OfType<Product>().Union(goods!.GetListExisting());
        }

        return prods
            .OrderByDescending(x => x.IsFolder)
            .ThenBy(x => x.ItemName);
    }

    private void UpdateTaxValue()
    {
        if (canCalculate)
        {
            if (!int.TryParse(choiceVat.ChoiceValue, out var vat))
            {
                vat = 0;
            }

            textVat.DecimalValue = textSumma.DecimalValue * vat / 100;
        }
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        string text = string.Empty;

        if (selectProduct.SelectedItem == Guid.Empty)
        {
            text = "Выберите изделие/материал";
        }
        else if (selectCalc.SelectedItem == Guid.Empty && withCalculation)
        {
            text = "Выберите калькуляцию";
        }
        else if (textAmount.DecimalValue <= 0)
        {
            text = "Количество должно быть больше 0";
        }

        if (!string.IsNullOrEmpty(text))
        {
            MessageBox.Show(text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }
    }

    private void SelectProduct_DocumentSelectedChanged(object sender, DocumentChangedEventArgs e)
    {
        if (!includePrice)
        {
            return;
        }

        decimal avgPrice = 0;

        if (e.NewDocument is Material material)
        {
            var repo = services.GetRequiredService<IMaterialRepository>();
            if (repo != null)
            {
                avgPrice = repo.GetAveragePrice(material);
            }
        }
        else if (e.NewDocument is Goods goods && Contract != null)
        {
            var repo = services.GetRequiredService<IContractApplicationRepository>();
            var priceRepo = services.GetRequiredService<IPriceApprovalRepository>();
            if (repo != null && priceRepo != null)
            {
                var apps = repo.GetCurrents(Contract);
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

        textPrice.DecimalValue = avgPrice == 0 ? ((Product)e.NewDocument).Price : avgPrice;
    }

    private void AmountOrPriceChanged(object sender, EventArgs e)
    {
        if (canCalculate)
        {
            textSumma.DecimalValue = textPrice.DecimalValue * textAmount.DecimalValue;
        }
    }

    private void CostOrTaxChanged(object sender, EventArgs e)
    {
        if (canCalculate)
        {
            textFullSumma.DecimalValue = textSumma.DecimalValue + textVat.DecimalValue;
        }

        UpdateTaxValue();
    }

    private void ChoiceVat_ChoiceValueChanged(object sender, EventArgs e)
    {
        UpdateTaxValue();
    }

    private void SelectProduct_SelectedItemChanged(object sender, EventArgs e)
    {
        var calcRepo = services.GetRequiredService<ICalculationRepository>();
        selectCalc.DataSource = calcRepo.GetByOwner(
            productCalculation?.CalculationId,
            selectProduct.SelectedItem,
            userDefindedQuery: true,
            callback: q => q
                .Select("calculation.{id, code}")
                .SelectRaw("calculation.code || ' от ' || calculation.date_approval as item_name")
                .WhereRaw("calculation.state = 'approved'::calculation_state")
                .OrderBy("calculation.code"));
    }

    private void SelectLot_DataSourceOnLoad(object sender, DataSourceLoadEventArgs e)
    {
        if (Contract?.OwnerId != null && selectProduct.SelectedItem != Guid.Empty)
        {
            var currents = Array.Empty<ProductionLot>();
            if (selectLot.SelectedItem != Guid.Empty)
            {
                currents = new ProductionLot[] { (ProductionLot)selectLot.SelectedDocument };
            }

            e.Values = selectLot.DataSource = services
                .GetRequiredService<IProductionLotRepository>()
                .GetActiveLots(Contract.OwnerId.Value, selectProduct.SelectedItem)
                .Union(currents);
        }
    }

    private void SelectLot_DocumentDialogColumns(object sender, DocumentDialogColumnsEventArgs e)
    {
        ModelsHelper.CreateProductionLotColumns(e.Columns, true);
    }

    private void SelectLot_UserDocumentModified(object sender, DocumentChangedEventArgs e)
    {
        var lot = (ProductionLot)e.NewDocument;
        textAmount.DecimalValue = lot.FreeQuantity;
    }
}
