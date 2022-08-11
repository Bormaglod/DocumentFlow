//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Employees;

public class EmployeeBrowser : BaseEmployeeBrowser<Employee>, IEmployeeBrowser
{
    public EmployeeBrowser(IEmployeeRepository repository, IPageManager pageManager) : base(repository, pageManager)
    {
        Toolbar.IconSize = ButtonIconSize.Small;
    }

    protected override string HeaderText => "Сотрудники";
}
