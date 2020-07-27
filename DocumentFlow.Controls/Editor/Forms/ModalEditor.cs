//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.06.2018
// Time: 17:10
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Forms
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Flee.PublicTypes;
    using NHibernate;
    using Syncfusion.Data.Extensions;
    using Syncfusion.Windows.Forms;
    using DocumentFlow.Controls.Editor.Core;
    using DocumentFlow.Core;
    using DocumentFlow.Data.Core;
    using DocumentFlow.DataSchema;

    public partial class ModalEditor : MetroForm
    {
        class FieldExpressionData
        {
            public IEditorExpression Editor { get; set; }
            public string Destination { get; set; }
            public IDynamicExpression Expression { get; set; }
            public string Query { get; set; }
        }

        private readonly ISession session;
        private readonly DatasetEditor editor;
        private ExpressionContext context;
        private readonly List<FieldExpressionData> expressions;
        private readonly List<string> locked;
        bool binding = false;

        public ModalEditor(ISession session, string formTitle, DatasetEditor editor)
        {
            InitializeComponent();

            this.session = session;
            this.editor = editor;

            locked = new List<string>();
            foreach (IEditorControl c in editor.Controls)
            {
                c.CreateControl(session, panel2, null);
            }

            int height = SystemInformation.CaptionHeight +
                         SystemInformation.FrameBorderSize.Height * 2 +
                         panel1.Height +
                         GetHeightControls(panel2.Controls);

            Height = Math.Min(SystemInformation.WorkingArea.Height, height) + 12;

            Text = formTitle;

            expressions = new List<FieldExpressionData>();
        }

        public bool Create(Guid owner, int status)
        {
            if (ShowModal(null, status))
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        Db.ExecuteUpdate(session, editor.Dataset.Insert, editor.GetTypes(), (x) => x == "owner_id" ? owner : (context.Variables.ContainsKey(x) ? context.Variables[x] : null));
                        try
                        {
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            ExceptionHelper.MesssageBox(ex);
                        }
                    }
                    catch (ParameterNotFoundException pe)
                    {
                        MessageBox.Show($"Невозможно вставить новую запись, т.к. при выполнении запроса '{editor.Dataset.Insert}' произошла ошибка - {pe.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return false;
        }

        public bool Edit(long id, int status)
        {
            IDictionary<string, Type> types = editor.GetTypes();
            try
            {
                IDictionary row = Db.ExecuteSelect(session, editor.Dataset.Select, types, ("id", id)).Single();

                if (ShowModal(row, status))
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        try
                        {
                            Db.ExecuteUpdate(session, editor.Dataset.Update, types, (x) => context.Variables.ContainsKey(x) ? context.Variables[x] : null);
                            try
                            {

                                transaction.Commit();
                                return true;
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                ExceptionHelper.MesssageBox(ex);
                            }
                        }
                        catch (ParameterNotFoundException pe)
                        {
                            MessageBox.Show($"Невозможно обновить запись, т.к. при выполнении запроса '{editor.Dataset.Update}' произошла ошибка - {pe.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (ParameterNotFoundException pe)
            {
                MessageBox.Show($"Невозможно начать редактировать запись, т.к. при выполнении запроса '{editor.Dataset.Select}' произошла ошибка - {pe.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        public bool Delete(long id, int status)
        {
            IDictionary<string, Type> types = editor.GetTypes();
            try
            {
                IDictionary row = Db.ExecuteSelect(session, editor.Dataset.Select, types, ("id", id)).Single();
                CreateContextVariables(row, status);

                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        Db.ExecuteUpdate(session, editor.Dataset.Delete, types, (x) => context.Variables.ContainsKey(x) ? context.Variables[x] : null);
                        try
                        {

                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            ExceptionHelper.MesssageBox(ex);
                        }
                    }
                    catch (ParameterNotFoundException pe)
                    {
                        MessageBox.Show($"Невозможно удалить запись, т.к. при выполнении запроса '{editor.Dataset.Delete}' произошла ошибка - {pe.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (ParameterNotFoundException pe)
            {
                MessageBox.Show($"Невозможно начать удаление записи, т.к. при выполнении запроса '{editor.Dataset.Select}' произошла ошибка - {pe.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private void CreateContextVariables(IDictionary row, int status)
        {
            EditorContext ec = new EditorContext(session, Guid.Empty, editor)
            {
                ActionPopupate = PopulateControl
            };

            context = new ExpressionContext(ec);
            ec.Variables = context.Variables;

            var controls = editor.GetControls();
            if (controls == null)
                return;

            foreach (IPopulated c in controls.OfType<IPopulated>())
            {
                c.Populate(session, row, editor.GetTypes(), status);

                IBindingEditorControl bindingEditor = c as IBindingEditorControl;
                if (c != null && bindingEditor.Value != null)
                    context.Variables.Add(bindingEditor.DataField, bindingEditor.Value);
            }

            if (!context.Variables.ContainsKey("id") && row != null && row.Keys.OfType<string>().Contains("id"))
                context.Variables.Add("id", row["id"]);

            foreach (IEditorExpression c in editor.GetControls().OfType<IEditorExpression>())
            {
                CreateExpressionList(c);
            }
        }

        private void PopulateControl(IEditorControl control, IDictionary data)
        {
            if (control is IPopulated populated)
            {
                populated.Populate(session, data, editor.GetTypes(), 0);
            }
        }

        private bool ShowModal(IDictionary row, int status)
        {
            UnbindEvents();
            expressions.Clear();

            CreateContextVariables(row, status);

            BindEvents();
            return ShowDialog() == DialogResult.OK;
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

        private void CreateExpressionList(IEditorExpression editor)
        {
            if (editor.Expressions.Count == 0)
                return;

            for (int i = 0; i < editor.Expressions.Count; i++)
            {
                if (string.IsNullOrEmpty(editor.Expressions[i].Expression) && string.IsNullOrEmpty(editor.Expressions[i].SQLExpression))
                    continue;

                FieldExpressionData data = new FieldExpressionData()
                {
                    Editor = editor,
                    Destination = editor.Expressions[i].Destination,
                    Expression = string.IsNullOrEmpty(editor.Expressions[i].Expression) ? null : context.CompileDynamic(editor.Expressions[i].Expression),
                    Query = string.IsNullOrEmpty(editor.Expressions[i].SQLExpression) ? null : editor.Expressions[i].SQLExpression
                };

                expressions.Add(data);
            }
        }

        private void UnbindEvents()
        {
            if (binding)
            {
                foreach (IBindingEditorControl c in editor.GetControls().OfType<IBindingEditorControl>())
                {
                    c.ValueChanged -= FieldValueChanged;
                }

                binding = false;
            }
        }

        private void BindEvents()
        {
            if (binding)
                return;

            foreach (IBindingEditorControl c in editor.GetControls().OfType<IBindingEditorControl>())
            {
                c.ValueChanged += FieldValueChanged;
            }

            binding = true;
        }

        private void FieldValueChanged(object sender, EventArgs e)
        {
            IBindingEditorControl editor_control = (IBindingEditorControl)sender;
            if (locked.Contains(editor_control.DataField))
                return;

            locked.Add(editor_control.DataField);
            try
            {
                context.Variables[editor_control.DataField] = editor_control.Value;
                foreach (FieldExpressionData data in expressions.Where(x => x.Editor == editor_control))
                {
                    IBindingEditorControl result = null;
                    if (!string.IsNullOrEmpty(data.Destination))
                    {
                        result = editor
                            .GetControls()
                            .OfType<IBindingEditorControl>()
                            .FirstOrDefault(x => string.Compare(x.DataField, data.Destination, true) == 0);
                        if (result == null)
                        {
                            throw new Exception($"В схеме указано поле {data.Destination}, но оно отсутсвует в реализации класса {data.Expression.Owner.GetType().Name}");
                        }
                    }

                    if (data.Expression != null)
                    {
                        if (result != null)
                            result.Value = data.Expression.Evaluate();
                        else
                            data.Expression.Evaluate();
                    }
                    else
                    {
                        try
                        {
                            IDictionary row = Db.ExecuteSelect(session, data.Query, editor.GetTypes(), (x) => context.Variables.ContainsKey(x) ? context.Variables[x] : null).SingleOrDefault();
                            if (result != null)
                            {
                                string key = row.Keys.ToList<string>().Single();
                                result.Value = row[key];
                            }
                        }
                        catch (ParameterNotFoundException pe)
                        {
                            MessageBox.Show($"Невозможно обновить поле {result.DataField}, т.к. возникла ошибка при выполнении запроса '{data.Query}' - {pe.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if (result != null)
                    {
                        context.Variables[result.DataField] = result.Value;
                    }
                }
            }
            finally
            {
                locked.Remove(editor_control.DataField);
            }
        }
    }
}
