﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.08.2022
//
// Версия 2023.1.28
//  - свойство employee_name только для чтения
//  - добавлен метод SetEmployeeName
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.Wages.Core;

public interface IWageEmployeeInfo
{
    Guid employee_id { get; set; }
    string employee_name { get; }
    decimal wage { get; set; }

    void SetEmployeeName(string employeeName);
}
