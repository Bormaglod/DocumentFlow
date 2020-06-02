//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.11.2014
// Time: 18:26
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    public abstract class Entity : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        public virtual Entity GetReference(Type type)
        {
            PropertyInfo pi = GetType().GetProperties().FirstOrDefault(p => p.PropertyType == type);
            return pi?.GetValue(this, null) as Entity;
        }

        public virtual Entity GetReference<T>() where T : Entity => GetReference(typeof(T));

        public virtual void SetReference(Type type, Entity entity)
        {
            PropertyInfo pi = GetType().GetProperties().FirstOrDefault(p => p.PropertyType == type);
            if (pi != null)
            {
                pi.SetValue(this, entity, null);
            }
        }

        public virtual void SetReference<T>(T entity) where T : Entity => SetReference(typeof(T), entity);

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
