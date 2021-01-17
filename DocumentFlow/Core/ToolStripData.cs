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
using DocumentFlow.Code.Core;
using DocumentFlow.Data;
using DocumentFlow.Data.Entities;
using DocumentFlow.Interfaces;

namespace DocumentFlow
{
    public abstract class ToolStripData : ITool
    {
        private readonly Dictionary<IUserAction, ToolStripItem> items = new Dictionary<IUserAction, ToolStripItem>();
        private readonly UserActionCollection commands;

        public ToolStripData(ToolStrip toolStrip, UserActionCollection commandCollection)
        {
            ToolStrip = toolStrip;
            commands = commandCollection;

            var list = toolStrip.Items
                .OfType<ToolStripItem>()
                .Where(x =>
                {
                    var (CommandName, GroupName) = GetCommandComponents(x.Tag?.ToString());
                    return (!string.IsNullOrEmpty(CommandName) && GroupName == "user-defined");
                })
                .ToList();

            foreach (var item in list)
            {
                toolStrip.Items.Remove(item);
            }

            foreach (ToolStripItem item in toolStrip.Items)
            {
                if (item is ToolStripSeparator || item.Tag == null)
                    continue;

                var (CommandName, _) = GetCommandComponents(item.Tag.ToString());
                if (!(commands.Get(CommandName) is UserAction ci))
                {
                    ci = commands.Add(CommandMethod.Embedded, CommandName);
                }

                if (ci != null)
                {
                    ci.PropertyChanged += Notify_PropertyChanged;
                    items.Add(ci, item);
                }
            }

            UpdateButtonVisibleStatus();
        }

        public IEnumerable<IUserAction> Commands => items.Keys;

        bool ITool.Contains(IUserAction command) => items.ContainsKey(command);

        void ITool.AddCommand(IUserAction command)
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

        protected ToolStripItem this[IUserAction command] => items[command];

        public void UpdateButtonVisibleStatus()
        {
            var grp_items = new Dictionary<string, List<ToolStripItem>>();
            foreach (ToolStripItem item in ToolStrip.Items)
            {
                if (item.Tag == null)
                {
                    continue;
                }

                var (CommandName, GroupName) = GetCommandComponents(item.Tag.ToString());
                if (grp_items.ContainsKey(GroupName))
                {
                    grp_items[GroupName].Add(item);
                }
                else
                {
                    var list = new List<ToolStripItem> { item };
                    grp_items.Add(GroupName, list);
                }
            }

            foreach (string group_name in grp_items.Keys)
            {
                int visibles = 0;
                foreach (ToolStripItem item in grp_items[group_name])
                {
                    if (item is ToolStripSeparator)
                        continue;

                    IUserAction c = items.FirstOrDefault(x => x.Value == item).Key;
                    if (c == null || c.Visible)
                    {
                        visibles++;
                    }
                }

                bool visible = visibles > 0;
                foreach (var separator in grp_items[group_name].OfType<ToolStripSeparator>())
                {
                    separator.Visible = visible;
                }
            }
        }

        protected abstract ToolStripItem CreateToolStripItem(IUserAction command, Picture picture);

        protected abstract void UpdateToolStripItem(IUserAction command, Picture picture);

        protected Picture GetCommandPicture(IUserAction command)
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

        private (string CommandName, string GroupName) GetCommandComponents(string fullCommand)
        {
            if (string.IsNullOrEmpty(fullCommand))
                return (string.Empty, string.Empty);

            string[] names = fullCommand.Split(new char[] { '|' }, 2);
            switch (names.Length)
            {
                case 0:
                    return (string.Empty, string.Empty);
                case 1:
                    return (names[0], string.Empty);
                default:
                    return (names[0], names[1]);
            }
        }

        private ToolStripItem AddToolStripItem(IUserAction command)
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
            if (sender is IUserAction command && items.ContainsKey(command))
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
                if (command is IExecutor executor)
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
                if (command is IExecutor executor)
                {
                    commands.Execute(command.Code, executor.GetParameters());
                }
            }
        }
    }
}
