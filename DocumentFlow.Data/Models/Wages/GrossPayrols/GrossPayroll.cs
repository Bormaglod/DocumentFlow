//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;


[EntityName("Начисление зар. платы")]
public class GrossPayroll : BasePayroll
{
    [WritableCollection]
    public IList<GrossPayrollEmployee> Employees { get; protected set; } = null!;
}
