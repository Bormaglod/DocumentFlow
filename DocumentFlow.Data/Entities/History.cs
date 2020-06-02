//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.05.2019
// Time: 21:25
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Entities
{
    using System;

    using DocumentFlow.Data.Core;

    public class History : EntityID
    {
        public virtual Guid ReferenceId { get; set; }
        public virtual Status FromStatus { get; set; }
        public virtual Status ToStatus { get; set; }
        public virtual DateTime Changed { get; set; }
        public virtual UserAlias User { get; set; }
    }
}
