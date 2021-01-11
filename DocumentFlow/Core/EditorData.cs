﻿//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.11.2020
// Time: 22:35
//-----------------------------------------------------------------------

using System.Windows.Forms;
using DocumentFlow.Code;
using DocumentFlow.Controls.Editor.Code;

namespace DocumentFlow
{
    public class EditorData : ModalEditorData
    {
        private readonly IBrowser ownerBrowser;
        private readonly CommandCollection commandCollection;
        private readonly ToolBarData toolBar;

        public EditorData(IContainer container, IBrowser browser, ICommandFactory commandFactory, IBrowserParameters browserParameters, ToolStrip toolStrip) : base(container, browserParameters)
        {
            commandCollection = new CommandCollection(this, commandFactory);
            toolBar = new ToolBarData(toolStrip, commandCollection);
            ownerBrowser = browser;
        }

        protected override IToolBar GetToolBar() => toolBar;
        protected override ICommandCollection GetCommandCollection() => commandCollection;
        protected override IBrowser GetBrowser() => ownerBrowser;
    }
}