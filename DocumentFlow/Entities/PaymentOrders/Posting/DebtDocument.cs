//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.02.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.PaymentOrders.Posting;

public class DebtDocument : IAccountingDocument
{
    public DebtDocument(IAccountingDocument document, string tableName, string documentName, Type editorType) => (Document, TableName, DocumentName, EditorType) = (document, tableName, documentName, editorType);

    public IAccountingDocument Document { get; }
    public string TableName { get; }
    public string DocumentName { get; }
    public Type EditorType { get; }
    public Guid organization_id => Document.organization_id;
    public string? organization_name => Document.organization_name;
    public DateTime? document_date { get => Document.document_date; set => Document.document_date = value; }
    public int? document_number { get => Document.document_number; set => Document.document_number = value; }
    public bool carried_out => Document.carried_out;
    public bool re_carried_out => Document.re_carried_out;
    public Guid user_created_id => Document.user_created_id;
    public DateTime date_created => Document.date_created;
    public Guid user_updated_id => Document.user_updated_id;
    public DateTime date_updated => Document.date_updated;
    public bool deleted => Document.deleted;
    public Guid? owner_id { get => Document.owner_id; set => Document.owner_id = value; }
    public Guid id { get => Document.id; set => Document.id = value; }
    public string? contractor_name { get; set; }
    public decimal full_cost { get; set; }
    public decimal paid { get; set; }

    public void SetOrganization(Guid orgId) => Document.SetOrganization(orgId);

    public override string ToString() => $"{DocumentName} {Document}";
}
