//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.06.2019
// Time: 10:28
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema.Types
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;
    using NHibernate;
    using Npgsql;
    using NpgsqlTypes;
    using DocumentFlow.Controls;
    using DocumentFlow.Data.Core;
    using DocumentFlow.DataSchema.Types.Core;

    [Tag("DataGrid")]
    public class F_DataGrid : EditorControl<L_DataGrid>, IPopulated
    {
        [JsonProperty("columns")]
        public IList<DatasetColumn> Columns { get; set; }

        [JsonProperty("editor")]
        public DatasetEditor Editor { get; set; }

        [JsonProperty("form-title")]
        public string FormTitle { get; set; }

        [JsonProperty("dataset")]
        public string Dataset { get; set; }

        void IPopulated.Populate(ISession session, IDictionary row, IDictionary<string, Type> types, int status)
        {
            if (string.IsNullOrEmpty(Dataset))
                return;

            NpgsqlCommand command = new NpgsqlCommand(Dataset);
            foreach (Match match in Regex.Matches(Dataset, Db.ParameterPattern))
            {
                string prop = match.Groups[1].Value;
                command.CreateParameter(prop, row[prop], types.ContainsKey(prop) ? types[prop] : null);
            }

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            Control.DataAdapter = adapter;
            Control.InitializeProperties(row, status, Columns);
            Control.CreateModalWindow(session, FormTitle, Editor);
            Control.RefreshData();
        }
    }
}
