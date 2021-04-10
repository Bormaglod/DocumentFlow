//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
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
using DocumentFlow.Code.Exceptions;

namespace DocumentFlow.Code.Implementation
{
    public class ContainerData : ControlData, IContainer
    {
        private Func<IInformation> getInfo;
        private readonly List<IControl> controls = new();
        private readonly List<string> locked = new();

        public ContainerData(Control container, Func<IInformation> getInformation = null) : base(container) => getInfo = getInformation;

        public ContainerData(Control container, string name, Func<IInformation> getInformation = null) : this(container, getInformation)
        {
            ControlName = name;
        }

        IControl IContainer.this[string controlName] 
        { 
            get
            {
                IContainer container = this;
                return container.ControlsAll.Where(x => x.ControlName == controlName).Single();
            }
        }

        IEnumerable<IControl> IContainer.Controls => controls.OfType<IControl>();

        IEnumerable<IControl> IContainer.ControlsAll
        {
            get
            {
                IContainer container = this;
                var all = new List<IControl>(container.Controls);
                foreach (var c in controls.OfType<IContainer>())
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

            if (control is BindingControlData binding)
            {
                binding.Locked = locked;
            }

            if (control is ContainerData containerData)
            {
                containerData.getInfo = getInfo;
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

        void IContainer.Populate(IDbConnection conn, object entity, IControlEnabled enabled, IControlVisible visible)
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

                    if (enabled == null && visible == null)
                        continue;

                    string field = string.Empty;
                    if (control is IDataName dataName)
                    {
                        field = dataName.Name;
                    }

                    if (string.IsNullOrEmpty(field))
                        continue;

                    if (enabled != null && getInfo != null)
                        control.Enabled = enabled.Ability(entity, field, getInfo());

                    if (visible != null && getInfo != null)
                        control.Visible = visible.Ability(entity, field, getInfo());
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

        public void Clear()
        {
            controls.Clear();
            Owner.Controls.Clear();
        }
    }
}
