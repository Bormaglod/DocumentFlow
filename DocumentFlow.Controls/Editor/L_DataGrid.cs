//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.06.2018
// Time: 21:04
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using NHibernate;
    using Npgsql;
    using Syncfusion.WinForms.DataGrid.Events;
    using DocumentFlow.Controls.Extensions;
    using DocumentFlow.Controls.Forms;
    using DocumentFlow.Data.Core;
    using DocumentFlow.DataSchema;
    
    public partial class L_DataGrid : UserControl
    {
        private ModalEditor editor;
        private IDictionary row;
        private int status;

        public L_DataGrid()
        {
            InitializeComponent();
        }

        public NpgsqlDataAdapter DataAdapter { get; set; }

        public void InitializeProperties(IDictionary ownerRow, int ownerStatus, IList<DatasetColumn> columns)
        {
            row = ownerRow;
            status = ownerStatus;

            gridMain.Columns.Clear();
            if (columns != null)
            {
                foreach (DatasetColumn c in columns)
                {
                    gridMain.CreateColumn(c);
                }

                gridMain.CreateSummaryRow(columns);
            }
        }

        public void CreateModalWindow(ISession session, string title, DatasetEditor schema)
        {
            editor = new ModalEditor(session, title, schema)
            {
                Text = title
            };
        }

        public void RefreshData()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Db.ConnectionString))
            {
                DataAdapter.SelectCommand.Connection = connection;

                DataSet ds = new DataSet();
                DataAdapter.Fill(ds);
                gridMain.DataSource = ds.Tables[0];
            }
        }

        private void Edit()
        {
            if (gridMain.CurrentItem == null)
                return;

            DataRowView rowView = (DataRowView)gridMain.CurrentItem;
            long id = Convert.ToInt64(rowView.Row["id"]);
            if (editor.Edit(id, status))
                RefreshData();
        }

        private void ExecuteDoubleClickCommand(object sender, CellClickEventArgs e)
        {
            Edit();
        }

        private void bttonCreate_Click(object sender, EventArgs e)
        {
            if (editor.Create((Guid)row["id"], status))
                RefreshData();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DataRowView rowView = (DataRowView)gridMain.CurrentItem;
            long id = Convert.ToInt64(rowView.Row["id"]);
            if (editor.Delete(id, status))
                RefreshData();
        }
    }
}
