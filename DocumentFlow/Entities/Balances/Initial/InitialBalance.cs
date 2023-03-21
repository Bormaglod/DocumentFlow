﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.06.2022
//
// Версия 2022.12.29
//  - интерфейс теперь public
//  - добавлено перечисление BalanceCategory
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;

namespace DocumentFlow.Entities.Balances.Initial;

public enum BalanceCategory { Debet, Credit }

public class InitialBalance : AccountingDocument
{
    public Guid reference_id { get; set; }
    public decimal operation_summa { get; set; }
    public decimal amount { get; set; }
}
