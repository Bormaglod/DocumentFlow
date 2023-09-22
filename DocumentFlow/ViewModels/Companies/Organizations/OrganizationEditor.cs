//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.06.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Events;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Organization), RepositoryType = typeof(IOrganizationRepository))]
public partial class OrganizationEditor : EditorPanel, IOrganizationEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public OrganizationEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;
    }

    public override void RegisterNestedBrowsers()
    {
        EditorPage.RegisterNestedBrowser<IOurAccountBrowser>();
        EditorPage.RegisterNestedBrowser<IOrgEmployeeBrowser>();
    }

    protected Organization Organization { get; set; } = null!;

    protected override void DoBindingControls()
    {
        textCode.DataBindings.Add(nameof(textCode.TextValue), DataContext, nameof(Organization.Code), false, DataSourceUpdateMode.OnPropertyChanged);
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(Organization.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textFullName.DataBindings.Add(nameof(textFullName.TextValue), DataContext, nameof(Organization.FullName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textInn.DataBindings.Add(nameof(textInn.DecimalValue), DataContext, nameof(Organization.Inn), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        textKpp.DataBindings.Add(nameof(textKpp.DecimalValue), DataContext, nameof(Organization.Kpp), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        textOgrn.DataBindings.Add(nameof(textOgrn.DecimalValue), DataContext, nameof(Organization.Ogrn), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        textOkpo.DataBindings.Add(nameof(textOkpo.DecimalValue), DataContext, nameof(Organization.Okpo), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        comboOkopf.DataBindings.Add(nameof(comboOkopf.SelectedItem), DataContext, nameof(Organization.OkopfId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        comboAccount.DataBindings.Add(nameof(comboAccount.SelectedItem), DataContext, nameof(Organization.AccountId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        textAddress.DataBindings.Add(nameof(textAddress.TextValue), DataContext, nameof(Organization.Address), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textPhone.DataBindings.Add(nameof(textPhone.TextValue), DataContext, nameof(Organization.Phone), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textEmail.DataBindings.Add(nameof(textEmail.TextValue), DataContext, nameof(Organization.Email), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        toggleMainOrg.DataBindings.Add(nameof(toggleMainOrg.ToggleValue), DataContext, nameof(Organization.DefaultOrg), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        comboOkopf.DataSource = services
            .GetRequiredService<IOkopfRepository>()
            .GetListExisting(callback: q => q.OrderBy("item_name"));

        if (Organization.Id != Guid.Empty)
        {
            comboAccount.DataSource = services
                .GetRequiredService<IOurAccountRepository>()
                .GetByOwner(Organization.AccountId, Organization.Id);
        }
    }

    private void ComboOkopf_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IOkopfBrowser>(comboOkopf.SelectedDocument);
    }

    private void ComboAccount_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IOurAccountBrowser>(comboAccount.SelectedDocument);
    }
}
