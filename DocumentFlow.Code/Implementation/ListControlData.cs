//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.10.2020
// Time: 21:35
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DocumentFlow.Data;

namespace DocumentFlow.Code.Implementation
{
    public class ListControlData : BindingControlData
    {
        private readonly IEditor editor;
        private readonly Action<IEnumerable<IIdentifier>> addItems;
        private readonly ChoiceItems getItems2;
        private readonly СriterionChoiceItems getItems3;

        public ListControlData(Control control, ChoiceItems getItems, Action<IEnumerable<IIdentifier>> addItems) : base(control) 
        {
            this.getItems2 = getItems;
            this.addItems = addItems;
        }

        public ListControlData(IEditor editor, Control control, СriterionChoiceItems getItems, Action<IEnumerable<IIdentifier>> addItems) : base(control)
        {
            this.editor = editor;
            this.getItems3 = getItems;
            this.addItems = addItems;
        }

        public override void Populate(IDbConnection connection, object data)
        {
            if (getItems2 != null)
                addItems(getItems2(connection));
            else
                addItems(getItems3(editor, connection));
 
            base.Populate(connection, data);
        }
    }
}
