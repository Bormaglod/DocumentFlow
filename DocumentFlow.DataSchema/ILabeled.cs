//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.12.2019
// Time: 17:49
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.Drawing;

    public interface ILabeled
    {
        string Text { get; set; }
        int Width { get; set; }
        int EditWidth { get; set; }
        bool AutoSize { get; set; }
        ContentAlignment TextAlign { get; set; }
        bool Visible { get; set; }
    }
}
