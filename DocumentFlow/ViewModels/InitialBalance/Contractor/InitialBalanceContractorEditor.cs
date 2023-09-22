﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Events;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(InitialBalanceContractor), RepositoryType = typeof(IInitialBalanceContractorRepository))]
public partial class InitialBalanceContractorEditor : EditorPanel, IInitialBalanceContractorEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public InitialBalanceContractorEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        InitialBalance.OrganizationId = services
            .GetRequiredService<IOrganizationRepository>()
            .GetMain()
            .Id;

        choiceDebt.FromEnum<DebtType>(KeyGenerationMethod.EnumValue);
    }

    protected InitialBalanceContractor InitialBalance { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = InitialBalance.Id != Guid.Empty;
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(InitialBalanceContractor.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(InitialBalanceContractor.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(InitialBalanceContractor.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        selectContractor.DataBindings.Add(nameof(selectContractor.SelectedItem), DataContext, nameof(InitialBalanceContractor.ReferenceId), false, DataSourceUpdateMode.OnPropertyChanged);
        comboContract.DataBindings.Add(nameof(comboContract.SelectedItem), DataContext, nameof(InitialBalanceContractor.ContractId), false, DataSourceUpdateMode.OnPropertyChanged);
        choiceDebt.DataBindings.Add(nameof(choiceDebt.ChoiceValue), DataContext, nameof(InitialBalanceContractor.DebtType), true, DataSourceUpdateMode.OnPropertyChanged);
        textOperationSumma.DataBindings.Add(nameof(textOperationSumma.DecimalValue), DataContext, nameof(InitialBalanceContractor.OperationSumma), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        selectContractor.DataSource = services.GetRequiredService<IContractorRepository>().GetListExisting();
        comboContract.DataSource = services
            .GetRequiredService<IContractRepository>()
            .GetByOwner(InitialBalance.ReferenceId);
    }

    private void SelectContractor_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IContractorBrowser>(e.Document);
    }

    private void SelectContractor_DocumentSelectedChanged_1(object sender, DocumentSelectedEventArgs e)
    {
        if (selectContractor.SelectedItem != Guid.Empty)
        {
            comboContract.DataSource = services
                .GetRequiredService<IContractRepository>()
                .GetByOwner(e.Document.Id);
        }
        else
        {
            comboContract.Clear();
        }
    }

    private void ComboContract_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IContractBrowser>(e.Document);
    }
}
