//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.04.2022
//
// Версия 2022.8.28
//  - вызов NotifyPropertyChanged упрощен (параметр передается по
//    умолчанию - имя свойства подставляется автоматически)
// Версия 2022.9.9
//  - добавлено поле double_rate
// Версия 2022.12.2
//  - тип поля double_rate заменен на bool?
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Productions.Performed;

[Description("Выполнение работы")]
public class OperationsPerformed : AccountingDocument
{
    private long quantity;

    public Guid EmployeeId { get; set; }
    public Guid OperationId { get; set; }
    public Guid? ReplacingMaterialId { get; set; }

    public long Quantity
    {
        get => quantity;
        set
        {
            quantity = value;
            NotifyPropertyChanged();
        }
    }

    public decimal Salary { get; set; }
    public bool? DoubleRate { get; set; }
    public string? LotName { get; protected set; }
    public string? OrderName { get; protected set; }
    public string? GoodsName { get; protected set; }
    public Guid CalculationId { get; protected set; }
    public string CalculationName { get; protected set; } = string.Empty;
    public string OperationCode { get; protected set; } = string.Empty;
    public string? OperationName { get; protected set; }
    public string? EmployeeName { get; protected set; }
    public string? MaterialName { get; protected set; }
}