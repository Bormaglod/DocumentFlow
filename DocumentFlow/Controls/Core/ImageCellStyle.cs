//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.12.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Core;

public class ImageCellStyle
{
    public string DisplayText { get; set; } = string.Empty;
    public Image? Image { get; set; } = null;
    public TextImageRelation TextImageRelation { get; set; }
    public ImageLayout ImageLayout { get; set; }
}
