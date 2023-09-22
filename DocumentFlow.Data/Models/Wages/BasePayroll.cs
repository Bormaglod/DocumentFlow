//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.08.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Models;

public class BasePayroll : BillingDocument
{
    /// <summary>
    /// Возвращает сумму заработной платы.
    /// </summary>
    public decimal Wage { get; protected set; }
}
