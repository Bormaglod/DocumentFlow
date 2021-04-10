//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2020
// Time: 17:04
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Data.Core
{
    public class DetailEntity : Entity<long>, IDetailEntity
    {
        public Guid owner_id { get; set; }
    }
}
