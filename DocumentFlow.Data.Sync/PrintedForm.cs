using System;

namespace DocumentFlow.Data.Sync
{
    public class PrintedForm
    {
        public Guid id { get; set; }
        public DateTime date_updated { get; set; }
        public string schema_form { get; set; }
    }
}
