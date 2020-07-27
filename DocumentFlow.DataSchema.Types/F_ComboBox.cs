//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.03.2019
// Time: 21:38
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema.Types
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using NHibernate;
    using Syncfusion.Windows.Forms.Tools;
    using DocumentFlow.Core;
    using DocumentFlow.Controls;
    using DocumentFlow.Data.Core;
    using DocumentFlow.DataSchema;
    using DocumentFlow.DataSchema.Types.Core;

    [Tag("ComboBox")]
    public class F_ComboBox : DatasetEditorControl<L_ComboBox, int?>
    {
        [JsonProperty("items")]
        public IList<ComboBoxItem> Items { get; set; } = new List<ComboBoxItem>();

        [JsonProperty("default")]
        public int? Default { get; set; }

        protected override void DefaultCreateControl(ISession session)
        {
            base.DefaultCreateControl(session);
            Control.SelectedIndexChanging += Control_SelectedIndexChanging;
        }

        protected override void PreparePopulate(ISession session, IDictionary row, IDictionary<string, Type> types, int status)
        {
            base.PreparePopulate(session, row, types, status);

            Control.ClearItems();

            try
            {
                IList<ComboBoxItem> items = string.IsNullOrEmpty(Dataset) ? Items : Db.ExecuteSelect<ComboBoxItem>(session, Dataset, types, (x) => row?[x]);
                foreach (var item in items)
                {
                    Control.Items.Add(item);
                }
            }
            catch (ParameterNotFoundException pe)
            {
                throw new Exception($"{DataField}: при выполнении запроса '{Dataset}' произошла ошибка - {pe.Message}", pe);
            }
        }

        protected override int? GetDefaultValue() => Default ?? null;

        protected override object GetValue() => (Control.SelectedItem as ComboBoxItem)?.Key;

        protected override void SetValue(object value)
        {
            if (value != null)
            {
                int key = Convert.ToInt32(value);
                Control.SelectedItem = Control.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Key == key);
            }
            else
                Control.SelectedItem = DefaultValue;
        }

        private void Control_SelectedIndexChanging(object sender, SelectedIndexChangingArgs e)
        {
            OnValueChanged();
        }
    }
}
