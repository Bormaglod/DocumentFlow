//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Companies;

[Description("Приложение")]
public class ContractApplication : Directory
{
    public string? contract_name { get; protected set; }
    public DateTime document_date { get; set; }
    public DateTime date_start { get; set; }
    public DateTime? date_end { get; set; }
    public override string ToString() => $"{item_name} №{code} от {document_date:d}";
}
