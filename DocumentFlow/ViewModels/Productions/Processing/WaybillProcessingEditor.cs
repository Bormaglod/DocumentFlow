//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2023
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging;

using DocumentFlow.Controls;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Events;
using DocumentFlow.Data.Models;
using DocumentFlow.Dialogs.Interfaces;
using DocumentFlow.Messages;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(WaybillProcessing), RepositoryType = typeof(IWaybillProcessingRepository))]
public partial class WaybillProcessingEditor : EditorPanel, IWaybillProcessingEditor
{
    private readonly IServiceProvider services;

    public WaybillProcessingEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();

        this.services = services;

        gridContent.AddCommand("Заполнить", Properties.Resources.icons8_todo_16, PopulateProcessingRows);
        gridContent.RegisterDialog<IProductPriceDialog, WaybillProcessingPrice>();

        Waybill.OrganizationId = services.GetRequiredService<IOrganizationRepository>().GetMain().Id;
    }

    protected WaybillProcessing Waybill { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = Waybill.Id != Guid.Empty;
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(WaybillProcessing.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(WaybillProcessing.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(WaybillProcessing.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        selectContractor.DataBindings.Add(nameof(selectContractor.SelectedItem), DataContext, nameof(WaybillProcessing.ContractorId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        selectContract.DataBindings.Add(nameof(selectContract.SelectedItem), DataContext, nameof(WaybillProcessing.ContractId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        selectOrder.DataBindings.Add(nameof(selectOrder.SelectedItem), DataContext, nameof(WaybillProcessing.OwnerId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        textWaybillNumber.DataBindings.Add(nameof(textWaybillNumber.TextValue), DataContext, nameof(WaybillProcessing.WaybillNumber), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        dateWaybill.DataBindings.Add(nameof(dateWaybill.DateTimeValue), DataContext, nameof(WaybillProcessing.WaybillDate), true, DataSourceUpdateMode.OnPropertyChanged, DateTime.MinValue);
    }

    protected override void CreateDataSources()
    {
        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        selectContractor.DataSource = services.GetRequiredService<IContractorRepository>().GetSuppliers();
        selectContract.DataSource = services
            .GetRequiredService<IContractRepository>()
            .GetList(callback: q => q.Where("id", Waybill.ContractId));
        selectOrder.DataSource = services
            .GetRequiredService<IProductionOrderRepository>()
            .GetList(callback: q => q.Where("id", Waybill.OwnerId));

        gridContent.DataSource = Waybill.Prices;
    }

    private void PopulateProcessingRows()
    {
        if (Waybill.OwnerId != null)
        {
            if (MessageBox.Show("Перед заполнением таблица будет очищена. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            Waybill.Prices.Clear();

            var rows = services
                .GetRequiredService<IProductionOrderRepository>()
                .GetOnlyGivingMaterials<WaybillProcessingPrice>(Waybill.OwnerId.Value);
            foreach (var item in rows)
            {
                Waybill.Prices.Add(item);
            }
        }
    }

    private void SelectOrder_DataSourceOnLoad(object sender, DataSourceLoadEventArgs e)
    {
        e.Values = services
            .GetRequiredService<IProductionOrderRepository>()
            .GetListUserDefined(callback: q => q
                .WhereFalse("production_order.deleted")
                .WhereTrue("production_order.carried_out")
                .WhereFalse("production_order.closed"));
    }

    private void SelectContractor_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IContractorEditor), e.Document));
    }

    private void SelectContract_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IContractEditor), e.Document));
    }

    private void SelectOrder_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IProductionOrderEditor), e.Document));
    }

    private void SelectOrder_DocumentDialogColumns(object sender, DocumentDialogColumnsEventArgs e)
    {
        ModelsHelper.CreateProductinOrderColumns(e.Columns);
    }

    private void SelectContract_DataSourceOnLoad(object sender, DataSourceLoadEventArgs e)
    {
        if (selectContractor.SelectedItem != Guid.Empty)
        {
            e.Values = services
                .GetRequiredService<IContractRepository>()
                .GetSuppliers(selectContractor.SelectedItem);
        }
        else
        {
            e.Values = null;
        }
    }

    private void SelectContractor_UserDocumentModified(object sender, DocumentChangedEventArgs e)
    {
        if (e.NewDocument != e.OldDocument)
        {
            Waybill.ContractId = Guid.Empty;
        }
    }

    private void GridContent_ConfirmGeneratingColumn(object sender, ConfirmGeneratingColumnArgs e)
    {
        e.Cancel = e.MappingName switch
        {
            nameof(ProductPrice.Price) or nameof(ProductPrice.ProductCost) or nameof(ProductPrice.Tax) or nameof(ProductPrice.TaxValue) or nameof(ProductPrice.FullCost) => true,
            _ => false,
        };
    }

    private void SelectContractor_DeleteButtonClick(object sender, EventArgs e)
    {
        Waybill.ContractId = Guid.Empty;
    }

    private void GridContent_DialogParameters(object sender, DialogParametersEventArgs e)
    {
        if (selectContract.SelectedItem != Guid.Empty)
        {
            ((IProductPriceDialog)e.Dialog).Contract = (Contract)selectContract.SelectedDocument;
        }
    }
}
