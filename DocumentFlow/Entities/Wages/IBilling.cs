//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.08.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.Wages;

public interface IBilling
{
#pragma warning disable IDE1006 // Стили именования
    int billing_year { get; }
    short billing_month { get; }
#pragma warning restore IDE1006 // Стили именования
}
