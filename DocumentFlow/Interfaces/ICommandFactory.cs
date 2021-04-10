//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.03.2020
// Time: 22:51
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using DocumentFlow.Code;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Interfaces
{
    public interface ICommandFactory
    {
        Command this[string code] { get; }
        IEnumerable<Command> Commands { get; }
        void Execute(Command command, params object[] parameters);
        void Execute(string command, params object[] parameters);

        IPage OpenDiagram(Guid id);
        IPage OpenDiagram(IIdentifier<Guid> identifier);
        IPage OpenDiagram(Hashtable parameters);
        IPage OpenDocument(Guid id);
        IPage OpenDocument(Hashtable parameters);
        IPage OpenEditor(IBrowser browser, Guid id, Command command, IBrowserParameters editorParams);
        IPage OpenEditor(IBrowser browser, IIdentifier<Guid> identifier, Command command, IBrowserParameters editorParams);
        IPage OpenCodeEditor(Command command, IEnumerable<Diagnostic> failures = null);
        IPage OpenCodeEditor(Guid id, IEnumerable<Diagnostic> failures = null);
        IPage OpenCodeEditor(Hashtable parameters, IEnumerable<Diagnostic> failures = null);
        void About();
        void Logout();
    }
}
