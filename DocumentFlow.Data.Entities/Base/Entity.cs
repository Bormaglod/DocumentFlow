//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.11.2014
// Time: 18:26
//-----------------------------------------------------------------------

using System;
using System.ComponentModel;

namespace DocumentFlow.Data.Base
{
    public abstract class Entity<T> : INotifyPropertyChanged, IIdentifier, IIdentifier<T> where T : IComparable<T>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public T id { get; set; }

        protected void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        object IIdentifier.oid
        {
            get { return id; }
        }
    }
}
