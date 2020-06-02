//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.06.2019
// Time: 22:42
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using NHibernate;

    abstract public class ContainerControl<T> : EditorControl<T>, IContainer where T : Control, new()
    {
        [JsonProperty("controls")]
        public IList<IEditorControl> Controls { get; set; }

        IList<IEditorControl> IContainer.GetControls(bool withChilds)
        {
            if (Controls == null)
                return new List<IEditorControl>();

            List<IEditorControl> list = new List<IEditorControl>(Controls);
            if (withChilds)
            {
                foreach (IContainer item in Controls.OfType<IContainer>())
                {
                    list.AddRange(item.GetControls(true));
                }
            }

            return list;
        }

        protected override void DefaultCreateControl(ISession session)
        {
            base.DefaultCreateControl(session);
            if (Controls != null)
            {
                foreach (IEditorControl item in Controls)
                {
                    item.CreateControl(session, Control, null);
                    item.Control.BringToFront();
                }
            }
        }
    }
}
