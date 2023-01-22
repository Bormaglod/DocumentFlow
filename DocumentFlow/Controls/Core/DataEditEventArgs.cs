//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.10.2021
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

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
