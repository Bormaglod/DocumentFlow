//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.07.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Employee), RepositoryType = typeof(IEmployeeRepository))]
public partial class EmployeeEditor : BaseEmployeeEditor, IEmployeeEditor
{
    public EmployeeEditor(IServiceProvider services) : base(services)
    {
        InitializeComponent();
    }

    protected Employee Employee { get; set; } = null!;
}
