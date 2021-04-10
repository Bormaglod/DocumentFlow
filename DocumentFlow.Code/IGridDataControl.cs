//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.11.2020
// Time: 19:21
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Code
{
    public interface IGridDataControl
    {
        string HeaderText { get; set; }
        IEditorCode Editor { get; set; }
        Action<object> CheckValues { get; set; }
        void RefreshData();
    }
}
