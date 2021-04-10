//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.03.2019
// Time: 17:13
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Data.Core
{
    public abstract class Directory : DocumentInfo, IDirectory, IParent, IComparable, IComparable<Directory>
    {
        public string code { get; set; }
        public string name { get; set; }
        public Guid? parent_id { get; set; }
        public bool is_folder => status_id == 500;
        public override string ToString() => string.IsNullOrEmpty(name) ? code : name;

        public int CompareTo(object obj)
        {
            if (obj is Directory other)
            {
                return CompareTo(other);
            }

            throw new Exception($"{obj} должен быть типа {GetType().Name}");
        }

        public int CompareTo(Directory other)
        {
            if (status_id != other.status_id)
            {
                if (is_folder)
                {
                    return -1;
                }

                if (other.is_folder)
                {
                    return 1;
                }
            }

            string name_current = string.IsNullOrEmpty(name) ? code : name;
            string name_other = string.IsNullOrEmpty(other.name) ? other.code : other.name;
            return name_current.CompareTo(name_other);
        }
    }
}
