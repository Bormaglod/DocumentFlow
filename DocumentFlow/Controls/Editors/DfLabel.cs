//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.04.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Controls.Editors;

public partial class DfLabel : BaseControl, ILabelControl
{
    public DfLabel(string header) : base(string.Empty)
    {
        InitializeComponent();
        SetLabelControl(label1, header);

        Dock = DockStyle.Top;
    }

    #region ILabelControl interface

    ILabelControl ILabelControl.SetText(string text)
    {
        Header = text;
        return this;
    }

    #endregion
}
