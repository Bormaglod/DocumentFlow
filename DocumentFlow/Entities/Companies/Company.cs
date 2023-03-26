//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Companies;

public class Company : Directory
{
    public string? FullName { get; set; }
    public decimal? Inn { get; set; }
    public decimal? Kpp { get; set; }
    public decimal? Ogrn { get; set; }
    public decimal? Okpo { get; set; }
    public Guid? OkopfId { get; set; }
    public string? OkopfName { get; protected set; }

    [DataOperation(DataOperation.Add | DataOperation.Update)]
    public Guid? AccountId { get; set; }
}
