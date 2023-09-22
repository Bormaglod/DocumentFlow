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

using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace DocumentFlow.Data;

public abstract class DocumentInfo : Entity<Guid>, IDocumentInfo
{
    private class DependentCollection
    {
        private readonly DocumentInfo owner;
        private readonly IList dependentCollection = null!;

        public DependentCollection(DocumentInfo owner, PropertyInfo prop)
        {
            this.owner = owner;

            var gta = prop.PropertyType.GenericTypeArguments[0];
            var listType = typeof(ObservableCollection<>).MakeGenericType(gta);

            var list = Activator.CreateInstance(listType);
            if (list is INotifyCollectionChanged changed)
            {
                changed.CollectionChanged += List_CollectionChanged;
                dependentCollection = (IList)list;

                prop.SetValue(owner, dependentCollection);
            }

            owner.PropertyChanged += Owner_PropertyChanged;
        }

        public List<IDependentEntity> NewItems { get; } = new();
        public List<IDependentEntity> UpdateItems { get; } = new();
        public List<IDependentEntity> RemoveItems { get; } = new();

        public void CompleteChanged()
        {
            NewItems.Clear();
            UpdateItems.Clear();
            RemoveItems.Clear();
        }

        private void Owner_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Id))
            {
                foreach (var part in dependentCollection.OfType<IDependentEntity>())
                {
                    part.SetOwner(owner.Id);
                }
            }
        }

        private void List_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        foreach (var part in e.NewItems.OfType<IDependentEntity>())
                        {
                            if (owner.Id != default)
                            {
                                part.SetOwner(owner.Id);
                            }

                            if (part.Id == 0)
                            {
                                NewItems.Add(part);
                            }
                            else
                            {
                                if (part is INotifyPropertyChanged changed)
                                {
                                    changed.PropertyChanged += Changed_PropertyChanged;
                                }
                                else
                                {
                                    UpdateItems.Add(part);
                                }
                            }
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        foreach (var part in e.OldItems.OfType<IDependentEntity>().Where(p => p.Id != 0))
                        {
                            RemoveItems.Add(part);
                        }
                    }

                    break;
            }
        }

        private void Changed_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is IDependentEntity entity)
            {
                if (!UpdateItems.Contains(entity))
                {
                    UpdateItems.Add(entity);
                }
            }
        }
    }

    private readonly List<DependentCollection> DependentCollectionList = new();

    private bool isLoaded = false;

    public DocumentInfo()
    {
        foreach (var collection in EntityProperties.CollectionPropertiesCache(GetType()))
        {
            DependentCollectionList.Add(new DependentCollection(this, collection));
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
