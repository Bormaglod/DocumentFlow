//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.05.2019
// Time: 21:25
//-----------------------------------------------------------------------

using System;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Data.Entities
{
    public class History : Entity<long>
    {
        public Guid reference_id { get; set; }
        public int from_status_id { get; set; }
        public int to_status_id { get; set; }
        public DateTime changed { get; set; }
        public string user_name { get; set; }
        public Status FromStatus { get; set; }
        public Status ToStatus { get; set; }
    }
}
