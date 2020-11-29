//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.10.2020
// Time: 20:30
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DocumentFlow.Code.Core;

namespace DocumentFlow.Code.Implementation
{
    public class ContainerData : ControlData, IContainer
    {
        private List<IControl> controls;

        public ContainerData(Control container) : base(container)
        {
            controls = new List<IControl>();
        }

        IEnumerable<IControl> IContainer.Controls => controls.OfType<IControl>();

        IEnumerable<IControl> IContainer.ControlsAll
        {
            get
            {
                IContainer container = this;
                List<IControl> all = new List<IControl>(container.Controls);
                foreach (IContainer c in controls.OfType<IContainer>())
                {
                    all.AddRange(c.ControlsAll);
                }

                return all;
            }
        }

        void IContainer.Add(IControl control)
        {
            if (control is ControlData data)
            {
                Owner.Controls.Add(data.Owner);
                controls.Add(control);
                data.Owner.BringToFront();
            }
        }

        void IContainer.Add(IControl[] controls)
        {
            IContainer container = this;
            for (int i = 0; i < controls.Length; i++)
            {
                container.Add(controls[i]);
            }
        }

        void IContainer.Populate(IDbConnection conn, object entity, DataFieldParameter getEnabled, DataFieldParameter getVisible)
        {
            IContainer container = this;
            foreach (IControl control in container.ControlsAll)
            {
                try
                {
                    if (control is IPopulate populateControl)
                    {
                        populateControl.Populate(conn, entity);
                    }

                    if (getEnabled == null && getVisible == null)
                        continue;

                    string field = string.Empty;
                    if (control is IBindingControl binding)
                    {
                        field = binding.FieldName;
                    }
                    else if (control is IDataGrid dataGrid)
                    {
                        field = dataGrid.Name;
                    }

                    if (string.IsNullOrEmpty(field))
                        continue;

                    if (getEnabled != null)
                        control.Enabled = getEnabled(field);

                    if (getVisible != null)
                        control.Visible = getVisible(field);
                }
                catch (Exception e)
                {
                    string controlName = string.Empty;
                    if (control is IBindingControl bindingControl)
                    {
                        controlName = $" {bindingControl.LabelText} ({bindingControl.FieldName})";
                    }

                    throw new PopulateControlException(control, PopulateMethod.Populate, $"Ошибка при попытке заполнения элемента управления{controlName}.", e);
                }
                
            }

            foreach (IControl control in container.ControlsAll)
            {
                if (control is IPopulate populateControl)
                {
                    try
                    {
                        populateControl.DoAfterPopulation();
                    }
                    catch (Exception e)
                    {
                        string controlName = string.Empty;
                        if (control is IBindingControl bindingControl)
                        {
                            controlName = $" {bindingControl.LabelText} ({bindingControl.FieldName})";
                        }

                        throw new PopulateControlException(control, PopulateMethod.AfterPopulate, $"Ошибка при попытке заполнения элемента управления{controlName}.", e);
                    }
                }
            }
        }

        IControl IContainer.AsControl() => this;
    }
}
