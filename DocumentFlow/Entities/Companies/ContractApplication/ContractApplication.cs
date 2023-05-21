//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//
// Версия 2023.5.21
//  - добавлено свойство Note
//
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
    public string? Note { get; set; }
    public override string ToString() => $"{ItemName} №{Code} от {DocumentDate:d}";
}
