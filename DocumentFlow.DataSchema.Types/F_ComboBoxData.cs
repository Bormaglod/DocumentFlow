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
    using System.Linq;
    using NHibernate;
    using Syncfusion.Windows.Forms.Tools;
    using DocumentFlow.Controls;
    using DocumentFlow.Data.Core;
    using DocumentFlow.DataSchema.Types.Core;

    [Tag("ComboBoxData")]
    public class F_ComboBoxData : DatasetEditorControl<L_ComboBoxAdv, Guid?>
    {
        protected override void DefaultCreateControl(ISession session)
        {
            base.DefaultCreateControl(session);
            Control.SelectedIndexChanging += Control_SelectedIndexChanging;
        }

        protected override Guid? GetDefaultValue() => null;

        protected override void PreparePopulate(ISession session, IDictionary row, IDictionary<string, Type> types, int status)
        {
            base.PreparePopulate(session, row, types, status);

            Control.ClearItems();
            foreach (var item in Db.ExecuteSelect<ComboBoxDataItem>(session, Dataset, types, (x) => row == null ? null : row[x]))
            {
                Control.AddItem(item);
            }
        }

        protected override object GetValue() => (Control.SelectedItem as ComboBoxDataItem)?.Id;

        protected override void SetValue(object value)
        {
            if (value == null)
            {
                Control.SelectedItem = null;
            }
            else
            {
                Guid guid = (Guid)value;
                Control.SelectedItem = Control.Items.OfType<ComboBoxDataItem>().FirstOrDefault(x => x.Id == guid);
            }
        }

        private void Control_SelectedIndexChanging(object sender, SelectedIndexChangingArgs e)
        {
            OnValueChanged();
        }
    }
}
