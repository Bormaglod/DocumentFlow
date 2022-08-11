//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core;

public class BaseDocument : DocumentInfo
{
    public Guid organization_id { get; protected set; }
    public string? organization_name { get; protected set; }

    [DataOperation(DataOperation.Add | DataOperation.Update)]
    public DateTime? document_date { get; set; }

    [DataOperation(DataOperation.Add | DataOperation.Update)]
    public int? document_number { get; set; }
    public void SetOrganization(Guid orgId) => organization_id = orgId;

    public override string ToString() => $"№{document_number} от {document_date:d}";
}
