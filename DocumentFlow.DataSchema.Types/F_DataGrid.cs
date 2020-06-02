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
            foreach (Match match in Regex.Matches(Dataset, "(?<!:):([a-zA-Z_]+)"))
            {
                string prop = match.Groups[1].Value;
                CreateParameter(command, prop, row[prop], types.ContainsKey(prop) ? types[prop] : null);
            }

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            Control.DataAdapter = adapter;
            Control.InitializeProperties(row, status, Columns);
            Control.CreateModalWindow(session, FormTitle, Editor);
            Control.RefreshData();
        }

        private void CreateParameter(NpgsqlCommand command, string name, object value, Type type)
        {
            if (value == null && type == null)
                return;

            if (type == null)
                type = value.GetType();

            NpgsqlDbType dbType;

            if (type == typeof(Guid?) || type == typeof(Guid))
                dbType = NpgsqlDbType.Uuid;
            else if (type == typeof(int?) || type == typeof(int))
                dbType = NpgsqlDbType.Integer;
            else if (type == typeof(string))
                dbType = NpgsqlDbType.Varchar;
            else
                throw new Exception("Тип не обработан.");

            NpgsqlParameter parameter = command.Parameters.Add(name, dbType);
            if (value == null)
                parameter.NpgsqlValue = DBNull.Value;
            else
                parameter.NpgsqlValue = value;
        }

        /*public override void CreateControl(ISession session, SchemaCommand schema, Entity entity)
        {
            base.CreateControl(session, schema, entity);
            if (entity is DocumentInfo)
                Control.SetOwner(entity as DocumentInfo);
            Control.CreateAdditionalControls(session, this);
            Control.CreateModalWindow(session, schema, Editor, FormTitle);
        }

        public override void Initialize()
        {
            if (Control != null)
                Control.Initialize();
        }

        public override void UpdateControlStates(long status)
        {
            base.UpdateControlStates(status);
            foreach (GridColumn column in Control.Columns)
            {
                DatasetColumn c = Columns.FirstOrDefault(x => string.Compare(x.DataField, column.MappingName) == 0);
                if (c == null)
                    continue;

                column.Visible = !c.Hidden;
                if (c.States != null)
                {
                    if (c.States.Visible.Count > 0)
                        column.Visible = column.Visible && c.States.Visible.Contains(status);

                    if (c.States.Invisible.Count > 0)
                        column.Visible = column.Visible && !c.States.Invisible.Contains(status);
                }
            }

            foreach (CommandProperty cmd in Commands)
            {
                if (cmd.States != null)
                {
                    bool visible = true;
                    if (cmd.States.Visible.Count > 0)
                        visible = visible && cmd.States.Visible.Contains(status);

                    if (cmd.States.Invisible.Count > 0)
                        visible = visible && !cmd.States.Invisible.Contains(status);

                    Control.SetCommandVisible(cmd, visible);

                    bool enabled = true;
                    if (cmd.States.Enabled.Count > 0)
                        enabled = enabled && cmd.States.Enabled.Contains(status);

                    if (cmd.States.Disabled.Count > 0)
                        enabled = enabled && !cmd.States.Disabled.Contains(status);

                    Control.SetCommandEnabled(cmd, enabled);
                }
            }
        }

        protected override object GetValue() => items;

        protected override void SetValue(object value)
        {
            Type type = value.GetType();
            if (value == null || type.GetInterface("IList`1") == null)
                Control.DataSource = null;
            else
            {
                items = value;
                Type entityType = type.GetGenericArguments()[0];
                Type genericType = typeof(BindingList<>).MakeGenericType(entityType);
                Control.DataSource = Activator.CreateInstance(genericType, value);
            }
        }*/
    }
}
