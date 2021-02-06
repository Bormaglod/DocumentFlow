//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2021
// Time: 23:36
//-----------------------------------------------------------------------

namespace DocumentFlow.Reports
{
    public class Margin
    {
        private float width;
        private float height;

        public Margin() { }
        public Margin(PageSize size) => SetSize(size);

        public float Left { get; set; } = 10;
        public float Top { get; set; } = 10;
        public float Right { get; set; } = 10;
        public float Bottom { get; set; } = 10;
        public float Width => width;
        public float Height => height;
        public void SetSize(PageSize size)
        {
            width = size.Width - Left - Right;
            height = size.Height - Top - Bottom;
        }
    }
}
