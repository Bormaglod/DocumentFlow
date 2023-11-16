//-----------------------------------------------------------------------
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

[Entity(typeof(InitialBalanceEmployee), RepositoryType = typeof(IInitialBalanceEmployeeRepository))]
public partial class InitialBalanceEmployeeEditor : EditorPanel, IInitialBalanceEmployeeEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public InitialBalanceEmployeeEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        InitialBalanceEmployee.OrganizationId = services.GetRequiredService<IOrganizationRepository>().GetMain().Id;

        choiceDebt.FromEnum<DebtType>(KeyGenerationMethod.EnumValue);
    }

    protected InitialBalanceEmployee InitialBalanceEmployee { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = InitialBalanceEmployee.Id != Guid.Empty;
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(InitialBalanceEmployee.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(InitialBalanceEmployee.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(InitialBalanceEmployee.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        selectEmployee.DataBindings.Add(nameof(selectEmployee.SelectedItem), DataContext, nameof(InitialBalanceEmployee.ReferenceId), false, DataSourceUpdateMode.OnPropertyChanged);
        choiceDebt.DataBindings.Add(nameof(choiceDebt.ChoiceValue), DataContext, nameof(InitialBalanceEmployee.DebtType), true, DataSourceUpdateMode.OnPropertyChanged);
        textOperationSumma.DataBindings.Add(nameof(textOperationSumma.DecimalValue), DataContext, nameof(InitialBalanceEmployee.OperationSumma), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        selectEmployee.DataSource = services.GetRequiredService<IOurEmployeeRepository>().GetListExisting();
    }

    private void ComboContract_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IContractBrowser>(e.Document);
    }

    private void SelectEmployee_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IOurEmployeeBrowser>(e.Document);
    }
}
