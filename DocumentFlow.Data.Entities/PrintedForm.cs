//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.06.2019
// Time: 17:17
//-----------------------------------------------------------------------

using System;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Data.Entities
{
    public class PrintedForm : Entity<Guid>
    {
        public string code { get; set; }
        public string name { get; set; }
        public Guid? picture_id { get; set; }
        public string schema_form { get; set; }
        public Picture Picture { get; set; }
    }
}
