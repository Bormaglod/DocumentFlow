//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.08.2019
// Time: 13:31
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Entities
{
    using System;
    using DocumentFlow.Data.Core;

    public class DocumentRefs : EntityID
    {
        public virtual Guid OwnerId { get; set; }
        public virtual string FileName { get; set; }
        public virtual string Note { get; set; }
        public virtual long Crc { get; set; }
    }
}
