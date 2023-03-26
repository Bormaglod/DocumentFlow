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
    public Guid OrganizationId => Document.OrganizationId;
    public string? OrganizationName => Document.OrganizationName;
    public DateTime? DocumentDate { get => Document.DocumentDate; set => Document.DocumentDate = value; }
    public int? DocumentNumber { get => Document.DocumentNumber; set => Document.DocumentNumber = value; }
    public bool CarriedOut => Document.CarriedOut;
    public bool ReCarriedOut => Document.ReCarriedOut;
    public Guid UserCreatedId => Document.UserCreatedId;
    public DateTime DateCreated => Document.DateCreated;
    public Guid UserUpdatedId => Document.UserUpdatedId;
    public DateTime DateUpdated => Document.DateUpdated;
    public bool Deleted => Document.Deleted;
    public Guid? OwnerId { get => Document.OwnerId; set => Document.OwnerId = value; }
    public Guid Id { get => Document.Id; set => Document.Id = value; }
    public string? ContractorName { get; set; }
    public decimal FullCost { get; set; }
    public decimal Paid { get; set; }

    public void SetOrganization(Guid orgId) => Document.SetOrganization(orgId);

    public override string ToString() => $"{DocumentName} {Document}";
}
