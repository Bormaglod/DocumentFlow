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
    public class Borders
    {
        public bool Left { get; set; } = false;
        public bool Right { get; set; } = false;
        public bool Top { get; set; } = false;
        public bool Bottom { get; set; } = false;
        public bool All { get; set; } = false;
        public string ColorName { get; set; } = "Black";
        public float Width { get; set; } = 0.5f;
        public Color Color => Color.FromName(ColorName);
    }
}
