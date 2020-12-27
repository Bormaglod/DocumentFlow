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
using DocumentFlow.Code.System;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data.Entities;

namespace DocumentFlow
{
    public class CommandCollection : ICommandCollection
    {
        private readonly List<ICommand> commands = new List<ICommand>();
        private readonly object owner;
        private readonly ICommandFactory commandFactory;

        IEnumerator IEnumerable.GetEnumerator() => commands.GetEnumerator();

        public IEnumerator<ICommand> GetEnumerator() => commands.GetEnumerator();

        public CommandCollection(object owner, ICommandFactory commandFactory)
        {
            this.owner = owner;
            this.commandFactory = commandFactory;
        }

        ICommand ICommandCollection.Get(string name) => commands.FirstOrDefault(x => x.Code == name);

        ICommand ICommandCollection.Add(CommandMethod method, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"{nameof(name)} не может быть пустым или иметь значение null", nameof(name));
            }

            Command cmd = commandFactory.Commands.FirstOrDefault(x => x.code == name);
            if (cmd == null)
            {
                throw new CommandNotFoundException($"Команда {name} не существует.");
            }

            var command = new CommandItem(owner, cmd, method);
            commands.Add(command);
            return command;
        }

        void ICommandCollection.OpenDocument(Guid id) => commandFactory.Execute("open-document", id);

        void ICommandCollection.OpenDiagram(Guid id) => commandFactory.Execute("open-diagram", id);

        public CommandItem Add(CommandMethod method, string name)
        {
            var cmd = commandFactory.Commands.FirstOrDefault(x => x.code == name);
            if (cmd != null)
            {
                var command = new CommandItem(owner, cmd, method);
                commands.Add(command);

                return command;
            }

            return null;
        }

        public Command GetCommand(ICommand command) => commandFactory[command.Code];

        public void Execute(string command, params object[] parameters) => commandFactory.Execute(command, parameters);
    }
}
