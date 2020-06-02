//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.01.2020
// Time: 09:22
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core
{
    using System;

    public interface IChildItem : ICloneable
    {
        long Id { get; set; }
        Guid? Owner { get; set; }
    }
}
