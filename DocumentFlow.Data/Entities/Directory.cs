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
    public abstract class Directory : DocumentInfo
    {
        public string code { get; set; }
        public string name { get; set; }
        public Guid? parent_id { get; set; }
        public override string ToString() => name;
    }
}
