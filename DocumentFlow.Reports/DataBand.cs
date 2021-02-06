//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2021
// Time: 23:36
//-----------------------------------------------------------------------

namespace DocumentFlow.Reports
{
    public class DataBand : Band 
    {
        private int currentRow = 0;

        public string DataSource { get; set; }

        public void Reset() => currentRow = 0;

        public void NextRow() => currentRow++;

        public bool EOL() => currentRow >= Page.Report.Storage.Count(DataSource);

        public override object Get(string source, string field) => Page.Report.Storage.Get(source, field, currentRow);
    }
}
