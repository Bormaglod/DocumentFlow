//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.03.2020
// Time: 18:23
//-----------------------------------------------------------------------

using System;
using System.CodeDom.Compiler;
using System.Collections;
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
                        createViewer = conn.QuerySingleOrDefault<Guid>(sql, new { command.id }) != default;
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
            ICommandFactory factory = this;
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
                            factory.Logout();
                            break;
                        case "about":
                            factory.About();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        IPage ICommandFactory.OpenDiagram(Guid id)
        {
            IPage page = container.Get<DiagramViewer>(id);
            if (page != null)
            {
                container.Selected = page;
                return page;
            }
            else
            {
                DiagramViewer viewer = new DiagramViewer(container, id);
                container.Add(viewer);
                return viewer;
            }
        }

        IPage ICommandFactory.OpenDiagram(IIdentifier<Guid> identifier)
        {
            ICommandFactory factory = this;
            return factory.OpenDiagram(identifier.id);
        }

        IPage ICommandFactory.OpenDiagram(Hashtable parameters)
        {
            if (!parameters.ContainsKey("id"))
            {
                throw new ArgumentException("Ожидался параметр id", "id");
            }

            ICommandFactory factory = this;
            if (parameters["id"] is Guid id)
            {
                return factory.OpenDiagram(id);
            }
            else
            {
                throw new ArgumentException("Аргумент id должен быть типа Guid", "id");
            }
        }

        IPage ICommandFactory.OpenDocument(Guid id)
        {
            IPage page = null;
            if (id != Guid.Empty)
            {
                page = container.Get<ContentEditor>(id);
            }

            if (page != null)
            {
                container.Selected = page;
                return page;
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
                        return e;
                    }
                    catch (Exception e)
                    {
                        ExceptionHelper.MesssageBox(e);
                        return null;
                    }
                }
            }
        }

        IPage ICommandFactory.OpenDocument(Hashtable parameters)
        {
            if (!parameters.ContainsKey("id"))
            {
                throw new ArgumentException("Ожидался параметр id", "id");
            }

            ICommandFactory factory = this;
            if (parameters["id"] is Guid id)
            {
                return factory.OpenDocument(id);
            }
            else
            {
                throw new ArgumentException("Аргумент id должен быть типа Guid", "id");
            }
        }

        IPage ICommandFactory.OpenEditor(IBrowser browser, Guid id, Command command, IBrowserParameters editorParams)
        {
            ICommandFactory factory = this;
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
                return page;
            }
            else
            {
                try
                {
                    ContentEditor e = new ContentEditor(container, browser, this, id, command, editorParams);
                    container.Add(e);
                    return e;
                }
                catch (MissingImpException)
                {
                    return null;
                }
                catch (Exception e)
                {
                    if (MessageBox.Show($"{ExceptionHelper.Message(e)}\nОткрыть окно для создания кода?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        factory.OpenCodeEditor(command);
                    }

                    return null;
                }
            }
        }

        IPage ICommandFactory.OpenEditor(IBrowser browser, IIdentifier<Guid> identifier, Command command, IBrowserParameters editorParams)
        {
            ICommandFactory factory = this;
            return factory.OpenEditor(browser, identifier.id, command, editorParams);
        }

        IPage ICommandFactory.OpenCodeEditor(Command command, CompilerErrorCollection errors)
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

            if (errors != null && page is CodeEditor codeEditor)
            {
                codeEditor.ShowErrors(errors);
            }

            return page;
        }

        IPage ICommandFactory.OpenCodeEditor(Guid id, CompilerErrorCollection errors)
        {
            ICommandFactory factory = this;
            using (var conn = Db.OpenConnection())
            {
                Command command = conn.QuerySingle<Command>("select * from command where id = :id", new { id });
                return factory.OpenCodeEditor(command, errors);
            }
        }

        IPage ICommandFactory.OpenCodeEditor(Hashtable parameters, CompilerErrorCollection errors)
        {
            ICommandFactory factory = this;
            if (!parameters.ContainsKey("id"))
            {
                throw new ArgumentException("Ожидался параметр id", "id");
            }

            if (parameters["id"] is Guid pid)
            {
                return factory.OpenCodeEditor(pid, errors);
            }
            else
            {
                throw new ArgumentException("Аргумент id должен быть типа Guid", "id");
            }
        }

        void ICommandFactory.Logout()
        {
            container.Logout();
        }

        void ICommandFactory.About()
        {
            container.About();
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

        private IPage OpenDiagram(params object[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentException($"Команда OpenDiagram должжна иметь 1 параметр, а передано {parameters.Length}");
            }

            ICommandFactory factory = this;
            switch (parameters[0])
            {
                case Hashtable hashtable: return factory.OpenDiagram(hashtable);
                case Guid id: return factory.OpenDiagram(id);
                case IIdentifier<Guid> identifier: return factory.OpenDiagram(identifier);
                default:
                    throw new ArgumentException("Аргумент id должен быть типа Guid", "id");
            }
        }

        private IPage OpenDocument(params object[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentException($"Команда OpenDocument должжна иметь 1 параметр, а передано {parameters.Length}");
            }

            ICommandFactory factory = this;
            switch (parameters[0])
            {
                case Hashtable hashtable: return factory.OpenDocument(hashtable);
                case Guid id: return factory.OpenDocument(id);
                default:
                    throw new ArgumentException("Аргумент id должен быть типа Guid", "id");
            }
        }

        private IPage OpenEditor(params object[] parameters)
        {
            if (parameters.Length != 4)
            {
                throw new ArgumentException($"Команда OpenEditor должжна иметь 4 параметра, а передано {parameters.Length}");
            }

            ICommandFactory factory = this;
            if (parameters[0] is IBrowser browser && parameters[2] is Command command && parameters[3] is IBrowserParameters editorParams)
            {
                switch (parameters[1])
                {
                    case Guid id: return factory.OpenEditor(browser, id, command, editorParams);
                    case IIdentifier<Guid> identifier: return factory.OpenEditor(browser, identifier, command, editorParams);
                }
            }

            throw new ArgumentException("В команду OpenEditor переданы неверные тип(ы) параметра(ов).");
        }

        private IPage OpenCodeEditor(params object[] parameters)
        {
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Вызов команды open-browser-code без параметров.");
            }

            CompilerErrorCollection errors = null;
            if (parameters.Length > 1)
            {
                errors = parameters[1] as CompilerErrorCollection;
            }

            ICommandFactory factory = this;
            switch (parameters[0])
            {
                case Command cmd: return factory.OpenCodeEditor(cmd, errors);
                case Guid id: return factory.OpenCodeEditor(id, errors);
                case Hashtable hashtable: return factory.OpenCodeEditor(hashtable, errors);
                default: 
                    throw new ArgumentException("Аргумент id должен быть типа Guid", "id");
            }
        }
    }
}
