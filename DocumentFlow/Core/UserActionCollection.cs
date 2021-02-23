//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.12.2020
// Time: 19:58
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DocumentFlow.Code;
using DocumentFlow.Code.Core;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data.Entities;
using DocumentFlow.Interfaces;

namespace DocumentFlow
{
    public class UserActionCollection : IUserActionCollection
    {
        private readonly List<IUserAction> commands = new List<IUserAction>();
        private readonly object owner;
        private readonly ICommandFactory commandFactory;

        IEnumerator IEnumerable.GetEnumerator() => commands.GetEnumerator();

        public IEnumerator<IUserAction> GetEnumerator() => commands.GetEnumerator();

        public UserActionCollection(object owner, ICommandFactory commandFactory)
        {
            this.owner = owner;
            this.commandFactory = commandFactory;
        }

        IUserAction IUserActionCollection.Add(CommandMethod method, string name, bool skipNotFound)
        {
            IUserActionCollection userActions = this;
            return userActions.Add(method, name, string.Empty, skipNotFound);
        }

        IUserAction IUserActionCollection.Add(CommandMethod method, string name, string alias, bool skipNotFound)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"{nameof(name)} не может быть пустым или иметь значение null", nameof(name));
            }

            Command cmd = commandFactory.Commands.FirstOrDefault(x => x.code == name);
            if (cmd == null)
            {
                if (skipNotFound)
                {
                    return null;
                }

                throw new CommandNotFoundException($"Команда {name} не существует.");
            }

            IUserActionCollection userActions = this;
            if (userActions.Get(name, string.IsNullOrEmpty(alias)) != null)
            {
                throw new ArgumentException($"{nameof(name)} уже есть в наборе команд.");
            }

            var command = string.IsNullOrEmpty(alias) ? 
                new UserAction(owner, cmd, method) : 
                new UserAction(owner, cmd, method, alias);
            commands.Add(command);
            return command;
        }

        void IUserActionCollection.OpenDocument(Guid id) => commandFactory.OpenDocument(id);

        void IUserActionCollection.OpenDiagram(Guid id) => commandFactory.OpenDiagram(id);

        IUserAction IUserActionCollection.Get(string name, bool withoutAlias) => commands.FirstOrDefault(x => withoutAlias ? x.Code == name : x.Alias == name);

        public Command GetCommand(IUserAction command) => commandFactory[command.Code];

        public void Execute(string command, params object[] parameters) => commandFactory.Execute(command, parameters);
    }
}
