//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Companies;

namespace DocumentFlow.Entities.Products;

public class Сustomer : Contractor
{
    public Guid? contract_id { get; protected set; }
    public Guid? application_id { get; protected set; }
    public string? doc_number { get; protected set; }
    public string? doc_name { get; protected set; }
    public DateTime date_start { get; protected set; }
    public decimal price { get; protected set; }
}
