//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.06.2019
// Time: 20:25
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Code.Core
{
    public class ComboBoxDataItem : IIdentifier
    {
        public Guid id { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
