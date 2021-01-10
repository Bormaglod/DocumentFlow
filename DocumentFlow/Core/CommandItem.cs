//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.12.2020
// Time: 20:07
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.ComponentModel;
using DocumentFlow.Code;
using DocumentFlow.Code.System;
using DocumentFlow.Data.Entities;

namespace DocumentFlow
{
    public class CommandItem : ICommand, INotifyPropertyChanged, ICommandExecutor
    {
        private string icon;
        private bool enabled;
        private bool visible;
        private string title;
        private readonly CommandMethod commandMethod;
        private readonly object ownerControl;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<ExecuteEventArgs> Click;
        public event EventHandler<GettingParametersEventArgs> GettingParameters;

        public CommandItem(object owner, Command command, CommandMethod method)
        {
            Command = command;
            icon = command.Picture?.code;
            enabled = true;
            visible = true;
            commandMethod = method;
            ownerControl = owner;
            title = command.name;
        }

        public Command Command { get; }

        public override string ToString() => Command.code;

        #region ICommand implementation

        string ICommand.Code => Command.code;

        string ICommand.Name => Command.name;
        string ICommand.Title => title;

        string ICommand.Icon => icon;

        bool ICommand.Enabled => enabled;

        bool ICommand.Visible => visible;

        CommandMethod ICommand.Method => commandMethod;

        ICommand ICommand.SetEnabled(bool enabledValue)
        {
            if (enabled != enabledValue)
            {
                enabled = enabledValue;
                NotifyPropertyChanged("Enabled");
            }

            return this;
        }

        ICommand ICommand.SetVisible(bool visibleValue)
        {
            if (visible != visibleValue)
            {
                visible = visibleValue;
                NotifyPropertyChanged("Visible");
            }

            return this;
        }

        ICommand ICommand.SetIcon(string name)
        {
            if (icon != name)
            {
                icon = name;
                NotifyPropertyChanged("Icon");
            }

            return this;
        }

        ICommand ICommand.SetTitle(string title)
        {
            if (this.title != title)
            {
                this.title = title;
                NotifyPropertyChanged("Title");
            }

            return this;
        }

        ICommand ICommand.ExecuteAction(EventHandler<ExecuteEventArgs> eventHandler)
        {
            Click = eventHandler;
            return this;
        }

        ICommand ICommand.AppendTo(IToolBar toolBar)
        {
            AppendTool(toolBar);
            return this;
        }

        ICommand ICommand.AppendTo(IContextMenu contextMenu)
        {
            AppendTool(contextMenu);
            return this;
        }

        #endregion

        #region ICommandExecutor implementation

        void ICommandExecutor.Execute()
        {
            switch (ownerControl)
            {
                case IBrowser browser:
                    Click?.Invoke(this, new ExecuteEventArgs(browser));
                    break;
                case IEditor editor:
                    Click?.Invoke(this, new ExecuteEventArgs(editor));
                    break;
            }
        }

        Hashtable ICommandExecutor.GetParameters()
        {
            if (GettingParameters != null)
            {
                GettingParametersEventArgs args = null;
                switch (ownerControl)
                {
                    case IBrowser browser:
                        args = new GettingParametersEventArgs(browser);
                        break;
                    case IEditor editor:
                        args = new GettingParametersEventArgs(editor);
                        break;
                }

                if (args != null)
                {
                    GettingParameters.Invoke(this, args);
                    return args.Parameters;
                }
            }

            return null;
        }

        #endregion

        private void AppendTool(ITool tool)
        {
            if (tool.Contains(this))
                return;

            tool.AddCommand(this);
        }

        private void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
