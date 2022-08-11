//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.08.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.Wages;

public class BasePayroll : BillingDocument
{
    public decimal wage { get; protected set; }
    
}
