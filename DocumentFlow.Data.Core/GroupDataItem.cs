//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.10.2020
// Time: 23:03
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Data.Core
{
    public class GroupDataItem : Entity<Guid>, IParent
    {
        public Guid? parent_id { get; set; }

        [ColumnDescription("Наименование")]
        public string name { get; set; }

        public int status_id { get; set; }

        public bool is_folder => status_id == 500;

        public override string ToString() => name;
    }
}
