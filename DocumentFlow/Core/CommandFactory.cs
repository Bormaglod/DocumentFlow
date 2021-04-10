//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.03.2020
// Time: 18:23
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.CodeAnalysis;
using Dapper;
using DocumentFlow.Code;
using DocumentFlow.Code.Implementation;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Entities;
using DocumentFlow.Data.Repositories;
using DocumentFlow.Interfaces;

namespace DocumentFlow
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IContainerPage container;
        private readonly IEnumerable<Command> commands;

        public CommandFactory(IContainerPage container)
        {
            this.container = container;

            commands = Commands.GetAll();
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

        IPage ICommandFactory.OpenDiagram(Guid id)
        {
            IPage page = container.Get(DiagramDockControl.Code(id));
            if (page != null)
            {
                container.Selected = page;
                return page;
            }
            else
            {
                DiagramDockControl viewer = new(container, id);
                container.Add(viewer);
                return viewer;
            }
        }

        IPage ICommandFactory.OpenDiagram(IIdentifier<Guid> identifier)
        {
            ICommandFactory factory = this;
            return factory.OpenDiagram(identifier.id);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2208:Правильно создавайте экземпляры исключений аргументов", Justification = "<Ожидание>")]
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
                page = container.Get(EditorDockControl.Code(id));
            }

            if (page != null)
            {
                container.Selected = page;
                return page;
            }
            else
            {
                using var conn = Db.OpenConnection();
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

                BrowserParameters editorParams = new()
                {
                    ParentId = parent == "document" ? null : row.parent_id,
                    OwnerId = row.owner_id,
                    OrganizationId = parent == "directory" ? null : row.organization_id
                };

                try
                {
                    EditorDockControl e = new(container, null, this, id, command, editorParams);
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2208:Правильно создавайте экземпляры исключений аргументов", Justification = "<Ожидание>")]
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
                page = container.Get(EditorDockControl.Code(id));
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
                    EditorDockControl e = new(container, browser, this, id, command, editorParams);
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

        IPage ICommandFactory.OpenCodeEditor(Command command, IEnumerable<Diagnostic> failures)
        {
            IPage page = container.Get(CodeDockControl.Code(command.id));
            if (page != null)
            {
                container.Selected = page;
            }
            else
            {
                page = new CodeDockControl(container, command);
                container.Add(page);
            }

            if (failures != null && page is CodeDockControl codeEditor)
            {
                codeEditor.ShowErrors(failures);
            }

            return page;
        }

        IPage ICommandFactory.OpenCodeEditor(Guid id, IEnumerable<Diagnostic> failures)
        {
            ICommandFactory factory = this;
            return factory.OpenCodeEditor(Commands.Get(id), failures);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2208:Правильно создавайте экземпляры исключений аргументов", Justification = "<Ожидание>")]
        IPage ICommandFactory.OpenCodeEditor(Hashtable parameters, IEnumerable<Diagnostic> failures)
        {
            ICommandFactory factory = this;
            if (!parameters.ContainsKey("id"))
            {
                throw new ArgumentException("Ожидался параметр id", "id");
            }

            if (parameters["id"] is Guid pid)
            {
                return factory.OpenCodeEditor(pid, failures);
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
            IPage page = container.Get(ViewerDockContol.Code(command.id));
            if (page != null)
            {
                container.Selected = page;
            }
            else
            {
                try
                {
                    ViewerDockContol viewer = new(container, this, command);
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
                                OpenCodeEditor(command, xe.Failures);
                            }

                            return true;
                        }

                        if (x is EmptyCodeException || x is MissingImpException)
                        {
                            if (MessageBox.Show($"{x.Message}\nОткрыть окно для создания кода?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2208:Правильно создавайте экземпляры исключений аргументов", Justification = "<Ожидание>")]
        private IPage OpenDiagram(params object[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentException($"Команда OpenDiagram должжна иметь 1 параметр, а передано {parameters.Length}");
            }

            ICommandFactory factory = this;
            return parameters[0] switch
            {
                Hashtable hashtable => factory.OpenDiagram(hashtable),
                Guid id => factory.OpenDiagram(id),
                IIdentifier<Guid> identifier => factory.OpenDiagram(identifier),
                _ => throw new ArgumentException("Аргумент id должен быть типа Guid", "id"),
            };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2208:Правильно создавайте экземпляры исключений аргументов", Justification = "<Ожидание>")]
        private IPage OpenDocument(params object[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentException($"Команда OpenDocument должжна иметь 1 параметр, а передано {parameters.Length}");
            }

            ICommandFactory factory = this;
            return parameters[0] switch
            {
                Hashtable hashtable => factory.OpenDocument(hashtable),
                Guid id => factory.OpenDocument(id),
                _ => throw new ArgumentException("Аргумент id должен быть типа Guid", "id"),
            };
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2208:Правильно создавайте экземпляры исключений аргументов", Justification = "<Ожидание>")]
        private IPage OpenCodeEditor(params object[] parameters)
        {
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Вызов команды open-browser-code без параметров.");
            }

            IEnumerable<Diagnostic> failures = null;
            if (parameters.Length > 1)
            {
                failures = parameters[1] as IEnumerable<Diagnostic>;
            }

            ICommandFactory factory = this;
            return parameters[0] switch
            {
                Command cmd => factory.OpenCodeEditor(cmd, failures),
                Guid id => factory.OpenCodeEditor(id, failures),
                Hashtable hashtable => factory.OpenCodeEditor(hashtable, failures),
                _ => throw new ArgumentException("Аргумент id должен быть типа Guid", "id"),
            };
        }
    }
}
