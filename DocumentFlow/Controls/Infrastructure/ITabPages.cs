//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.08.2021
//-----------------------------------------------------------------------

using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls.Infrastructure;

public interface ITabPages
{
    TabPageAdv Selected { get; set; }
    void Add(TabPageAdv page);
    void Remove(TabPageAdv page);
}
