//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.07.2023
//-----------------------------------------------------------------------

using System.Collections;

namespace DocumentFlow.Controls.Events;

public class DataSourceLoadEventArgs : EventArgs
{
    private readonly Guid current;

    public DataSourceLoadEventArgs(Guid current)
    {
        this.current = current;
    }

    public IEnumerable? Values { get; set; }

    public Guid CurrentDocument => current;
}
