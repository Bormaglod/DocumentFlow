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
    public string? ContractName { get; protected set; }
    public DateTime DocumentDate { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public override string ToString() => $"{ItemName} №{Code} от {DocumentDate:d}";
}
