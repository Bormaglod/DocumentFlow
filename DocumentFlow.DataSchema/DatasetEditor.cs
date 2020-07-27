//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.04.2020
// Time: 13:04
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class DatasetEditor
    {
        [JsonProperty("dataset", Required = Required.Always)]
        public DatasetCommand Dataset { get; set; }

        [JsonProperty("controls")]
        public IList<IEditorControl> Controls { get; set; }

        [JsonProperty("conditions")]
        public IList<ControlCondition> Conditions { get; set; }
        
        [JsonProperty("childs")]
        public IList<ChildViewerData> Childs { get; set; }

        public IList<IEditorControl> GetControls(bool withChilds = true) => withChilds ? GetControls() : Controls;

        public IDictionary<string, Type> GetTypes()
        {
            Dictionary<string, Type> types = new Dictionary<string, Type>();
            foreach (IBindingEditorControl control in GetControls().OfType<IBindingEditorControl>())
            {
                types.Add(control.DataField, control.ValueType);
            }

            return types;
        }

        private IList<IEditorControl> GetControls()
        {
            if (Controls == null)
            {
                return new List<IEditorControl>();
            }

            List<IEditorControl> list = new List<IEditorControl>(Controls);
            foreach (IContainer item in Controls.OfType<IContainer>())
            {
                list.AddRange(item.GetControls(true));
            }

            return list;
        }

        public DatasetEditor Clone()
        {
            DatasetEditor editor = new DatasetEditor
            {
                Dataset = Dataset,
                Controls = new List<IEditorControl>()
            };

            foreach (ICloneable control in Controls.OfType<ICloneable>())
            {
                editor.Controls.Add((IEditorControl)control.Clone());
            }

            editor.Conditions = Conditions;
            editor.Childs = Childs;

            return editor;
        }
    }
}
