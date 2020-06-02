//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.03.2019
// Time: 17:13
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Entities
{
    using System;

    public abstract class Directory : DocumentInfo
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual Guid? ParentId { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
