//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Controls.Core;

public class DataDeleteEventArgs<T> : EventArgs
    where T : IIdentifier<long>
{
    public DataDeleteEventArgs(T data) => (Cancel, DeletingData) = (false, data);

    public bool Cancel { get; set; }
    public T DeletingData { get; }
}
