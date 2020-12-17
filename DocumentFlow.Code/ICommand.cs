//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.10.2020
// Time: 19:18
//-----------------------------------------------------------------------

using System;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code
{
    public interface ICommand
    {
        event EventHandler<ExecuteEventArgs> Click;
        event EventHandler<GettingParametersEventArgs> GettingParameters;

        string Code { get; }
        string Name { get; }
        string Icon { get; }
        string Title { get; }
        bool Enabled { get; }
        bool Visible { get; }
        CommandMethod Method { get; }
        ICommand SetEnabled(bool enabledValue);
        ICommand SetVisible(bool visible);
        ICommand SetIcon(string name);
        ICommand ExecuteAction(EventHandler<ExecuteEventArgs> eventHandler);
        ICommand SetTitle(string title);
        ICommand AppendTo(IToolBar toolBar);
        ICommand AppendTo(IContextMenu contextMenu);
    }
}
