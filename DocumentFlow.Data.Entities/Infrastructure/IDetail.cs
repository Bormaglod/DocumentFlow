//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.11.2020
// Time: 20:01
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Data
{
    public interface IDetail : IIdentifier, IIdentifier<long>
    {
        Guid owner_id { get; }
    }
}
