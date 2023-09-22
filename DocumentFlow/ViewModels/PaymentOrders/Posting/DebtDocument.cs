//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Data.Models;

public sealed class DebtDocument : IBaseDocument, IComparable, IComparable<DebtDocument>
{
    public DebtDocument(IBaseDocument document, string tableName, string documentName, Type editorType) => (Document, TableName, DocumentName, EditorType) = (document, tableName, documentName, editorType);

    public IBaseDocument Document { get; }
    public string TableName { get; }
    public string DocumentName { get; }
    public Type EditorType { get; }
    public Guid OrganizationId => Document.OrganizationId;
    public string? OrganizationName => Document.OrganizationName;
    public DateTime? DocumentDate { get => Document.DocumentDate; set => Document.DocumentDate = value; }
    public int? DocumentNumber { get => Document.DocumentNumber; set => Document.DocumentNumber = value; }
    public Guid UserCreatedId => Document.UserCreatedId;
    public DateTime DateCreated => Document.DateCreated;
    public Guid UserUpdatedId => Document.UserUpdatedId;
    public DateTime DateUpdated => Document.DateUpdated;
    public bool Deleted => Document.Deleted;
    public Guid? OwnerId { get => Document.OwnerId; set => Document.OwnerId = value; }
    public Guid Id { get => Document.Id; set => Document.Id = value; }
    public bool? HasDocuments => false;
    public string? ContractorName { get; set; }
    public decimal FullCost { get; set; }
    public decimal Paid { get; set; }

    public int CompareTo(object? obj)
    {
        if (obj is DebtDocument other)
        {
            return CompareTo(other);
        }

        throw new Exception($"{obj} должен быть типа {GetType().Name}");
    }

    public int CompareTo(DebtDocument? other)
    {
        if (other == null)
        {
            return 1;
        }

        DateTime date = DocumentDate ?? DateTime.MinValue;
        DateTime other_date = other.DocumentDate ?? DateTime.MinValue;

        int res = date.CompareTo(other_date);
        if (res == 0)
        {
            int num = DocumentNumber ?? 0;
            int other_num = other.DocumentNumber ?? 0;
            res = num.CompareTo(other_num);
        }

        return res;
    }

    public void Loaded() { }
    public override string ToString() => $"{DocumentName} {Document}";
}
