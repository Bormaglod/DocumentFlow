//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.10.2020
// Time: 19:47
//-----------------------------------------------------------------------

using System;
using System.ComponentModel;
using DocumentFlow.Data.Entities;

namespace DocumentFlow
{
    public partial class ContentViewer : ToolWindow, IPage
    {
        public ContentViewer(IContainerPage container, ICommandFactory commandFactory, Command command, Guid? owner)
        {
            InitializeComponent();
            viewerControl1.ContainerForm = container;
            viewerControl1.CommandFactory = commandFactory;
            viewerControl1.ExecutedCommand = command;
            viewerControl1.OwnerId = owner;
            viewerControl1.InitializeViewer();
        }

        public ContentViewer(IContainerPage container, ICommandFactory commandFactory, Command cmd) : this(container, commandFactory, cmd, null) { }

        Guid IPage.Id => viewerControl1.ExecutedCommand.Id;

        Guid IPage.InfoId => viewerControl1.InfoId;

        IContainerPage IPage.Container => viewerControl1.ContainerForm;

        void IPage.Rebuild() => viewerControl1.InitializeViewer();

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            viewerControl1.OnClosing();
        }
    }
}
