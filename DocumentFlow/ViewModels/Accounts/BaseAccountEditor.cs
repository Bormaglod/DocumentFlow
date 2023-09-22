//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.06.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

public partial class BaseAccountEditor : EditorPanel, IDocumentEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public BaseAccountEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;
    }

    public Guid? OwnerId
    {
        get => DocumentInfo.OwnerId;
        set => DocumentInfo.OwnerId = value;
    }

    protected override void CreateDataSources()
    {
        comboBank.DataSource = services
            .GetRequiredService<IBankRepository>()
            .GetListExisting(callback: q => q.OrderBy("item_name"));
    }

    protected override void DoBindingControls()
    {
        textContractor.DataBindings.Add(nameof(textContractor.TextValue), DataContext, nameof(Account.CompanyName), false, DataSourceUpdateMode.OnPropertyChanged);
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(Account.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textAccount.DataBindings.Add(nameof(textAccount.DecimalValue), DataContext, nameof(Account.AccountValue), true, DataSourceUpdateMode.OnPropertyChanged);
        comboBank.DataBindings.Add(nameof(comboBank.SelectedItem), DataContext, nameof(Account.BankId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
    }

    private void ComboBank_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IBankBrowser>(e.Document);
    }
}
