﻿//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2021
// Time: 23:36
//-----------------------------------------------------------------------

namespace DocumentFlow.Reports
{
    public class DataBandComplex
    {
        public Band Header { get; set; }
        public DataBand DataBand { get; set; }
        public Band Footer { get; set; }
    }
}
