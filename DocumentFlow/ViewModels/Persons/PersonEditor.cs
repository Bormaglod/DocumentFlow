//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.06.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Person), RepositoryType = typeof(IPersonRepository))]
public partial class PersonEditor : EditorPanel, IPersonEditor
{
    public PersonEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();
    }

    protected Person Person { get; set; } = null!;

    protected override void DoBindingControls()
    {
        textName.DataBindings.Add(nameof(textName.TextValue), DataContext, nameof(Person.ItemName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textSurname.DataBindings.Add(nameof(textSurname.TextValue), DataContext, nameof(Person.Surname), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textFirstName.DataBindings.Add(nameof(textFirstName.TextValue), DataContext, nameof(Person.FirstName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textMiddleName.DataBindings.Add(nameof(textMiddleName.TextValue), DataContext, nameof(Person.MiddleName), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textPhone.DataBindings.Add(nameof(textPhone.TextValue), DataContext, nameof(Person.Phone), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
        textEmail.DataBindings.Add(nameof(textEmail.TextValue), DataContext, nameof(Person.Email), true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
    }
}
