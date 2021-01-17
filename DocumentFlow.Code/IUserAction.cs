//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.10.2020
// Time: 19:18
//-----------------------------------------------------------------------

using System;
using DocumentFlow.Code.Core;

namespace DocumentFlow.Code
{
    public interface IUserAction
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
        IUserAction SetEnabled(bool enabledValue);
        IUserAction SetVisible(bool visible);
        IUserAction SetIcon(string name);
        IUserAction ExecuteAction(EventHandler<ExecuteEventArgs> eventHandler);
        IUserAction SetTitle(string title);
        IUserAction AppendTo(IToolBar toolBar);
        IUserAction AppendTo(IContextMenu contextMenu);
    }
}
