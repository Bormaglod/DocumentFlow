//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.08.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.Wages.Core;

public interface IWageEmployeeInfo
{
    Guid employee_id { get; set; }
    string employee_name { get; set; }
    decimal wage { get; set; }
}
