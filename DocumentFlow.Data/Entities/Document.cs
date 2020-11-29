//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.03.2019
// Time: 17:13
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Data.Entities
{
    public class Document : DocumentInfo
    {
        public DateTime doc_date { get; set; }
        public int doc_year { get; set; }
        public long doc_number { get; set; }
        public string view_number { get; set; }
        public Guid organization_id { get; set; }
    }
}
