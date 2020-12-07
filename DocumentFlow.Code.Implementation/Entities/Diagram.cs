using System;
using System.Linq;
using DocumentFlow.Code.Core;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code.Implementation.DiagramImp
{
    public class Diagram : IDirectory
    {
        public Guid id { get; protected set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        object IIdentifier.oid
        {
            get { return id; }
        }
    }

    public class DiagramBrowser : BrowserCodeBase<Diagram>, IBrowserCode
    {
        private const string baseSelect = "select id, name from transition";

        public void Initialize(IBrowser browser)
        {
            browser.DataType = DataType.Directory;
            browser.CommandBarVisible = false;
            browser.DefineDoubleClickCommand("open-diagram");

            ICommandAdded open_diagram = browser.Commands.Add(CommandMethod.UserDefined, "open-diagram", "toolbar");
            open_diagram.Click += OpenDiagramClick;

            string[] visible = new string[] { "refresh", "open-diagram" };
            foreach (ICommand cmd in browser.Commands)
            {
                cmd.SetVisible(visible.Contains(cmd.Code));
            }

            browser.CreateColumns((columns) =>
            {
                columns.CreateText("id", "Id")
                    .SetWidth(180)
                    .SetVisible(false);

                columns.CreateText("name", "Наименование")
                    .SetHideable(false)
                    .SetAutoSizeColumnsMode(SizeColumnsMode.LastColumnFill);

                columns.CreateSortedColumns()
                    .Add("name", SortDirection.Ascending);
            });
        }

        private void OpenDiagramClick(object sender, ExecuteEventArgs e)
        {
            Diagram diagram = e.Browser.CurrentRow as Diagram;
            if (diagram != null)
            {
                e.Browser.Commands.OpenDiagram(diagram.id);
            }
        }

        protected override string GetSelect()
        {
            return baseSelect;
        }

        protected override string GetSelectById()
        {
            return baseSelect + " where id = :id";
        }

        public IEditorCode CreateEditor()
        {
            return null;
        }
    }
}
