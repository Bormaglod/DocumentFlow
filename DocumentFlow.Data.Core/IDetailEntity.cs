//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.11.2020
// Time: 20:01
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Data.Core
{
    public interface IDetailEntity : IIdentifier<long>
    {
        Guid owner_id { get; }
    }
}
