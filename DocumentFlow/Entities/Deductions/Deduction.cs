//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.11.2021
//
// Версия 2023.1.21
//  - поле BaseDeductions заменено на свойство и поменяло тип на
//    IReadOnlyDictionary
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

using Humanizer;

namespace DocumentFlow.Entities.Deductions;

public enum BaseDeduction { NotDefined, Salary, Material, Person }

[Description("Удержание")]
public class Deduction : Directory
{
    private static readonly Dictionary<BaseDeduction, string> baseDeductions = new()
    {
        [BaseDeduction.Salary] = "Заработная плата",
        [BaseDeduction.Material] = "Материалы",
        [BaseDeduction.Person] = "Фикс. сумма",
        [BaseDeduction.NotDefined] = string.Empty
    };

    [EnumType("base_deduction")]
    public string? BaseCalc { get; set; }
    public Guid? PersonId { get; set; }
    public decimal Value { get; set; }
    public decimal? FixValue { get; protected set; }
    public decimal? PercentValue { get; protected set; }
    public string? BaseCalcText => BaseDeductions[BaseDeduction];

    public BaseDeduction BaseDeduction
    {
        get 
        { 
            if (BaseCalc == null)
            {
                return BaseDeduction.NotDefined;
            }

            return Enum.Parse<BaseDeduction>(BaseCalc.Pascalize()); 
        }
        protected set { BaseCalc = value == BaseDeduction.NotDefined ? null : value.ToString().Underscore(); }
    }

    public static IReadOnlyDictionary<BaseDeduction, string> BaseDeductions => baseDeductions;
}
