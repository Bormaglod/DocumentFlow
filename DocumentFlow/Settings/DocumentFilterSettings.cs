//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Settings;

public class DocumentFilterSettings
{
    public bool DateFromEnabled { get; set; }
    public bool DateToEnabled { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}
