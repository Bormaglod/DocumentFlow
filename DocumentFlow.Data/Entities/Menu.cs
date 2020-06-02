//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.06.2018
// Time: 22:09
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Entities
{
    using System;
    using DocumentFlow.Data.Core;

    public class Menu : EntityUID
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual Guid? ParentId { get; set; }
        public virtual int OrderIndex { get; set; }
        public virtual Command Command { get; set; }
    }
}
