//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.01.2021
// Time: 14:27
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace DocumentFlow.Code
{
    public interface IReportForm
    {
        string Name { get; }
        bool IsDefault { get; }
        IReportForm Parameter(string name, object value);
        object GetParameter(string name);
    }

    public class ReportForm : IReportForm
    {
        private readonly Dictionary<string, object> values = new Dictionary<string, object>();

        private ReportForm() { }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        IReportForm IReportForm.Parameter(string name, object value)
        {
            values.Add(name, value);
            return this;
        }

        object IReportForm.GetParameter(string name) => values[name];

        public static IReportForm Create(string name, bool isDefault = false)
        {
            ReportForm form = new ReportForm()
            {
                Name = name,
                IsDefault = isDefault
            };

            return form;
        }
    }

    public interface IReportCollection
    {
        IEnumerable<IReportForm> Forms { get; }
        IReportForm Default { get; }
        IReportCollection Add(IReportForm form);
    }
}
