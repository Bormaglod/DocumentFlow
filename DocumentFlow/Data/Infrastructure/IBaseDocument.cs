//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.06.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Infrastructure;

public interface IBaseDocument : IDocumentInfo
{
    Guid organization_id { get; }
    string? organization_name { get; }
    DateTime? document_date { get; set; }
    int? document_number { get; set; }
    void SetOrganization(Guid orgId);
}
