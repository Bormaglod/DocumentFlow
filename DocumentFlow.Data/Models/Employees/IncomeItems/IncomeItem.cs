//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Data.Models;

public class IncomeItem : Identifier<Guid>, IItem
{
    public string Code { get; set; } = string.Empty;
    public string? ItemName { get; set; }

    public override string ToString() => Code;
}
