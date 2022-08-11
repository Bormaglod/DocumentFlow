//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Wages;

public class Wage1cEditor : BasePayrollEditor<Wage1c, Wage1cEmployee, IWage1cEmployeeRepository>, IWage1cEditor
{
    public Wage1cEditor(IWage1cRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
    }
}