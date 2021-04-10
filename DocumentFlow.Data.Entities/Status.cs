//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.03.2019
// Time: 19:44
//-----------------------------------------------------------------------

using System;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Data.Entities
{
    public class Status : Entity<int>
    {
        public string code { get; set; }
        public string note { get; set; }
        public Guid? picture_id { get; set; }
        public Picture Picture { get; set; }
        public override string ToString() => note;
    }
}
