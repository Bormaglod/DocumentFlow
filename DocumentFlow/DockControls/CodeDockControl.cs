//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.09.2020
// Time: 22:38
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using Microsoft.CodeAnalysis;
using Dapper;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.Windows.Forms.Edit;
using Syncfusion.Windows.Forms.Edit.Enums;
using WeifenLuo.WinFormsUI.Docking;
using DocumentFlow.Core;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data;
using DocumentFlow.Data.Entities;
using DocumentFlow.Interfaces;

namespace DocumentFlow
{
    public partial class CodeDockControl : DockContent, IPage
    {
        private readonly IContainerPage containerPage;
        private readonly Command command;

        private class ErrorData
        {
            private readonly Diagnostic failure;

            public ErrorData(Diagnostic failure)
            {
                this.failure = failure;
            }

            [Display(Name = "Код")]
            public string Code => failure.Id;

            [Display(Name = "Строка")]
            public int Line => failure.Location.GetLineSpan().StartLinePosition.Line + 1;

            [Display(Name = "Позиция")]
            public int Position => failure.Location.GetLineSpan().StartLinePosition.Character + 1;

            [Display(Name = "Описание")]
            public string Description => failure.GetMessage();
        }

        public CodeDockControl(IContainerPage container, Command cmd)
        {
            InitializeComponent();

            containerPage = container;
            command = cmd;

            editControl.ApplyConfiguration(KnownLanguages.CSharp);
            editControl.Text = command.script;
            editControl.FlushChanges();
            editControl.CurrentPosition = new Point(1, 1);

            tabSplitterContainer1.SplitterPosition = tabSplitterContainer1.Height - 200;
            tabSplitterContainer1.Collapsed = true;

            editControl.HighlightCurrentLine = true;
            editControl.CurrentLineHighlightColor = Color.Orange;

            TabText = $"Настройка: {cmd.name}";
        }

        public static string Code(Guid id) => $"code editor [{id}]";

        #region IPage implementation

        string IPage.Code => Code(command.id);

        Guid IPage.Id => command.id;

        string IPage.Header => Text;

        IContainerPage IPage.Container => containerPage;

        void IPage.Rebuild() { }

        #endregion

        public void ShowErrors(IEnumerable<Diagnostic> failures)
        {
            gridErrors.DataSource = failures
                .Select(x => new ErrorData(x))
                .OrderBy(x => x.Line)
                .ThenBy(x => x.Position);
            tabSplitterContainer1.Collapsed = false;
        }

        private void Compile()
        {
            IList<IContentPage> list = containerPage.GetContentPages().Where(p => p.CommandId == command.id).AsList();

            try
            {
                command.Compile();
                gridErrors.DataSource = null;
                tabSplitterContainer1.Collapsed = true;

                foreach (IPage page in list)
                {
                    page.Rebuild();
                }

                return;
            }
            catch (CompilerException e)
            {
                ShowErrors(e.Failures);
            }
            catch (Exception e)
            {
                ExceptionHelper.MesssageBox(e);
            }

            foreach (IPage page in list)
            {
                page.Close();
            }
        }

        private void Save()
        {
            command.script = editControl.Text;
            using var conn = Db.OpenConnection();
            using var transaction = conn.BeginTransaction();
            try
            {
                conn.Execute("update command set script = :script where id = :id", command, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                ExceptionHelper.MesssageBox(ex);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e) => Save();

        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            Save();
            Close();
        }

        private void editControl_Closing(object sender, StreamCloseEventArgs e) => e.Action = SaveChangesAction.Discard;

        private void buttonCompile_Click(object sender, EventArgs e)
        {
            Save();
            Compile();
        }

        private void gridErrors_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            if (e.DataRow.RowData is ErrorData error)
            {
                editControl.CurrentPosition = new Point(error.Position, error.Line);
                editControl.Focus();
            }
        }
    }
}
