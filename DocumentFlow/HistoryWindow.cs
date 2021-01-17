//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.05.2019
// Time: 21:15
//-----------------------------------------------------------------------

using System;
using System.Windows.Forms;
using DocumentFlow.Data.Repositories;

namespace DocumentFlow
{
    public partial class HistoryWindow : Form
    {
        private HistoryWindow(Guid entityId)
        {
            InitializeComponent();
            gridHistory.DataSource = Histories.Get(entityId);
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
