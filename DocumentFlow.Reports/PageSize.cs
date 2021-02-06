//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2021
// Time: 23:36
//-----------------------------------------------------------------------

using System.Drawing;
using DocumentFlow.Core;

namespace DocumentFlow.Reports
{
    public class PageSize
    {
        public float Width { get; set; } = 210;
        public float Height { get; set; } = 297;
        public SizeF Size => new SizeF(Length.FromMillimeter(Width).ToPoint(), Length.FromMillimeter(Height).ToPoint());
    }
}
