//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.09.2020
// Time: 22:38
//-----------------------------------------------------------------------

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Syncfusion.Windows.Forms.Edit.Enums;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Entities;
using Dapper;
using Syncfusion.Windows.Forms.Edit;

namespace DocumentFlow
{
    public partial class CodeEditor : ToolWindow, IPage
    {
        private readonly IContainerPage containerPage;
        private Command command;
        private BindingList<ErrorData> errorList = new BindingList<ErrorData>();

        private class ErrorData
        {
            private CompilerError error;

            public ErrorData(CompilerError error)
            {
                this.error = error;
            }

            [Display(Name = "Код")]
            public string Code => error.ErrorNumber;

            [Display(Name = "Строка")]
            public int Line => error.Line;

            [Display(Name = "Описание")]
            public string Description => error.ErrorText;
        }

        public CodeEditor(IContainerPage container, Command cmd)
        {
            InitializeComponent();

            containerPage = container;
            command = cmd;

            editControl.ApplyConfiguration(KnownLanguages.CSharp);
            editControl.Text = command.script;
            editControl.FlushChanges();
            editControl.CurrentPosition = new System.Drawing.Point(1, 1);

            tabSplitterContainer1.SplitterPosition = tabSplitterContainer1.Height - 200;
            tabSplitterContainer1.Collapsed = true;

            Text = $"Настройка: {cmd.name}";
        }

        Guid IPage.Id => command.Id;

        Guid IPage.InfoId => command.Id;

        IContainerPage IPage.Container => containerPage;

        void IPage.Rebuild() { }

        public void ShowErrors(CompilerErrorCollection errors)
        {
            errorList.Clear();
            foreach (CompilerError error in errors)
            {
                errorList.Add(new ErrorData(error));
            }

            gridErrors.DataSource = errorList;
            tabSplitterContainer1.Collapsed = false;
        }

        private void Compile()
        {
            IList<IPage> list = containerPage.Get(command.Id)
                .Where(x => x.Id != x.InfoId)
                .AsList();

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
                ShowErrors(e.Errors);
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
            using (var conn = Db.OpenConnection())
            {
                using (var transaction = conn.BeginTransaction())
                {
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
            }

            IList<IPage> list = containerPage.Get(command.Id).Where(x => x.Id != x.InfoId).AsList();
            if (list.Count > 0)
            {
                Compile();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            Save();
            Close();
        }

        private void editControl_Closing(object sender, StreamCloseEventArgs e)
        {
            e.Action = SaveChangesAction.Discard;
        }

        private void buttonCompile_Click(object sender, EventArgs e)
        {
            Save();
            Compile();
        }
    }
}
