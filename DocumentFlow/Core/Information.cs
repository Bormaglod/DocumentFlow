//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.12.2020
// Time: 17:10
//-----------------------------------------------------------------------

using System;
using DocumentFlow.Code;

namespace DocumentFlow.Core
{
    public class Information : IInformation
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Changed { get; set; }
        public string Author { get; set; }
        public string Editor { get; set; }
        public string StatusCode { get; set; }
    }
}
