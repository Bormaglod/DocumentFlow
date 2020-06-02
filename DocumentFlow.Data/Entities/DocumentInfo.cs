//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.03.2019
// Time: 21:31
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Entities
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Reflection;
    using DocumentFlow.Data.Core;

    public abstract class DocumentInfo : EntityUID, IComparable, ICloneable
    {
        public virtual Status Status { get; set; }
        public virtual Guid? OwnerId { get; set; }
        public virtual EntityKind EntityKind { get; set; }
        public virtual UserAlias UserCreated { get; }
        public virtual DateTime? DateCreated { get; }
        public virtual UserAlias UserUpdated { get; }
        public virtual DateTime? DateUpdated { get; }
        public virtual UserAlias UserLocked { get; set; }
        public virtual DateTime? DateLocked { get; set; }
        public virtual bool IsGroup() => Status.Id == 500;

        int IComparable.CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            Type curType = GetType();
            if (curType.Name.EndsWith("Proxy"))
                curType = curType.BaseType;

            Type objType = obj.GetType();
            if (objType.Name.EndsWith("Proxy"))
                objType = objType.BaseType;

            if (curType == objType)
                return this.ToString().CompareTo(obj.ToString());
            else
                throw new ArgumentException("Предпринята попытка сравнить два разных объекта.");
        }

        object ICloneable.Clone()
        {
            return CreateCloneObject(this);
        }

        private DocumentInfo CreateCloneObject(DocumentInfo cloneableObject)
        {
            DocumentInfo newObject = Activator.CreateInstance(cloneableObject.GetType()) as DocumentInfo;
            foreach (PropertyInfo prop in cloneableObject.GetType().GetProperties())
            {
                if (!prop.CanWrite)
                    continue;

                object propValue = prop.GetValue(cloneableObject);
                if (propValue == null || propValue.GetType().GetInterface("IList") == null)
                {
                    DefaultValueAttribute def = prop.GetCustomAttribute<DefaultValueAttribute>();
                    prop.SetValue(newObject, def == null ? propValue : def.Value);
                }
                else
                {
                    IList list = prop.GetValue(newObject) as IList;
                    foreach (var item in propValue as IList)
                    {
                        IChildItem cloneableItem = item as IChildItem;
                        if (cloneableItem != null)
                        {
                            IChildItem newItem = cloneableItem.Clone() as IChildItem;
                            newItem.Owner = newObject.Id;
                            list.Add(newItem);
                        }
                    }

                    prop.SetValue(newObject, list);
                }
            }

            newObject.Id = Guid.Empty;
            newObject.Status = null;
            return newObject;
        }
    }
}
