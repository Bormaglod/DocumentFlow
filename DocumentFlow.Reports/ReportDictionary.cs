//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2021
// Time: 23:36
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace DocumentFlow.Reports
{
    public class ReportDictionary
    {
        public List<Connection> Connections { get; set; } = new();
        public List<CommandParameter> Parameters { get; set; } = new();
        public Connection GetConnection(string source)
        {
            foreach (Connection connection in Connections)
            {
                TableDataSource tableDataSource = connection.Sources.FirstOrDefault(x => x.Name == source);
                if (tableDataSource != null)
                {
                    return connection;
                }
            }

            return null;
        }
    }
}
