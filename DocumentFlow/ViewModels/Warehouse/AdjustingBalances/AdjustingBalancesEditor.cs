//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Events;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(AdjustingBalances), RepositoryType = typeof(IAdjustingBalancesRepository))]
public partial class AdjustingBalancesEditor : EditorPanel, IAdjustingBalancesEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public AdjustingBalancesEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        Balance.OrganizationId = services.GetRequiredService<IOrganizationRepository>().GetMain().Id;
    }

    protected AdjustingBalances Balance { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = Balance.Id != Guid.Empty;
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(AdjustingBalances.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(AdjustingBalances.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(AdjustingBalances.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        selectMaterial.DataBindings.Add(nameof(selectMaterial.SelectedItem), DataContext, nameof(AdjustingBalances.MaterialId), false, DataSourceUpdateMode.OnPropertyChanged);
        textQuantity.DataBindings.Add(nameof(textQuantity.DecimalValue), DataContext, nameof(AdjustingBalances.Quantity), false, DataSourceUpdateMode.OnPropertyChanged);
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
