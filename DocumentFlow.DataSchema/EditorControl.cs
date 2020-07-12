//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.03.2019
// Time: 20:00
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Flee.PublicTypes;
    using Newtonsoft.Json;
    using NHibernate;

    abstract public class EditorControl<T>: IEditorControl, ICloneable, IControlExpression where T: Control, new()
    {
        Control IEditorControl.Control => Control;
        string IEditorControl.Name => GetName();

        string IControlExpression.CanCreate => CanCreateExpression;

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public string ControlType { get; set; }

        [DefaultValue(0)]
        [JsonProperty("left", DefaultValueHandling = DefaultValueHandling.Populate)]
        public int Left { get; set; }

        [DefaultValue(0)]
        [JsonProperty("top", DefaultValueHandling = DefaultValueHandling.Populate)]
        public int Top { get; set; }

        [DefaultValue(100)]
        [JsonProperty("width", DefaultValueHandling = DefaultValueHandling.Populate)]
        public int Width { get; set; }

        [DefaultValue(32)]
        [JsonProperty("height", DefaultValueHandling = DefaultValueHandling.Populate)]
        public int Height { get; set; }

        [DefaultValue(true)]
        [JsonProperty("enabled", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool Enabled { get; set; }

        [DefaultValue(true)]
        [JsonProperty("visible", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool Visible { get; set; }

        [DefaultValue(DockStyle.Top)]
        [JsonProperty("dock", DefaultValueHandling = DefaultValueHandling.Populate)]
        public DockStyle Dock { get; set; }

        [JsonProperty("margin")]
        public IList<int> Margin { get; set; }

        [JsonProperty("padding")]
        public IList<int> Padding { get; set; }

        /// <summary>
        /// Выражение возвращающее true, если элемент управления будет создан. Или false, если не будет создан.
        /// </summary>
        [JsonProperty("can-create")]
        public string CanCreateExpression { get; set; }

        [JsonIgnore]
        protected T Control { get; private set; }

        bool IEditorControl.Enabled
        {
            get
            {
                if (Control is IEnabled e)
                    return e.Enabled;

                return Control.Enabled;
            }

            set
            {
                if (Control is IEnabled e)
                    e.Enabled = value;
                else
                    Control.Enabled = value;
            }
        }

        bool IEditorControl.Visible
        {
            get => Control.Visible;
            set => Control.Visible = value;
        }

        void IEditorControl.CreateControl(ISession session, Control container, ExpressionContext context)
        {
            if (context != null && !string.IsNullOrEmpty(CanCreateExpression))
            {
                IGenericExpression<bool> expression = context.CompileGeneric<bool>(CanCreateExpression);
                if (!expression.Evaluate())
                    return;
            }

            DefaultCreateControl(session);
            if (container != null)
            {
                container.Controls.Add(Control);
                Control.BringToFront();
            }
        }

        object ICloneable.Clone()
        {
            return MemberwiseClone();
        }

        protected virtual void DefaultCreateControl(ISession session)
        {
            Control = new T
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(Left, Top),
                Enabled = Enabled,
                Visible = Visible,
                Width = Width,
                Height = Height,
                Dock = Dock
            };

            if (Margin != null)
            {
                Control.Margin = new Padding(Margin[0], Margin[1], Margin[2], Margin[3]);
            }

            if (Padding != null)
            {
                Control.Padding = new Padding(Padding[0], Padding[1], Padding[2], Padding[3]);
            }
        }

        protected virtual string GetName() => Name;

        public override string ToString()
        {
            return Name;
        }
    }
}
