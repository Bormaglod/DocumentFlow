﻿//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.10.2020
// Time: 18:50
//-----------------------------------------------------------------------

using System.Windows.Forms;
using DocumentFlow.Code.Core;

namespace DocumentFlow.Code
{
    public interface IToolBar : ITool
    {
        ToolStripItemDisplayStyle ButtonStyle { get; set; }
        ButtonIconSize IconSize { get; set; }
    }
}
