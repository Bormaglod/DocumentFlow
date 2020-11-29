//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.03.2019
// Time: 21:31
//-----------------------------------------------------------------------

using System;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Data.Entities
{
    public abstract class DocumentInfo : EntityUID, IComparable
    {
        public int status_id { get; set; }
        public Guid? owner_id { get; set; }
        public Guid entity_kind_id { get; set; }
        public Guid user_created_id { get; }
        public DateTime date_created { get; }
        public Guid user_updated_id { get; }
        public DateTime date_updated { get; }
        public Guid? user_locked_id { get; set; }
        public DateTime? date_locked { get; set; }

        public bool IsGroup() => status_id == 500;

        int IComparable.CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            Type curType = GetType();
            Type objType = obj.GetType();

            if (curType == objType)
                return ToString().CompareTo(obj.ToString());
            else
                throw new ArgumentException("Предпринята попытка сравнить два разных объекта.");
        }
    }
}
