using System;

namespace DocumentFlow.Data.Sync
{
    public class Command
    {
        public Guid id { get; set; }
        public DateTime date_updated { get; set; }
        public string script { get; set; }
    }
}
