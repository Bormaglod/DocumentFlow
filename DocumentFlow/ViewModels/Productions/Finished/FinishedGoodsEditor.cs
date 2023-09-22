//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.09.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(FinishedGoods), RepositoryType = typeof(IFinishedGoodsRepository))]
public partial class FinishedGoodsEditor : EditorPanel, IFinishedGoodsEditor, IDocumentEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public FinishedGoodsEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        Finished.OrganizationId = services.GetRequiredService<IOrganizationRepository>().GetMain().Id;
    }

    public Guid? OwnerId
    {
        get => Finished.OwnerId;
        set => Finished.OwnerId = value;
    }

    protected FinishedGoods Finished { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = Finished.Id != Guid.Empty;
        if (method == ConstructDataMethod.Create && Finished.OwnerId != null)
        {
            var lots = services.GetRequiredService<IProductionLotRepository>();
            var lot = lots.Get(Finished.OwnerId.Value, ignoreAdjustedQuery: true);

            Finished.GoodsId = lot.GoodsId;
            Finished.Quantity = lot.Quantity - lots.GetFinishedGoods(lot);

            UpdatePrices(lot.CalculationId);
        }
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(FinishedGoods.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(FinishedGoods.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(FinishedGoods.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        selectLot.DataBindings.Add(nameof(selectLot.SelectedItem), DataContext, nameof(FinishedGoods.OwnerId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        selectGoods.DataBindings.Add(nameof(selectGoods.SelectedItem), DataContext, nameof(FinishedGoods.GoodsId), false, DataSourceUpdateMode.OnPropertyChanged);
        textQuantity.DataBindings.Add(nameof(textQuantity.DecimalValue), DataContext, nameof(FinishedGoods.Quantity), false, DataSourceUpdateMode.OnPropertyChanged);
        textPrice.DataBindings.Add(nameof(textPrice.DecimalValue), DataContext, nameof(FinishedGoods.Price), true, DataSourceUpdateMode.OnPropertyChanged, 0m);
        textProductCost.DataBindings.Add(nameof(textProductCost.DecimalValue), DataContext, nameof(FinishedGoods.ProductCost), true, DataSourceUpdateMode.OnPropertyChanged, 0m);
    }

    protected override void CreateDataSources()
    {
        var lots = services.GetRequiredService<IProductionLotRepository>();
        var currents = Array.Empty<ProductionLot>();
        if (Finished.OwnerId != null)
        {
            currents = new ProductionLot[] { lots.Get(Finished.OwnerId.Value) };
        }

        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        selectLot.DataSource = services
            .GetRequiredService<IProductionLotRepository>()
            .GetActiveLots()
            .Union(currents);
        selectGoods.DataSource = services
            .GetRequiredService<IGoodsRepository>()
            .GetListExisting();
    }

    private void UpdatePrices(Guid calculationId)
    {
        var calc = services
            .GetRequiredService<ICalculationRepository>()
            .Get(calculationId, userDefindedQuery: false, ignoreAdjustedQuery: true);
        Finished.Price = calc.CostPrice;
        Finished.ProductCost = calc.CostPrice * textQuantity.DecimalValue;
    }

    private void SelectLot_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowEditor<IProductionLotEditor>(e.Document);
    }

    private void SelectGoods_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowEditor<IGoodsEditor>(e.Document);
    }

    private void SelectLot_DocumentDialogColumns(object sender, DocumentDialogColumnsEventArgs e)
    {
        ModelsHelper.CreateProductionLotColumns(e.Columns, true);
    }

    private void SelectLot_SelectedItemChanged(object sender, EventArgs e)
    {
        selectGoods.EnabledEditor = selectLot.SelectedItem == Guid.Empty;
    }

    private void SelectGoods_SelectedItemChanged(object sender, EventArgs e)
    {
        if (selectGoods.SelectedItem != Guid.Empty)
        {
            var goods = services
                .GetRequiredService<IGoodsRepository>()
                .Get(selectGoods.SelectedItem, userDefindedQuery: false, ignoreAdjustedQuery: true);
            if (goods.MeasurementId != null)
            {
                var meas = services
                    .GetRequiredService<IMeasurementRepository>()
                    .Get(goods.MeasurementId.Value);

                textQuantity.Suffix = meas.Abbreviation ?? meas.ItemName ?? meas.Code;
                textQuantity.ShowSuffix = true;

                return;
            }
        }

        textQuantity.ShowSuffix = false;
    }

    private void SelectLot_UserDocumentModified(object sender, DocumentSelectedEventArgs e)
    {
        var lot = (ProductionLot)e.Document;
        Finished.GoodsId = lot.GoodsId;
        Finished.Quantity = lot.FreeQuantity != 0 ? lot.FreeQuantity : lot.Quantity;
        UpdatePrices(lot.CalculationId);
    }

    private void SelectGoods_UserDocumentModified(object sender, DocumentSelectedEventArgs e)
    {
        var goods = (Goods)e.Document;
        if (goods.CalculationId != null)
        {
            UpdatePrices(goods.CalculationId.Value);
        }
    }
}
