//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.03.2019
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Extension;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;

using System.Data;

namespace DocumentFlow.Data;

public abstract class DocumentInfo : Entity<Guid>, IDocumentInfo
{
    private readonly List<IDependentCollection> DependentCollectionList = new();

    private bool isLoaded = false;

    public DocumentInfo()
    {
        foreach (var prop in EntityProperties.CollectionPropertiesCache(GetType()))
        {
            var gta = prop.PropertyType.GenericTypeArguments[0];
            var listType = typeof(DependentCollection<>).MakeGenericType(gta);

            var list = Activator.CreateInstance(listType, this) ?? throw new Exception($"An error occurred while creating the object DependentCollection<{gta.Name}>.");
            prop.SetValue(this, list);

            DependentCollectionList.Add((IDependentCollection)list);
        }
    }

    [Write(false)]
    public Guid UserCreatedId { get; protected set; }

    [Write(false)]
    public DateTime DateCreated { get; protected set; }

    [Computed]
    public string? UserCreated { get; protected set; }

    [Write(false)]
    public Guid UserUpdatedId { get; protected set; }

    [Write(false)]
    public DateTime DateUpdated { get; protected set; }

    [Computed]
    public string? UserUpdated { get; protected set; }

    [Write(false)]
    public bool Deleted { get; protected set; }

    [Computed]
    public bool? HasDocuments { get; protected set; }

    public bool IsLoaded => isLoaded;

    public void SaveChanges(IDbConnection connection, IDbTransaction? transaction = null)
    {
        foreach (var collection in DependentCollectionList)
        {
            connection.ExecuteCommand(SQLCommand.Wipe, collection.RemoveItems, transaction);
            connection.Update(collection.UpdateItems, transaction);
            connection.Insert(collection.NewItems, transaction);

            collection.CompleteChanged();
        }
    }

    void IDocumentInfo.Loaded() => isLoaded = true;
}
