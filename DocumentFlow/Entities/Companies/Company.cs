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
    public string? full_name { get; set; }
    public decimal? inn { get; set; }
    public decimal? kpp { get; set; }
    public decimal? ogrn { get; set; }
    public decimal? okpo { get; set; }
    public Guid? okopf_id { get; set; }
    public string? okopf_name { get; protected set; }

    [DataOperation(DataOperation.Add | DataOperation.Update)]
    public Guid? account_id { get; set; }
}
