//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.08.2019
// Time: 13:31
//-----------------------------------------------------------------------

using System;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Data.Entities
{
    public class DocumentRefs : Entity<long>
    {
        public Guid owner_id { get; set; }

        public string file_name { get; set; }

        public string note { get; set; }

        public long crc { get; set; }

        public long length { get; set; }
    }
}
