//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Entities.Wages;

public class ReportCardEmployee : Entity<long>, IEntityClonable, ICloneable
{
    public Guid employee_id { get; set; }
    public string employee_name { get; protected set; } = string.Empty;
    public string[]? labels { get; set; }
    public short[]? hours { get; set; }
    public string[]? info { get; protected set; }


    public object Clone() => MemberwiseClone();

    public object Copy()
    {
        var copy = Clone();
        ((ReportCardEmployee)copy).id = 0;

        return copy;
    }

    public override string ToString() => employee_name;
}
