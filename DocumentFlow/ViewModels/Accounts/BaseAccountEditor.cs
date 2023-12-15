//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.06.2023
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging;

using DocumentFlow.Controls;
using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Messages;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

public partial class BaseAccountEditor : EditorPanel, IDocumentEditor
{
    private readonly IServiceProvider services;

    public BaseAccountEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();

        this.services = services;
    }

    public Guid? OwnerId => DocumentInfo.OwnerId;

    public void SetOwner(IDocumentInfo owner)
    {
        DocumentInfo.OwnerId = owner.Id;
        if (DocumentInfo is Account account && owner is Company company)
        {
            account.CompanyName = company.ItemName;
        }
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
        WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IBankEditor), e.Document));
    }
}
