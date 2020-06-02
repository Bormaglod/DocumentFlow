//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.12.2019
// Time: 13:31
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Entities
{
    using DocumentFlow.Data.Core;

    public class Email : EntityID
    {
        public virtual string Address { get; set; }
        public virtual string Host { get; set; }
        public virtual short Port { get; set; }
        public virtual string Password { get; set; }
        public virtual string SignaturePlain { get; set; }
        public virtual string SignatureHtml { get; set; }
    }
}
