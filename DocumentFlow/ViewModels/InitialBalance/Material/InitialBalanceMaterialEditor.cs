//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Events;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(InitialBalanceMaterial), RepositoryType = typeof(IInitialBalanceMaterialRepository))]
public partial class InitialBalanceMaterialEditor : EditorPanel, IInitialBalanceMaterialEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public InitialBalanceMaterialEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        InitialBalance.OrganizationId = services.GetRequiredService<IOrganizationRepository>().GetMain().Id;
    }

    protected InitialBalanceMaterial InitialBalance { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = InitialBalance.Id != Guid.Empty;
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(InitialBalanceMaterial.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(InitialBalanceMaterial.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(InitialBalanceMaterial.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        selectMaterial.DataBindings.Add(nameof(selectMaterial.SelectedItem), DataContext, nameof(InitialBalanceMaterial.ReferenceId), false, DataSourceUpdateMode.OnPropertyChanged);
        textAmount.DataBindings.Add(nameof(textAmount.DecimalValue), DataContext, nameof(InitialBalanceMaterial.Amount), false, DataSourceUpdateMode.OnPropertyChanged);
        textOperationSumma.DataBindings.Add(nameof(textOperationSumma.DecimalValue), DataContext, nameof(InitialBalanceMaterial.OperationSumma), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        selectMaterial.DataSource = services.GetRequiredService<IMaterialRepository>().GetListExisting();
    }

    private void SelectMaterial_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IMaterialBrowser>(e.Document);
    }
}
