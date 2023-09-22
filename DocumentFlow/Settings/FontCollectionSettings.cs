//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.07.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Settings;

public class FontCollectionSettings
{
    public FontSettings Title { get; set; } = new FontSettings("Times New Roman", 12, FontStyle.Bold);
    public FontSettings Header { get; set; } = new FontSettings("Times New Roman", 10, FontStyle.Bold);
    public FontSettings Footer { get; set; } = new FontSettings("Times New Roman", 10, FontStyle.Regular);
    public FontSettings Base { get; set; } = new();
}
