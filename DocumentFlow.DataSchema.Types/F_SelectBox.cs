//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.03.2019
// Time: 22:49
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema.Types
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using NHibernate;
    using DocumentFlow.Core;
    using DocumentFlow.Controls;
    using DocumentFlow.Controls.Forms;
    using DocumentFlow.Data.Core;
    using DocumentFlow.DataSchema.Types.Core;

    [Tag("SelectBox")]
    public class F_SelectBox : DatasetEditorControl<L_SelectBox, Guid?>
    {
        class SelectBoxItem
        {
            public Guid? Id { get; set; }
            public long Status_Id { get; set; }
            public string Name { get; set; }
            public Guid? Parent_Id { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        [DefaultValue(SelectView.Tree)]
        [JsonProperty("select-view", DefaultValueHandling = DefaultValueHandling.Populate)]
        public SelectView SelectView { get; set; }

        protected override void DefaultCreateControl(ISession session)
        {
            base.DefaultCreateControl(session);
            Control.ValueChanged += Control_ValueChanged;
            Control.SelectView = SelectView;
        }

        private void Control_ValueChanged(object sender, SelectBoxValueChanged e)
        {
            OnValueChanged();
        }

        protected override Guid? GetDefaultValue() => null;

        protected override void PreparePopulate(ISession session, IDictionary row, IDictionary<string, Type> types, int status)
        {
            base.PreparePopulate(session, row, types, status);

            Control.Clear();
            if (!string.IsNullOrEmpty(Dataset))
            {
                try
                {
                    AddItems(Db.ExecuteSelect<SelectBoxItem>(session, Dataset, types, (x) => row?[x]), null);
                }
                catch (ParameterNotFoundException pe)
                {
                    throw new Exception($"{DataField}: при выполнении запроса '{Dataset}' произошла ошибка - {pe.Message}", pe);
                }
            }
        }

        protected override object GetValue() => (Control.SelectedItem as SelectBoxItem)?.Id;

        protected override void SetValue(object value)
        {
            if (value == null)
            {
                Control.SelectedItem = null;
            }
            else
            {
                Guid guid = (Guid)value;
                Control.SelectedItem = Control.Items.OfType<SelectBoxItem>().FirstOrDefault(x => x.Id == guid);
            }
        }

        private void AddItems(IList<SelectBoxItem> list, SelectBoxItem parent)
        {
            foreach (SelectBoxItem item in list.Where(x => x.Parent_Id == parent?.Id))
            {
                Control.AddItem(item, parent, item.Status_Id == 500);
                AddItems(list, item);
            }
        }
    }
}
