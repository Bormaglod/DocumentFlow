//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.12.2019
// Time: 13:31
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Data.Entities
{
    public class Email : Entity<long>
    {
        public string address { get; set; }
        public string host { get; set; }
        public short port { get; set; }
        public string password { get; set; }
        public string signature_plain { get; set; }
        public string signature_html { get; set; }
    }
}
