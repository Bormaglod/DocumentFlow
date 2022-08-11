//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.01.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Core;

[Obsolete]
public class DataSourceEventArgs<T> : EventArgs
{
    public DataSourceEventArgs() { }

    public IEnumerable<T>? Source { get; set; }
}
