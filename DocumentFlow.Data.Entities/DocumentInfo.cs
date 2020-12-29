//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.03.2019
// Time: 21:31
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Data.Entities
{
    public class DocumentInfo : Entity<Guid>, IDocumentInfo
    {
        public int status_id { get; set; }
        public string status_name { get; protected set; }
        public Guid? owner_id { get; set; }
        public Guid entity_kind_id { get; set; }
        public Guid user_created_id { get; }
        public DateTime date_created { get; }
        public string user_created { get; }
        public Guid user_updated_id { get; }
        public DateTime date_updated { get; }
        public string user_updated { get; }
        public Guid? user_locked_id { get; set; }
        public DateTime? date_locked { get; set; }
    }
}
