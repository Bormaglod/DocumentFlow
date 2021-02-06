//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2021
// Time: 23:36
//-----------------------------------------------------------------------

using System.Drawing;

namespace DocumentFlow.Reports
{
    public class Fill
    {
        public string ColorName { get; set; } = "White";
        public Color Color => Color.FromName(ColorName);
    }
}
