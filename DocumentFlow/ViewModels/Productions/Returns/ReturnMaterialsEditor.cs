//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Events;
using DocumentFlow.Data.Models;
using DocumentFlow.Dialogs.Interfaces;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(ReturnMaterials), RepositoryType = typeof(IReturnMaterialsRepository))]
public partial class ReturnMaterialsEditor : EditorPanel, IReturnMaterialsEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public ReturnMaterialsEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        gridContent.AddCommand("Заполнить", Properties.Resources.icons8_todo_16, PopulateMaterialRows);
        gridContent.RegisterDialog<IMaterialQuantityDialog, ReturnMaterialsRows>();

        Returns.OrganizationId = services.GetRequiredService<IOrganizationRepository>().GetMain().Id;
    }

    protected ReturnMaterials Returns { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = Returns.Id != Guid.Empty;
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(ReturnMaterials.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(ReturnMaterials.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(ReturnMaterials.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        selectContractor.DataBindings.Add(nameof(selectContractor.SelectedItem), DataContext, nameof(ReturnMaterials.ContractorId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        selectContract.DataBindings.Add(nameof(selectContract.SelectedItem), DataContext, nameof(ReturnMaterials.ContractId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        selectOrder.DataBindings.Add(nameof(selectOrder.SelectedItem), DataContext, nameof(ReturnMaterials.OwnerId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
    }

    protected override void CreateDataSources()
    {
        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        selectContractor.DataSource = services.GetRequiredService<IContractorRepository>().GetSuppliers();
        selectContract.DataSource = services
            .GetRequiredService<IContractRepository>()
            .GetList(callback: q => q.Where("id", Returns.ContractId));
        selectOrder.DataSource = services
            .GetRequiredService<IProductionOrderRepository>()
            .GetList(callback: q => q.Where("id", Returns.OwnerId));

        gridContent.DataSource = Returns.Materials;
    }

    private void PopulateMaterialRows()
    {
        if (Returns.OwnerId != null)
        {
            if (MessageBox.Show("Перед заполнением таблица будет очищена. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            Returns.Materials.Clear();

            var rows = services
                .GetRequiredService<IWaybillProcessingRepository>()
                .GetReturnMaterials(Returns.OwnerId.Value);

            foreach (var item in rows)
            {
                Returns.Materials.Add(item);
            }
        }
    }

    private void SelectContractor_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowEditor<IContractorEditor>(e.Document);
    }

    private void SelectContractor_UserDocumentModified(object sender, DocumentChangedEventArgs e)
    {
        if (e.NewDocument != e.OldDocument)
        {
            Returns.ContractId = Guid.Empty;
        }
    }

    private void SelectContract_DataSourceOnLoad(object sender, DataSourceLoadEventArgs e)
    {
        if (selectContractor.SelectedItem != Guid.Empty)
        {
            e.Values = services
                .GetRequiredService<IContractRepository>()
                .GetSuppliers(selectContractor.SelectedItem);
        }
    }

    private void SelectContract_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowEditor<IContractEditor>(e.Document);
    }

    private void SelectOrder_DataSourceOnLoad(object sender, DataSourceLoadEventArgs e)
    {
        var repo = services.GetRequiredService<IProductionOrderRepository>();
        if (Returns.ContractorId != null && Returns.ContractId != null)
        {
            e.Values = repo.GetWithReturnMaterial(Returns.ContractorId.Value, Returns.ContractId.Value);
        }
    }

    private void SelectOrder_DocumentDialogColumns(object sender, DocumentDialogColumnsEventArgs e)
    {
        ModelsHelper.CreateProductinOrderColumns(e.Columns);
    }

    private void SelectOrder_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowEditor<IProductionOrderEditor>(e.Document);
    }

    private void SelectContractor_DeleteButtonClick(object sender, EventArgs e)
    {
        Returns.ContractId = Guid.Empty;
    }
}
