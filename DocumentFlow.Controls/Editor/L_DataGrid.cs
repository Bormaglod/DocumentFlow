//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.06.2018
// Time: 21:04
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid.Events;
using DocumentFlow.Controls.Forms;
using DocumentFlow.Data.Core;
using DocumentFlow.Code;
using DocumentFlow.Code.Implementation;

namespace DocumentFlow.Controls.Editor
{
    public partial class L_DataGrid : UserControl, IGridDataControl
    {
        private Func<IDbConnection, IList> getItemsFunc;
        private Type entityType;
        private Guid owner;

        public L_DataGrid(Guid ownerId, Func<IDbConnection, IList> getItems)
        {
            InitializeComponent();
            gridMain.Style.ProgressBarStyle.AllowForegroundSegments = true;
            Columns = new GridColumnCollection(gridMain, null);
            getItemsFunc = getItems;
            owner = ownerId;
        }

        public IColumnCollection Columns { get; }

        public IEditorCode Editor { get; set; }

        public string HeaderText { get; set; }

        public Action<object> CheckValues { get; set; }

        public void RefreshData()
        {
            using (var conn = Db.OpenConnection())
            {
                IList list = getItemsFunc(conn);
                entityType = list.GetType().GetGenericArguments().First();
                gridMain.DataSource = list;
            }
        }

        private void Edit()
        {
            ModalEditor editorForm = new ModalEditor(owner, HeaderText, CheckValues);
            if (gridMain.SelectedItem is IDetail detail && editorForm.Edit(Editor, detail))
            {
                RefreshData();
            }
        }

        private void ExecuteDoubleClickCommand(object sender, CellClickEventArgs e)
        {
            Edit();
        }

        private void BttonCreate_Click(object sender, EventArgs e)
        {
            ModalEditor editorForm = new ModalEditor(owner, HeaderText, CheckValues);
            if (editorForm.Create(Editor, entityType))
            {
                RefreshData();
            }
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            ModalEditor editorForm = new ModalEditor(owner, HeaderText, CheckValues);
            if (gridMain.SelectedItem is IDetail detail && editorForm.Delete(Editor, detail))
            {
                RefreshData();
            }
        }
    }
}
