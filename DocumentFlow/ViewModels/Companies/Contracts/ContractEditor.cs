//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.06.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Contract), RepositoryType = typeof(IContractRepository))]
public partial class ContractEditor : EditorPanel, IContractEditor, IDocumentEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public ContractEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        choiceType.FromEnum<ContractorType>(KeyGenerationMethod.PostgresEnumValue);
    }

    public Guid? OwnerId
    {
        get => Contract.OwnerId;
        set => Contract.OwnerId = value;
    }

    protected Contract Contract { get; set; } = null!;

    public override void RegisterNestedBrowsers()
    {
        EditorPage.RegisterNestedBrowser<IContractApplicationBrowser>();
    }

    protected override void OnEntityPropertyChanged(string? propertyName)
    {
        if (propertyName == nameof(Contract.ItemName) || propertyName == nameof(Contract.Code) || propertyName == nameof(Contract.DocumentDate))
        {
            OnHeaderChanged();
        }
    }

    protected override void DoBindingControls()
    {
        textContractor.DataBindings.Add(nameof(textContractor.TextValue), DataContext, nameof(Contract.ContractorName), false, DataSourceUpdateMode.OnPropertyChanged);
        choiceType.DataBindings.Add(nameof(choiceType.ChoiceValue), DataContext, nameof(Contract.CType), false, DataSourceUpdateMode.OnPropertyChanged);
        textNumber.DataBindings.Add(nameof(textNumber.TextValue), DataContext, nameof(Contract.Code), false, DataSourceUpdateMode.OnPropertyChanged);
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(Contract.ItemName), false, DataSourceUpdateMode.OnPropertyChanged);
        dateContract.DataBindings.Add(nameof(dateContract.DateTimeValue), DataContext, nameof(Contract.DocumentDate), false, DataSourceUpdateMode.OnPropertyChanged);
        toggleTax.DataBindings.Add(nameof(toggleTax.ToggleValue), DataContext, nameof(Contract.TaxPayer), false, DataSourceUpdateMode.OnPropertyChanged);
        selectSignatory.DataBindings.Add(nameof(selectSignatory.SelectedItem), DataContext, nameof(Contract.SignatoryId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        selectOrgSignatory.DataBindings.Add(nameof(selectOrgSignatory.SelectedItem), DataContext, nameof(Contract.OrgSignatoryId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        dateStart.DataBindings.Add(nameof(dateStart.DateTimeValue), DataContext, nameof(Contract.DateStart), false, DataSourceUpdateMode.OnPropertyChanged);
        dateEnd.DataBindings.Add(nameof(dateEnd.DateTimeValue), DataContext, nameof(Contract.DateEnd), true, DataSourceUpdateMode.OnPropertyChanged, DateTime.MinValue);
        textPaymentPeriod.DataBindings.Add(nameof(textPaymentPeriod.IntegerValue), DataContext, nameof(Contract.PaymentPeriod), true, DataSourceUpdateMode.OnPropertyChanged);
        toggleDefault.DataBindings.Add(nameof(toggleDefault.ToggleValue), DataContext, nameof(Contract.IsDefault), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        var emps = services.GetRequiredService<IEmployeeRepository>();
        if (Contract.SignatoryId != null)
        {
            selectSignatory.DataSource = emps.GetByOwner(Contract.SignatoryId, OwnerId, callback: q => q.WhereFalse("employee.deleted"));
        }
        else
        {
            selectSignatory.DataSource = emps.GetByOwner(OwnerId, callback: q => q.WhereFalse("employee.deleted"));
        }

        if (Contract.OrgSignatoryId != null)
        {
            selectOrgSignatory.DataSource = services.GetRequiredService<IOurEmployeeRepository>().GetListExisting(callback: q => q.OrWhere("our_employee.id", Contract.OrgSignatoryId));
        }
        else
        {
            selectOrgSignatory.DataSource = services.GetRequiredService<IOurEmployeeRepository>().GetListExisting();
        }
    }

    private void SelectSignatory_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IEmployeeBrowser>(e.Document);
    }

    private void SelectOrgSignatory_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IOrgEmployeeBrowser>(e.Document);
    }
}
