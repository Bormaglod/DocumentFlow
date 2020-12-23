//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.05.2019
// Time: 21:15
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Entities;

namespace DocumentFlow
{
    public partial class HistoryWindow : Form
    {
        private HistoryWindow(Guid entityId)
        {
            InitializeComponent();
            
            using (var conn = Db.OpenConnection())
            {
                string sql = "select ua.name as user_name, h.*, fs.*, ts.* from history h join status fs on (fs.id = h.from_status_id) join status ts on (ts.id = h.to_status_id) join user_alias ua on (ua.id = h.user_id) where reference_id = :id order by changed desc";
                IEnumerable<History> list = conn.Query<History, Status, Status, History>(sql, (history, fromStatus, toStatus) => {
                    history.FromStatus = fromStatus;
                    history.ToStatus = toStatus;
                    return history;
                }, new { id = entityId });
                gridHistory.DataSource = list;
            }
        }

        public static void ShowWindow(Guid entityId)
        {
            HistoryWindow win = new HistoryWindow(entityId);
            win.ShowDialog();
        }

        private void SfButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
