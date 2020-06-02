//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.12.2019
// Time: 18:34
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema.Types.Core
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class TagAttribute : Attribute
    {
        public TagAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
