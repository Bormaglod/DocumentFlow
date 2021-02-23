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
using DocumentFlow.Code.Core;
using DocumentFlow.Data.Entities;
using DocumentFlow.Interfaces;

namespace DocumentFlow
{
    public class UserAction : IUserAction, INotifyPropertyChanged, IExecutor
    {
        private string icon;
        private bool enabled;
        private bool visible;
        private string title;
        private readonly CommandMethod commandMethod;
        private readonly object ownerControl;
        private string alias;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<ExecuteEventArgs> Click;
        public event EventHandler<GettingParametersEventArgs> GettingParameters;

        public UserAction(object owner, Command command, CommandMethod method)
        {
            Command = command;
            icon = command.Picture?.code;
            enabled = true;
            visible = true;
            commandMethod = method;
            ownerControl = owner;
            title = command.name;
            alias = command.code;
        }

        public UserAction(object owner, Command command, CommandMethod method, string alias) : this(owner, command, method) => this.alias = alias;

        public Command Command { get; }

        public override string ToString() => Command.code;

        #region IUserAction implementation

        string IUserAction.Code => Command.code;

        string IUserAction.Alias => alias;

        string IUserAction.Name => Command.name;

        string IUserAction.Title => title;

        string IUserAction.Icon => icon;

        bool IUserAction.Enabled => enabled;

        bool IUserAction.Visible => visible;

        CommandMethod IUserAction.Method => commandMethod;

        IUserAction IUserAction.SetEnabled(bool enabledValue)
        {
            if (enabled != enabledValue)
            {
                enabled = enabledValue;
                NotifyPropertyChanged("Enabled");
            }

            return this;
        }

        IUserAction IUserAction.SetVisible(bool visibleValue)
        {
            if (visible != visibleValue)
            {
                visible = visibleValue;
                NotifyPropertyChanged("Visible");
            }

            return this;
        }

        IUserAction IUserAction.SetIcon(string name)
        {
            if (icon != name)
            {
                icon = name;
                NotifyPropertyChanged("Icon");
            }

            return this;
        }

        IUserAction IUserAction.SetTitle(string title)
        {
            if (this.title != title)
            {
                this.title = title;
                NotifyPropertyChanged("Title");
            }

            return this;
        }

        IUserAction IUserAction.ExecuteAction(EventHandler<ExecuteEventArgs> eventHandler)
        {
            Click = eventHandler;
            return this;
        }

        IUserAction IUserAction.AppendTo(IToolBar toolBar)
        {
            AppendTool(toolBar);
            return this;
        }

        IUserAction IUserAction.AppendTo(IContextMenu contextMenu)
        {
            AppendTool(contextMenu);
            return this;
        }

        #endregion

        #region IExecutor implementation

        void IExecutor.Execute()
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

        Hashtable IExecutor.GetParameters()
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
