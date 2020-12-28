//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.06.2018
// Time: 17:10
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Windows.Forms;
using DocumentFlow.Code;
using DocumentFlow.Code.Implementation;
using DocumentFlow.Controls.Editor.Code;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Controls.Forms
{
    public partial class ModalEditor : Form
    {
        private readonly IContainer container;
        private readonly IBrowserParameters parameters;
        private IEditor editorData;
        private readonly Action<object> checkValuesData;

        public ModalEditor(Guid ownerId, string headerText, Action<object> checkValues)
        {
            InitializeComponent();

            container = new ContainerData(panelControls);
            parameters = new BrowserParameters() { OwnerId = ownerId };
            checkValuesData = checkValues;

            Text = headerText;
        }

        public bool Create(IEditorCode editor, Type entityType)
        {
            editorData = new ModalEditorData(container, parameters)
            {
                Entity = Activator.CreateInstance(entityType) as IIdentifier
            };

            if (editorData.Entity == null)
                throw new ArgumentNullException($"Объект типа {entityType} должен реализовывать интерфейс IIdentifier");

            if (editor is IDataOperation operation)
            {
                editor.Initialize(editorData, new Database());
                CalculateHeightForm();
                using (var conn = Db.OpenConnection())
                {
                    container.Populate(conn, editorData.Entity);

                    if (ShowDialog() == DialogResult.OK)
                    {
                        using (var transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                operation.Insert(conn, transaction, parameters, editorData);
                                transaction.Commit();
                                return true;
                            }
                            catch (Exception e)
                            {
                                transaction.Rollback();
                                ExceptionHelper.MesssageBox(e);
                            }

                        }
                    }
                }
            }

            return false;
        }

        public bool Edit(IEditorCode editor, IIdentifier id)
        {
            if (editor is IDataOperation operation)
            {
                using (var conn = Db.OpenConnection())
                {
                    editorData = new ModalEditorData(container, parameters)
                    {
                        Entity = operation.Select(conn, id, parameters) as IIdentifier
                    };
                    editor.Initialize(editorData, new Database());
                    CalculateHeightForm();

                    container.Populate(conn, editorData.Entity);

                    if (ShowDialog() == DialogResult.OK)
                    {
                        using (var transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                operation.Update(conn, transaction, editorData);
                                transaction.Commit();
                                return true;
                            }
                            catch (Exception e)
                            {
                                transaction.Rollback();
                                ExceptionHelper.MesssageBox(e);
                            }
                        }
                    }
                }
            }

            return false;
        }

        public bool Delete(IEditorCode editor, IIdentifier id)
        {
            if (editor is IDataOperation operation)
            {
                using (var conn = Db.OpenConnection())
                {
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            operation.Delete(conn, transaction, id);
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            ExceptionHelper.MesssageBox(e);
                        }
                    }
                }
            }

            return false;
        }

        private int GetHeightControls(IEnumerable controls)
        {
            int height = 0;
            foreach (Control control in controls)
            {
                if (control.Dock == DockStyle.Fill)
                {
                    height = Math.Max(height, GetHeightControls(control.Controls));
                }
                else
                {
                    height = Math.Max(height, control.Top + control.Height);
                }
            }

            return height;
        }

        private void CalculateHeightForm()
        {
            int height = SystemInformation.CaptionHeight +
                         SystemInformation.FrameBorderSize.Height * 2 +
                         panel1.Height +
                         GetHeightControls(panelControls.Controls);

            Height = Math.Min(SystemInformation.WorkingArea.Height, height) + 12;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                checkValuesData?.Invoke(editorData.Entity);
            }
            catch (Exception ex)
            {
                ExceptionHelper.MesssageBox(ex);
                DialogResult = DialogResult.None;
            }
        }
    }
}
