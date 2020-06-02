//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.05.2019
// Time: 21:15
//-----------------------------------------------------------------------

namespace DocumentFlow
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using NHibernate;
    using Syncfusion.Windows.Forms;
    using DocumentFlow.Data.Entities;

    public partial class HistoryWindow : MetroForm
    {
        public HistoryWindow(ISession session, Guid entityId)
        {
            InitializeComponent();
            
            using (ITransaction transaction = session.BeginTransaction())
            {
                IList<History> history = session.QueryOver<History>()
                    .Where(x => x.ReferenceId == entityId)
                    .OrderBy(x => x.Changed).Desc
                    .List();

                gridHistory.DataSource = new BindingList<History>(history);
            }
        }

        private void SfButton1_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
