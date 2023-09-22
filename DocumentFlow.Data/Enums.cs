//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.07.2023
//-----------------------------------------------------------------------

using System.ComponentModel;

namespace DocumentFlow.Data.Enums;

public enum AutoSizeColumnsMode
{
    //
    // Сводка:
    //     No sizing. Default column width or defined width set to column.
    None,
    //
    // Сводка:
    //     Calculates the width of column based on header and cell contents. So that header
    //     and cell content’s are not truncated.
    AllCells,
    //
    // Сводка:
    //     Applies AutoSizeColumnsMode.AllCells width to all the columns except last column
    //     which is visible and the remaining width from total width of SfDataGrid is set
    //     to last column.
    LastColumnFill,
    //
    // Сводка:
    //     Applies AutoSizeColumnsMode.AllCells width to all the columns except last column
    //     which is visible and sets the maximum between last column auto spacing width
    //     and remaining width to last column.
    AllCellsWithLastColumnFill,
    //
    // Сводка:
    //     Calculates the width of column based on cell contents. So that cell content’s
    //     are not truncated.
    AllCellsExceptHeader,
    //
    // Сводка:
    //     Calculates the width of column based on header content. So that header content
    //     is not truncated.
    ColumnHeader,
    //
    // Сводка:
    //     Divides the total width equally for columns.
    Fill
}

public enum HorizontalAlignment
{
    /// <summary>
    ///  The object or text is aligned on the left of the control element.
    /// </summary>
    Left = 0,

    /// <summary>
    ///  The object or text is aligned on the right of the control element.
    /// </summary>
    Right = 1,

    /// <summary>
    ///  The object or text is aligned in the center of the control element.
    /// </summary>
    Center = 2,
}

public enum ColumnFormat
{
    Default,
    Currency,
    Progress
}

public enum SQLCommand 
{ 
    Delete, 
    Undelete, 
    Wipe 
}

public enum Privilege 
{ 
    Select, 
    Insert, 
    Update, 
    Delete 
}

public enum SystemOperation 
{ 
    Accept, 
    Delete, 
    DeleteChilds 
}

public enum BaseDeduction
{
    [Description("Заработная плата")]
    Salary,

    [Description("Материалы")]
    Material,

    [Description("Фикс. сумма")]
    Person
}

public enum MaterialKind
{
    [Description("Не определён")]
    Undefined,

    [Description("Провод")]
    Wire,

    [Description("Контакт")]
    Terminal,

    [Description("Колодка")]
    Housing,

    [Description("Уплотнитель")]
    Seal
}

public enum PriceSettingMethod
{
    [Description("Средняя цена")]
    Average,

    [Description("Справочник")]
    Dictionary,

    [Description("Ручной ввод")]
    Manual
}


public enum VatRate
{
    [Description("Без НДС")]
    WidthoutVat = 0,

    [Description("10%")]
    Vat10 = 10,

    [Description("20%")]
    Vat20 = 20
}

public enum PurchaseState 
{
    [Description("Создана")]
    NotActive,

    [Description("В работе")]
    Active,

    [Description("Отменена")]
    Canceled,

    [Description("Выполнена")]
    Completed 
}

public enum MessageDestination 
{ 
    Object, 
    List 
}

public enum MessageAction 
{ 
    Refresh, 
    Add, 
    Delete 
}

public enum DateRange
{
    CurrentDay,
    CurrentMonth,
    CurrentQuarter,
    CurrentYear
}

public enum BalanceCategory 
{ 
    Debet, 
    Credit 
}

public enum DebtType 
{
    [Description("Наш долг")]
    Organization,

    [Description("Долг контрагента")]
    Contractor 
}

public enum LotState 
{
    [Description("Создана")]
    Created,

    [Description("В производстве")]
    Production,

    [Description("Выполнена")]
    Completed 
}

public enum BalanceSheetContent 
{ 
    Material, 
    Goods 
}

public enum StimulatingValue
{
    [Description("Сумма")]
    Money,

    [Description("Процент")]
    Percent
}

public enum CalculationState
{
    [Description("Подготовлена")]
    Prepare,

    [Description("Утверждена")]
    Approved,

    [Description("В архиве")]
    Expired
}

public enum PaymentDirection 
{
    [Description("Приход")]
    Income,

    [Description("Расход")]
    Expense 
}