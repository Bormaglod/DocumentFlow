//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.03.2020
// Time: 18:23
//-----------------------------------------------------------------------

namespace DocumentFlow
{
    using System;
    using System.Windows.Forms;
    using NHibernate.Transform;
    using DocumentFlow.Core;
    using DocumentFlow.Data.Core;
    using DocumentFlow.Data.Entities;
    using DocumentFlow.DataSchema;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using NHibernate;
    using DocumentFlow.DataSchema.Types.Converters;
    using DocumentFlow.DataSchema.Types.Core;

    public struct EditorParams
    {
        public Guid Id;
        public Guid? Parent;
        public Guid? Owner;
        public EntityKind Kind;
        public DatasetEditor Editor;
    }

    public class CommandFactory : ICommandFactory
    {
        private class DocumentParameters
        {
            public Guid? owner_id { get; set; }
            public Guid entity_kind_id { get; set; }
            public string data_schema { get; set; }
        }

        private readonly IContainerPage container;

        public CommandFactory(IContainerPage container)
        {
            this.container = container;
        }

        void ICommandFactory.Execute(Command command, params object[] parameters)
        {
            switch (command.Code)
            {
                case "profile":
                    break;
                case "add-record":
                    CreateControlEditor((EditorParams)parameters[0]);
                    break;
                case "edit-record":
                    CreateControlEditor((EditorParams)parameters[0]);
                    break;
                case "view-picture":
                case "documents-schema":
                    CreateControlViewer(command);
                    break;
                case "open-diagram":
                    CreateDiagramViewer((Guid)parameters[0]);
                    break;
                case "open-document":
                    CreateControlEditor((List<(string, object)>)parameters[1]);
                    break;
                default:
                    string sql = @"with recursive r as 
                        (
	                        select id, parent_id from command where code in ('view-directory', 'view-document', 'report-commands')
	                        union all
	                        select c.id, c.parent_id from command c join r on (r.id = c.parent_id)
                        ) select id from r where id = :id";

                    bool createViewer = false;
                    using (var session = Db.OpenSession())
                    {
                        createViewer = session.CreateSQLQuery(sql)
                            .SetResultTransformer(Transformers.AliasToEntityMap)
                            .SetGuid("id", command.Id)
                            .UniqueResult() != null;
                    }

                    if (createViewer)
                    {
                        CreateControlViewer(command);
                    }

                    break;
            }
        }

        void ICommandFactory.Execute(string command, params object[] parameters)
        {
            using (var session = Db.OpenSession())
            {
                Command cmd = session.QueryOver<Command>().Where(x => x.Code == command).SingleOrDefault();
                if (cmd != null)
                {
                    ((ICommandFactory)this).Execute(cmd, parameters);
                }
            }
        }

        private void CreateDiagramViewer(Guid transitionId)
        {
            IPage page = container.Get(transitionId);
            if (page != null)
            {
                container.Selected = page;
            }
            else
            {
                DiagramViewer viewer = new DiagramViewer(transitionId);
                container.Add(viewer);
            }
        }

        private void CreateControlViewer(Command command)
        {
            IPage page = container.Get(command.Id);
            if (page != null)
            {
                container.Selected = page;
            }
            else
            {
                ContentViewer viewer = new ContentViewer(this, command, null);
                container.Add(viewer);
            }
        }

        private void CreateControlEditor(List<(string, object)> parameters)
        {
            if (parameters.Count == 0)
            {
                LogHelper.Logger.Error("Команда open-document: редактор не открыт. Не указан идентификатор документа.");
                return;
            }

            if (parameters[0].Item2 is Guid id)
            {
                using (ISession session = Db.OpenSession())
                {
                    DocumentParameters doc = session.CreateSQLQuery("select d.owner_id, d.entity_kind_id, c.data_schema from document d join command c on (c.entity_kind_id = d.entity_kind_id) where d.id = :id")
                        .SetGuid("id", id)
                        .SetResultTransformer(Transformers.AliasToBean<DocumentParameters>())
                        .UniqueResult<DocumentParameters>();

                    try
                    {
                        DatasetSchema schema = JsonConvert.DeserializeObject<DatasetSchema>(doc.data_schema, new ControlConverter());
                        EditorParams p = new EditorParams()
                        {
                            Id = id,
                            Parent = null,
                            Owner = doc.owner_id,
                            Kind = session.Get<EntityKind>(doc.entity_kind_id),
                            Editor = schema?.Editor
                        };

                        CreateControlEditor(p);
                    }
                    catch (UnknownTypeException e)
                    {
                        MessageBox.Show(e.Message, "Ошибка JSON-данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
                LogHelper.Logger.Error("Команда open-document: редактор не открыт. Неверный идентификатор документа.");
        }

        private void CreateControlEditor(EditorParams parameters)
        {
            if (parameters.Kind == null)
            {
                LogHelper.Logger.Error("Редактор не активирован. Значение параметра kind равно null.");
                return;
            }

            if (parameters.Editor == null)
            {
                LogHelper.Logger.Error("Редактор не активирован. Значение параметра editor равно null.");
                return;
            }

            IPage page = null;
            if (parameters.Id != Guid.Empty)
                page = container.Get(parameters.Id);

            if (page != null)
            {
                container.Selected = page;
            }
            else
            {
                try
                {
                    ContentEditor e = new ContentEditor(this, parameters);
                    container.Add(e);
                }
                catch (Exception e)
                {
                    ExceptionHelper.MesssageBox(e);
                }
            }
        }
    }
}
