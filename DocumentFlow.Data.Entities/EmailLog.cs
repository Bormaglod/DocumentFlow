//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.12.2019
// Time: 13:31
//-----------------------------------------------------------------------

using System;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Data.Entities
{
    public class EmailLog : Entity<long>
    {
        public long email_id { get; set; }
        public DateTime date_time_sending { get; set; }
        public string to_address { get; set; }
        public Guid document_id { get; set; }
    }
}
