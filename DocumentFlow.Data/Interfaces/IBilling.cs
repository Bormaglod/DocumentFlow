//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.08.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Interfaces;

public interface IBilling
{
    int BillingYear { get; }
    short BillingMonth { get; }
}
