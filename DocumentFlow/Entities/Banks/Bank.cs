//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Banks;

[Description("Банк")]
public class Bank : Directory
{
    public decimal bik { get; set; }
    public decimal account { get; set; }
    public string? town { get; set; }
}
