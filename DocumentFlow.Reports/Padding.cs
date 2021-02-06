//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2021
// Time: 23:36
//-----------------------------------------------------------------------

namespace DocumentFlow.Reports
{
    public class Padding
    {
        public float Left { get; set; } = 2;
        public float Top { get; set; } = 0;
        public float Right { get; set; } = 2;
        public float Bottom { get; set; } = 0;
        public float Width => Left + Right;
        public float Height => Top + Bottom;
    }
}
