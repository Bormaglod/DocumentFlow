//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.10.2020
// Time: 20:15
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Code
{
    public interface IBrowserParameters
    {
        Guid? ParentId { get; }
        Guid? OwnerId { get; }
        DateTime DateFrom { get; }
        DateTime DateTo { get; }
        Guid? OrganizationId { get; }
    }
}
