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
    public class DataStorage : IDataStorage
    {
        private readonly Dictionary<string, List<Dictionary<string, object>>> values;

        public DataStorage() => values = new Dictionary<string, List<Dictionary<string, object>>>();

        int IDataStorage.Count(string source) => values[source].Count;

        public object Get(string source, string field, int rowindex) => values[source][rowindex][field];

        public object Get(string source, string field) => values[source][0][field];

        public void AddSource(string source) => values.Add(source, new List<Dictionary<string, object>>());

        public void AddRow(string source, Dictionary<string, object> row) => values[source].Add(row);
    }
}
