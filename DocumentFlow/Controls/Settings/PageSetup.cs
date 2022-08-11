//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

using System.Drawing.Printing;

namespace DocumentFlow.Controls.Settings;

public class PageSetup
{
    public int PaperWidth { get; set; } = 210;
    public int PaperHeight { get; set; } = 297;
    public int LeftMargin { get; set; } = 10;
    public int TopMargin { get; set; } = 10;
    public int RightMargin { get; set; } = 10;
    public int BottomMargin { get; set; } = 10;
    public int PrintableWidth => PaperWidth - LeftMargin - RightMargin;
    public bool Landscape { get; set; } = false;
    public bool MirrorMargins { get; set; } = false;

    public bool PaperSizesEqual(PaperSize paperSize)
    {
        var w = Landscape ? PaperHeight : PaperWidth;
        var h = Landscape ? PaperWidth : PaperHeight;
        return
            Math.Round(paperSize.Width * 25.4 / 100) == w &&
            Math.Round(paperSize.Height * 25.4 / 100) == h;
    }
}
