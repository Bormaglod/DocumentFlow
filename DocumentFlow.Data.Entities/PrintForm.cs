//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.06.2019
// Time: 17:17
//-----------------------------------------------------------------------

using System;
using DocumentFlow.Data.Base;

namespace DocumentFlow.Data.Entities
{
    public class PrintForm : Entity<Guid>
    {
        public string name { get; set; }
        public Guid? picture_id { get; set; }
        public string form_text { get; set; }
        public string properties { get; set; }
        public Picture Picture { get; set; }
    }
}
