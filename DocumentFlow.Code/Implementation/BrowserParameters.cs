//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.10.2020
// Time: 16:50
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Code.Implementation
{
    public class BrowserParameters : IBrowserParameters
    {
        public Guid? ParentId { get; set; }
        public Guid? OwnerId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Guid? OrganizationId { get; set; }
    }
}
