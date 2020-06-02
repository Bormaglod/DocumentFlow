//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.06.2019
// Time: 23:20
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema.Types
{
    using System.Windows.Forms;
    using NHibernate;
    using DocumentFlow.DataSchema.Types.Core;

    [Tag("Container")]
    public class F_Container : ContainerControl<Panel>
    {
        protected override void DefaultCreateControl(ISession session)
        {
            base.DefaultCreateControl(session);
            if (Padding == null)
                Control.Padding = new Padding(1);
        }
    }
}
