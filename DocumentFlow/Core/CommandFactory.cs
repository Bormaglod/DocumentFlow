//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.03.2020
// Time: 18:23
//-----------------------------------------------------------------------

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Entities;
using DocumentFlow.Code;
using DocumentFlow.Code.Implementation;

namespace DocumentFlow
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IContainerPage container;
        private readonly IEnumerable<Command> commands;

        public CommandFactory(IContainerPage container)
        {
            this.container = container;

            using (var conn = Db.OpenConnection())
            {
                string sql = "select * from command c left join picture p on (p.id = c.picture_id) left join entity_kind ek on (ek.id = c.entity_kind_id)";
                commands = conn.Query<Command, Picture, EntityKind, Command>(sql, (command, picture, entity) =>
                {
                    command.Picture = picture;
                    command.EntityKind = entity;
                    return command;
                });
            }
        }

        #region ICommandFactory implementation

        Command ICommandFactory.this[string code] => commands.FirstOrDefault(x => x.code == code);

        IEnumerable<Command> ICommandFactory.Commands => commands;

        void ICommandFactory.Execute(Command command, params object[] parameters)
        {
            switch (command.code)
            {
                case "profile":
                    break;
                case "add-record":
                    OpenEditor(parameters);
                    break;
                case "edit-record":
                    OpenEditor(parameters);
                    break;
                case "view-picture":
                case "documents-schema":
                    OpenDataViewer(command);
                    break;
                case "open-diagram":
                    OpenDiagram(parameters);
                    break;
                case "open-document":
                    OpenDocument(parameters);
                    break;
                case "open-browser-code":
                    OpenCodeEditor(parameters);
                    break;
                default:
                    string sql = @"with recursive r as 
                        (
	                        select id, parent_id from command where code in ('view-directory', 'view-document', 'report-commands')
	                        union all
	                        select c.id, c.parent_id from command c join r on (r.id = c.parent_id)
                        ) select id from r where id = :id";

                    bool createViewer = false;
                    using (var conn = Db.OpenConnection())
                    {
                        createViewer = conn.Query(sql, new { command.id }).SingleOrDefault() != null;
                    }

                    if (createViewer)
                    {
                        OpenDataViewer(command);
                    }

                    break;
            }
        }

        void ICommandFactory.Execute(string command, params object[] parameters)
        {
            using (var conn = Db.OpenConnection())
            {
                Command cmd = commands.SingleOrDefault(x => x.code == command);
                if (cmd != null)
                {
                    ((ICommandFactory)this).Execute(cmd, parameters);
                }
                else
                {
                    switch (command)
                    {
                        case "logout":
                            container.Logout();
                            break;
                        case "about":
                            container.About();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        #endregion

        private void OpenDataViewer(Command command)
        {
            IPage page = container.Get<ContentViewer>(command.id);
            if (page != null)
            {
                container.Selected = page;
            }
            else
            {
                try
                {
                    ContentViewer viewer = new ContentViewer(container, this, command);
                    container.Add(viewer);
                }
                catch (AggregateException e)
                {
                    e.Handle((x) =>
                    {
                        if (x is CompilerException xe)
                        {
                            if (MessageBox.Show("Код содержащий описание окна содержит ошибки, поэтому окно не может быть создано. Вы хотите исправить ошибки?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                            {
                                OpenCodeEditor(command, xe.Errors);
                            }

                            return true;
                        }

                        if (x is EmptyCodeException xc)
                        {
                            if (MessageBox.Show($"{xc.Message}\nОткрыть окно для создания кода?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                            {
                                OpenCodeEditor(command);
                            }

                            return true;
                        }

                        return false;
                    });
                }
                catch (SqlExecuteException e)
                {
                    if (MessageBox.Show($"{ExceptionHelper.Message(e)}\nОткрыть окно для создания кода?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        OpenCodeEditor(command);
                    }
                }
            }
        }

        private void OpenDiagram(Guid id)
        {
            IPage page = container.Get<DiagramViewer>(id);
            if (page != null)
            {
                container.Selected = page;
            }
            else
            {
                DiagramViewer viewer = new DiagramViewer(container, id);
                container.Add(viewer);
            }
        }

        private void OpenDiagram(params object[] parameters)
        {
            if (parameters.Length != 1)
            {
                return;
            }

            if (parameters[0] is Dictionary<string, object> parameterValues)
            {
                if (!parameterValues.ContainsKey("id"))
                {
                    throw new ArgumentException("Ожидался параметр id", "id");
                }

                if (parameterValues["id"] is Guid id)
                {
                    OpenDiagram(id);
                }
                else
                    throw new ArgumentException("Аргумент id должен быть типа Guid", "id");
            }
            else if (parameters[0] is Guid id)
            {
                OpenDiagram(id);
            }
            else
                throw new ArgumentException("Аргумент id должен быть типа Guid", "id");
        }

        private void OpenDocument(Guid id)
        {
            IPage page = null;
            if (id != Guid.Empty)
            {
                page = container.Get<ContentEditor>(id);
            }

            if (page != null)
            {
                container.Selected = page;
            }
            else
            {
                using (var conn = Db.OpenConnection())
                {
                    string sql = "select c.*, ek.* from command c join entity_kind ek on (ek.id = c.entity_kind_id) join document_info di on (ek.id = di.entity_kind_id and di.id = :id)";
                    Command command = conn.Query<Command, EntityKind, Command>(sql, (cmd, entity_kind) =>
                    {
                        cmd.EntityKind = entity_kind;
                        return cmd;
                    }, new { id }).Single();

                    string parent = conn.Query<string>("select get_root_parent(:table)", new { table = command.EntityKind.code }).Single();
                    if (parent == "document")
                    {
                        sql = $"select owner_id, organization_id from {command.EntityKind.code} where id = :id";
                    }
                    else
                    {
                        sql = $"select owner_id, parent_id from {command.EntityKind.code} where id = :id";
                    }

                    var row = conn.QuerySingle(sql, new { id });

                    BrowserParameters editorParams = new BrowserParameters()
                    {
                        ParentId = parent == "document" ? null : row.parent_id,
                        OwnerId = row.owner_id,
                        OrganizationId = parent == "directory" ? null : row.organization_id
                    };

                    try
                    {
                        ContentEditor e = new ContentEditor(container, null, this, id, command, editorParams);
                        container.Add(e);
                    }
                    catch (Exception e)
                    {
                        ExceptionHelper.MesssageBox(e);
                    }
                }
            }
        }

        private void OpenDocument(params object[] parameters)
        {
            if (parameters.Length != 1)
            {
                return;
            }

            if (parameters[0] is Dictionary<string, object> parameterValues)
            {
                if (!parameterValues.ContainsKey("id"))
                {
                    throw new ArgumentException("Ожидался параметр id", "id");
                }

                if (parameterValues["id"] is Guid id)
                {
                    OpenDocument(id);
                }
                else
                    throw new ArgumentException("Аргумент id должен быть типа Guid", "id");
            }
            else if (parameters[0] is Guid id)
            {
                OpenDocument(id);
            }
            else
                throw new ArgumentException("Аргумент id должен быть типа Guid", "id");
        }

        private void OpenEditor(params object[] parameters)
        {
            if (parameters.Length != 4)
            {
                return;
            }

            if (parameters[0] is IBrowser browser && parameters[1] is Guid id && parameters[2] is Command command && parameters[3] is IBrowserParameters editorParams)
            {
                IPage page = null;
                if (id != Guid.Empty)
                {
                    page = container.GetAll<ContentEditor>()
                        .OfType<IPage>()
                        .Where(x => x.InfoId == id)
                        .FirstOrDefault();
                }

                if (page != null)
                {
                    container.Selected = page;
                }
                else
                {
                    try
                    {
                        ContentEditor e = new ContentEditor(container, browser, this, id, command, editorParams);
                        container.Add(e);
                    }
                    catch (Exception e)
                    {
                        if (MessageBox.Show($"{ExceptionHelper.Message(e)}\nОткрыть окно для создания кода?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                        {
                            OpenCodeEditor(command);
                        }
                    }
                }
            }
        }

        private void OpenCodeEditor(params object[] parameters)
        {
            if (parameters.Length == 0)
            {
                return;
            }

            Command command = null;
            if (parameters[0] is Command cmd)
            {
                command = cmd;
            }
            else if (parameters[0] is Guid id)
            {
                using (var conn = Db.OpenConnection())
                {
                    command = conn.QuerySingle<Command>("select * from command where id = :id", new { id });
                }
            }
            else if (parameters[0] is Dictionary<string, object> parameterValues)
            {
                if (!parameterValues.ContainsKey("id"))
                {
                    throw new ArgumentException("Ожидался параметр id", "id");
                }

                if (parameterValues["id"] is Guid pid)
                {
                    using (var conn = Db.OpenConnection())
                    {
                        command = conn.QuerySingle<Command>("select * from command where id = :id", new { id = pid });
                    }
                }
                else
                    throw new ArgumentException("Аргумент id должен быть типа Guid", "id");
            }
            else
                throw new ArgumentException("Аргумент id должен быть типа Guid", "id");

            if (command != null)
            {
                IPage page = container.Get<CodeEditor>(command.id);
                if (page != null)
                {
                    container.Selected = page;
                }
                else
                {
                    page = new CodeEditor(container, command);
                    container.Add(page);
                }

                if (parameters.Length > 1 && parameters[1] is CompilerErrorCollection errors)
                {
                    (page as CodeEditor).ShowErrors(errors);
                }
            }
        }
    }
}
