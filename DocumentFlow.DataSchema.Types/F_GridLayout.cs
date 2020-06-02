//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.06.2019
// Time: 21:10
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema.Types
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using NHibernate;
    using DocumentFlow.DataSchema.Types.Core;

    public class F_GridLayoutRow
    {
        [JsonProperty("columns")]
        public IList<IEditorControl> Columns { get; set; }
    }

    [Tag("Grid")]
    public class F_GridLayout : EditorControl<TableLayoutPanel>, IContainer
    {
        [JsonProperty("row-count")]
        public int RowCount { get; set; }

        [JsonProperty("column-count")]
        public int ColumnCount { get; set; }

        [JsonProperty("rows")]
        public IList<F_GridLayoutRow> Rows { get; set; } = new List<F_GridLayoutRow>();

        IList<IEditorControl> IContainer.Controls => ((IContainer)this).GetControls(true);

        IList<IEditorControl> IContainer.GetControls(bool withChilds)
        {
            List<IEditorControl> list = new List<IEditorControl>();
            foreach (F_GridLayoutRow row in Rows)
            {
                list.AddRange(row.Columns);
                foreach (IContainer control in row.Columns.OfType<IContainer>())
                {
                    list.AddRange(control.GetControls(true));
                }
            }

            return list;
        }

        protected override void DefaultCreateControl(ISession session)
        {
            base.DefaultCreateControl(session);
            Control.RowCount = RowCount;
            Control.ColumnCount = ColumnCount;

            for (int i = 0; i < ColumnCount; i++)
            {
                SizeType size = i == ColumnCount - 1 ? SizeType.Percent : SizeType.AutoSize;

                if (i < Control.ColumnStyles.Count)
                    Control.ColumnStyles[i] = new ColumnStyle(size);
                else
                    Control.ColumnStyles.Add(new ColumnStyle(size));

                if (size == SizeType.Percent)
                    Control.ColumnStyles[i].Width = 100;
            }

            for (int i = 0; i < RowCount; i++)
            {
                if (i < Control.RowStyles.Count)
                    Control.RowStyles[i] = new RowStyle(SizeType.AutoSize);
                else
                    Control.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            int rowIdx = 0;
            foreach (F_GridLayoutRow row in Rows)
            {
                int columnIdx = 0;
                foreach (IEditorControl control in row.Columns)
                {
                    control.CreateControl(session, Control, null);
                    Control.SetColumn(control.Control, columnIdx++);
                    Control.SetRow(control.Control, rowIdx);
                }

                rowIdx++;
            }
        }
    }
}
