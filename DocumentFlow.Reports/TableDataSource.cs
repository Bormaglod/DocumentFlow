//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2021
// Time: 23:36
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace DocumentFlow.Reports
{
    public class TableDataSource
    {
        public string Name { get; set; }
        public string SelectCommand { get; set; }
        public List<string> Parameters { get; set; } = new List<string>();
        public List<Column> Columns { get; set; } = new List<Column>();
    }
}
