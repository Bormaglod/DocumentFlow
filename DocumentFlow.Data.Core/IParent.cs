//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.10.2020
// Time: 23:26
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Data.Core
{
    public interface IParent
    {
        Guid? parent_id { get; }
        bool is_folder { get; }
    }
}
