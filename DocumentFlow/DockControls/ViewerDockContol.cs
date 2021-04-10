//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.10.2020
// Time: 19:47
//-----------------------------------------------------------------------

using System;
using System.ComponentModel;
using WeifenLuo.WinFormsUI.Docking;
using DocumentFlow.Data.Entities;
using DocumentFlow.Interfaces;

namespace DocumentFlow
{
    public partial class ViewerDockContol : DockContent, IContentPage
    {
        public ViewerDockContol(IContainerPage container, ICommandFactory commandFactory, Command command, Guid? owner)
        {
            InitializeComponent();
            viewerControl1.ContainerForm = container;
            viewerControl1.CommandFactory = commandFactory;
            viewerControl1.ExecutedCommand = command;
            viewerControl1.OwnerId = owner;
            viewerControl1.InitializeViewer();
        }

        public ViewerDockContol(IContainerPage container, ICommandFactory commandFactory, Command cmd) : this(container, commandFactory, cmd, null) { }

        public static string Code(Guid id) => $"viewer [{id}]";

        #region IContentPage implementation

        Guid IContentPage.CommandId => viewerControl1.ExecutedCommand.id;

        #endregion

        #region IPage implementation

        string IPage.Code => Code(viewerControl1.ExecutedCommand.id);

        Guid IPage.Id => viewerControl1.CurrentId;

        string IPage.Header => viewerControl1.Text;

        void IPage.Rebuild() => viewerControl1.InitializeViewer();

        IContainerPage IPage.Container => viewerControl1.ContainerForm;

        #endregion

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            viewerControl1.OnClosing();
        }
    }
}
