//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Controls.Core;

public enum RuleChange { Update, DeleteAndInsert }

public class DataEditEventArgs<T> : EventArgs
    where T : IIdentifier<long>
{
    public DataEditEventArgs(T data) => (Cancel, EditingData, Rule) = (false, data, RuleChange.Update);

    public bool Cancel { get; set; }
    public T EditingData { get; }
    public RuleChange Rule { get; set; }
}
