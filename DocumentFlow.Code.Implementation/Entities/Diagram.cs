using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Dapper;
using DocumentFlow.Code.System;
using DocumentFlow.Data.Base;

namespace DocumentFlow.Code.Implementation.DiagramImp
{
    public class Diagram : NameDataItem { }

    public class DiagramBrowser : IBrowserCode, IBrowserOperation
    {
        private const string baseSelect = "select id, name from transition";

        void IBrowserCode.Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;
            browser.CommandBarVisible = false;
            browser.DefineDoubleClickCommand("open-diagram");

            browser.CreateColumns((columns) =>
            {
                columns.CreateText("id", "Id")
                    .SetWidth(180)
                    .SetVisible(false);

                columns.CreateText("name", "Наименование")
                    .SetHideable(false)
                    .SetAutoSizeColumnsMode(SizeColumnsMode.LastColumnFill);

                columns.CreateSortedColumns()
                    .Add("name", ListSortDirection.Ascending);
            });

            ICommand open_diagram = browser.Commands.Add(CommandMethod.UserDefined, "open-diagram");
            open_diagram.Click += OpenDiagramClick;

            browser.ToolBar.AddCommand(open_diagram);

            string[] visible = new string[] { "refresh", "open-diagram" };
            foreach (ICommand cmd in browser.Commands)
            {
                cmd.SetVisible(visible.Contains(cmd.Code));
            }
        }

        IList IBrowserOperation.Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<Diagram>(baseSelect).AsList();
        }

        object IBrowserOperation.Select(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<Diagram>(baseSelect + " where id = :id", new { id });
        }

        int IBrowserOperation.Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            throw new NotImplementedException();
        }

        private void OpenDiagramClick(object sender, ExecuteEventArgs e)
        {
            if (e.Browser.CurrentRow is Diagram diagram)
            {
                e.Browser.Commands.OpenDiagram(diagram.id);
            }
        }
    }
}
