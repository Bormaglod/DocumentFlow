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
    public class ReportFont
    {
        public string Name { get; set; } = "Microsoft Sans Serif";
        public float Size { get; set; } = 10;
        public FontStyle Style { get; set; } = FontStyle.Regular;
        public Font Instance => new Font(Name, Size, Style);
    }
}
