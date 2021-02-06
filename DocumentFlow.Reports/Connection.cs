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
    public enum ConnectionStringType { Current, Embeded, Specified }
    public class Connection
    {
        public ConnectionStringType ConnectionStringType { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public List<TableDataSource> Sources { get; set; } = new List<TableDataSource>();
    }
}
