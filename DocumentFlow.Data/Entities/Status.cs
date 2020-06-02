//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.03.2019
// Time: 19:44
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Entities
{
    using DocumentFlow.Data.Core;

    public class Status : Entity
    {
        public virtual int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Note { get; set; }
        public virtual Picture Picture { get; set; }

        public override string ToString()
        {
            return Note;
        }
    }
}
