//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Settings.Infrastructure;

namespace DocumentFlow.Settings.Core;

public class MarginsSettings : IMarginsSettings
{
    public MarginsSettings()
    {
        /*Left = settings.Page.Settings.LeftMargin;
        Top = settings.Page.Settings.TopMargin;
        Bottom = settings.Page.Settings.BottomMargin;
        Right = settings.Page.Settings.RightMargin;*/
    }

    public int Left { get; set; }
    public int Top { get; set; }
    public int Right { get; set; }
    public int Bottom { get; set; }
    public bool MirrorOnEven { get; set; }
}
