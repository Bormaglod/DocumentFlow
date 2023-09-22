//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.06.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Contractor), RepositoryType = typeof(IContractorRepository))]
public partial class ContractorEditor : EditorPanel, IContractorEditor, IDirectoryEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public ContractorEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        choiceSubject.FromEnum<SubjectsCivilLow>(KeyGenerationMethod.PostgresEnumValue);
    }

    public Guid? ParentId
    {
        get => GetDirectory().ParentId;
        set => GetDirectory().ParentId = value;
    }

    public override void RegisterNestedBrowsers()
    {
        EditorPage.RegisterNestedBrowser<IBalanceContractorBrowser>();
        EditorPage.RegisterNestedBrowser<IBalanceProcessingBrowser>();
        EditorPage.RegisterNestedBrowser<IContractBrowser>();
        EditorPage.RegisterNestedBrowser<IAccountBrowser>();
        EditorPage.RegisterNestedBrowser<IEmployeeBrowser>();
    }

    protected Contractor Contractor { get; set; } = null!;

    protected override void OnEntityPropertyChanged(string? propertyName)
    {
        switch (propertyName)
        {
            case nameof(Contractor.Code):
                OnHeaderChanged();
                break;
            case nameof(Contractor.Subject):
                UpdateVisibleControls();
                break;
        }
    }

    protected override void DoBindingControls()
    {
        UpdateMask();

        textCode.DataBindings.Add(nameof(textCode.TextValue), DataContext, nameof(Contractor.Code), false, DataSourceUpdateMode.OnPropertyChanged);
        selectGroup.DataBindings.Add(nameof(selectGroup.SelectedItem), DataContext, nameof(Contractor.ParentId), true, DataSourceUpdateMode.OnPropertyChanged);
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(Contractor.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textFullName.DataBindings.Add(nameof(textFullName.TextValue), DataContext, nameof(Contractor.FullName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        choiceSubject.DataBindings.Add(nameof(choiceSubject.ChoiceValue), DataContext, nameof(Contractor.Subject), false, DataSourceUpdateMode.OnPropertyChanged);
        textInn.DataBindings.Add(nameof(textInn.DecimalValue), DataContext, nameof(Contractor.Inn), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        textKpp.DataBindings.Add(nameof(textKpp.DecimalValue), DataContext, nameof(Contractor.Kpp), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        textOgrn.DataBindings.Add(nameof(textOgrn.DecimalValue), DataContext, nameof(Contractor.Ogrn), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        textOkpo.DataBindings.Add(nameof(textOkpo.DecimalValue), DataContext, nameof(Contractor.Okpo), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        comboOkopf.DataBindings.Add(nameof(comboOkopf.SelectedItem), DataContext, nameof(Contractor.OkopfId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        comboAccount.DataBindings.Add(nameof(comboAccount.SelectedItem), DataContext, nameof(Contractor.AccountId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        selectPerson.DataBindings.Add(nameof(selectPerson.SelectedItem), DataContext, nameof(Contractor.PersonId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
    }

    protected override void AfterConstructData(ConstructDataMethod method) => UpdateVisibleControls();

    protected override void CreateDataSources()
    {
        selectGroup.DataSource = services
            .GetRequiredService<IContractorRepository>()
            .GetOnlyFolders();

        comboOkopf.DataSource = services
            .GetRequiredService<IOkopfRepository>()
            .GetListExisting(callback: q => q.OrderBy("item_name"));

        selectPerson.DataSource = services
            .GetRequiredService<IPersonRepository>()
            .GetListExisting(callback: q => q.OrderBy("item_name"));

        if (Contractor.Id != Guid.Empty)
        {
            comboAccount.DataSource = services
                .GetRequiredService<IAccountRepository>()
                .GetByOwner(Contractor.AccountId, Contractor.Id);
        }
    }

    private void UpdateMask()
    {
        textInn.Mask = Contractor.SubjectCivilLow switch
        {
            SubjectsCivilLow.Person => "#### ###### ##",
            SubjectsCivilLow.LegalEntity => "#### ##### #",
            _ => string.Empty
        };
    }

    private void UpdateVisibleControls()
    {
        if (Contractor.SubjectCivilLow != null)
        {
            UpdateMask();
        }

        textKpp.Visible = Contractor.SubjectCivilLow == SubjectsCivilLow.LegalEntity;
        textOgrn.Visible = Contractor.SubjectCivilLow == SubjectsCivilLow.LegalEntity;
        textOkpo.Visible = Contractor.SubjectCivilLow == SubjectsCivilLow.LegalEntity;
        comboOkopf.Visible = Contractor.SubjectCivilLow == SubjectsCivilLow.LegalEntity;
        comboAccount.Visible = Contractor.SubjectCivilLow == SubjectsCivilLow.LegalEntity;
        selectPerson.Visible = Contractor.SubjectCivilLow == SubjectsCivilLow.Person;
    }

    private void ComboOkopf_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IOkopfBrowser>(e.Document);
    }

    private void SelectPerson_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IPersonBrowser>(e.Document);
    }
}
