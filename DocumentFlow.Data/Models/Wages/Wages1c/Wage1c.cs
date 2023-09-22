//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Начисление зар. платы 1C")]
public class Wage1c : BasePayroll
{
    [WritableCollection]
    public IList<Wage1cEmployee> Employees { get; protected set; } = null!;
}
