//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.12.2020
// Time: 22:03
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code;
using DocumentFlow.Code.System;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Entities;

namespace DocumentFlow
{
    public abstract class ToolStripData : ITool
    {
        private readonly Dictionary<ICommand, ToolStripItem> items = new Dictionary<ICommand, ToolStripItem>();
        private readonly CommandCollection commands;

        public ToolStripData(ToolStrip toolStrip, CommandCollection commandCollection)
        {
            ToolStrip = toolStrip;
            commands = commandCollection;

            var list = toolStrip.Items.OfType<ToolStripItem>().Where(x => x.Tag?.ToString() == "user-defined").ToList();
            foreach (var item in list)
            {
                toolStrip.Items.Remove(item);
            }

            foreach (ToolStripItem item in toolStrip.Items)
            {
                if (item is ToolStripSeparator || item.Tag == null)
                    continue;

                CommandItem ci = commands.Add(CommandMethod.Embedded, item.Tag.ToString());
                if (ci != null)
                {
                    items.Add(ci, item);
                }
            }

            UpdateButtonVisibleStatus();
        }

        bool ITool.Contains(ICommand command) => items.ContainsKey(command);

        void ITool.AddCommand(ICommand command)
        {
            var item = AddToolStripItem(command);
            switch (command.Method)
            {
                case CommandMethod.Sql:
                    break;
                case CommandMethod.Embedded:
                    item.Click += EmbededCommand_Click;
                    break;
                case CommandMethod.UserDefined:
                    item.Click += UserDefinedCommand_Click;
                    break;
            }

            UpdateButtonVisibleStatus();
        }

        protected ToolStrip ToolStrip { get; }

        protected ToolStripItem this[ICommand command] => items[command];

        public void UpdateButtonVisibleStatus()
        {
            int visibleCount = 0;
            for (int i = 0; i < ToolStrip.Items.Count; i++)
            {
                if (ToolStrip.Items[i] is ToolStripSeparator)
                {
                    ToolStrip.Items[i].Visible = visibleCount > 0;
                    visibleCount = 0;
                }
                else
                {
                    if (ToolStrip.Items[i].Visible)
                    {
                        visibleCount++;
                    }
                }
            }

            if (ToolStrip.Items[ToolStrip.Items.Count - 1] is ToolStripSeparator separator)
            {
                separator.Visible = false;
            }
        }

        protected IEnumerable<ICommand> GetCommands() => items.Keys;

        protected abstract ToolStripItem CreateToolStripItem(ICommand command, Picture picture);

        protected abstract void UpdateToolStripItem(ICommand command, Picture picture);

        protected Picture GetCommandPicture(ICommand command)
        {
            var c = commands.GetCommand(command);
            if (c == null)
                return null;

            var p = c.Picture;
            if (command.Icon != c.Picture.code)
            {
                using (var conn = Db.OpenConnection())
                {
                    p = conn.QueryFirstOrDefault<Picture>("select * from picture where code = :code", new { code = command.Icon });
                }
            }

            return p;
        }

        private ToolStripItem AddToolStripItem(ICommand command)
        {
            
            var item = CreateToolStripItem(command, GetCommandPicture(command));
            ToolStrip.Items.Add(item);

            items.Add(command, item);
            if (command is INotifyPropertyChanged notify)
            {
                notify.PropertyChanged += Notify_PropertyChanged;
            }

            return item;
        }

        private void Notify_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ICommand command && items.ContainsKey(command))
            {
                switch (e.PropertyName)
                {
                    case "Enabled":
                        items[command].Enabled = command.Enabled;
                        break;
                    case "Visible":
                        items[command].Visible = command.Visible;
                        break;
                    case "Icon":
                        UpdateToolStripItem(command, GetCommandPicture(command));
                        break;
                    case "Title":
                        items[command].Text = command.Title;
                        break;
                }
            }
        }

        private void UserDefinedCommand_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem item)
            {
                var command = items.FirstOrDefault(x => x.Value == item).Key;
                if (command is ICommandExecutor executor)
                {
                    executor.Execute();
                }
            }
        }

        private void EmbededCommand_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem item)
            {
                var command = items.FirstOrDefault(x => x.Value == item).Key;
                if (command is ICommandExecutor executor)
                {
                    commands.Execute(command.Code, executor.GetParameters());
                }
            }
        }
    }
}
