//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.07.2023
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging;

using DocumentFlow.Controls;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Messages;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.ViewModels;

public partial class BaseEmployeeEditor : EditorPanel, IDocumentEditor
{
    public BaseEmployeeEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();

        choiceRole.FromEnum<EmployeeRole>(KeyGenerationMethod.PostgresEnumValue);

        var persons = services.GetRequiredService<IPersonRepository>();
        selectPerson.DataSource = persons.GetListExisting(callback: q => q.OrderBy("item_name"));

        var posts = services.GetRequiredService<IOkpdtrRepository>();
        selectPost.DataSource = posts.GetListExisting(callback: q => q.OrderBy("item_name"));

        if (DocumentInfo is OurEmployee)
        {
            selectIncomeItems.Visible = true;

            var items = services.GetRequiredService<IIncomeItemRepository>();
            selectIncomeItems.DataSource = items.GetListUserDefined();
        }
    }

    public Guid? OwnerId => DocumentInfo.OwnerId;

    public void SetOwner(IDocumentInfo owner)
    {
        DocumentInfo.OwnerId = owner.Id;
        if (DocumentInfo is Employee employee && owner is Company company) 
        {
            employee.OwnerName = company.ItemName;
        }
    }

    protected override void DoBindingControls()
    {
        if (DataContext is Employee)
        {
            textOrg.DataBindings.Add(nameof(textOrg.TextValue), DataContext, nameof(Employee.OwnerName));
            selectPerson.DataBindings.Add(nameof(selectPerson.SelectedItem), DataContext, nameof(Employee.PersonId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
            selectPost.DataBindings.Add(nameof(selectPost.SelectedItem), DataContext, nameof(Employee.PostId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
            textPhone.DataBindings.Add(nameof(textPhone.TextValue), DataContext, nameof(Employee.Phone), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
            textEmail.DataBindings.Add(nameof(textEmail.TextValue), DataContext, nameof(Employee.Email), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
            choiceRole.DataBindings.Add(nameof(choiceRole.ChoiceValue), DataContext, nameof(Employee.EmpRole), false, DataSourceUpdateMode.OnPropertyChanged);
        }

        if (DataContext is OurEmployee)
        {
            selectIncomeItems.DataBindings.Add(nameof(selectIncomeItems.SelectedItems), DataContext, nameof(OurEmployee.IncomeItems), false, DataSourceUpdateMode.OnPropertyChanged);
        }
    }

    private void SelectPerson_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IPersonEditor), e.Document));
    }

    private void SelectPost_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IOkpdtrEditor), e.Document));
    }
}