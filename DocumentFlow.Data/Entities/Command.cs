//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.03.2019
// Time: 18:43
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Entities
{
    using System;
    using DocumentFlow.Data.Core;

    public class Command : EntityUID
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual Guid? ParentId { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual string Note { get; set; }
        public virtual EntityKind EntityKind { get; set; }
    }
}
