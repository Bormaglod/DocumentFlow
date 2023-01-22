//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.08.2021
//
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Controls.Infrastructure в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Infrastructure.Controls;

public interface ITabPages
{
    TabPageAdv Selected { get; set; }
    void Add(TabPageAdv page);
    void Remove(TabPageAdv page);
}
