//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.11.2020
// Time: 20:40
//-----------------------------------------------------------------------

using System;
using System.Data;
using System.Windows.Forms;

namespace DocumentFlow.Code.Implementation
{
    public class GridControlData : ControlData, IDataGrid, IDataName
    {
        private readonly string gridName;
        private readonly IColumnCollection gridColumns;

        public GridControlData(Control control, string name, IColumnCollection columns) : base(control)
        {
            gridColumns = columns;
            gridName = name;
        }

        public event EventHandler AfterPopulation;

        string IDataName.Name => gridName;

        IControl IPopulate.AfterPopulationAction(EventHandler afterPopulation)
        {
            AfterPopulation = afterPopulation;
            return this;
        }

        void IPopulate.DoAfterPopulation() => AfterPopulation?.Invoke(this, EventArgs.Empty);

        void IPopulate.Populate(IDbConnection connection, object data)
        {
            if (Owner is IGridDataControl control)
            {
                control.RefreshData();
            }
        }
        
        IDataGrid IDataGrid.CreateColumns(Action<IColumnCollection> createColumns)
        {
            createColumns(gridColumns);
            return this;
        }

        IDataGrid IDataGrid.SetEditor(string headerText, IEditorCode editor, Action<object> checkValues)
        {
            if (Owner is IGridDataControl control)
            {
                control.Editor = editor;
                control.HeaderText = headerText;
                control.CheckValues = checkValues;
            }

            return this;
        }
    }
}
