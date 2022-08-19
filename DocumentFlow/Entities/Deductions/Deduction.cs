﻿//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

using Humanizer;

namespace DocumentFlow.Entities.Deductions;

public enum BaseDeduction { NotDefined, Salary, Material, Person }

[Description("Удержание")]
public class Deduction : Directory
{
    [EnumType("base_deduction")]
    public string? base_calc { get; set; }
    public Guid? person_id { get; set; }
    public decimal value { get; set; }
    public decimal? fix_value { get; protected set; }
    public decimal? percent_value { get; protected set; }
    public string? base_calc_text => BaseDeductions[BaseDeduction];

    public BaseDeduction BaseDeduction
    {
        get 
        { 
            if (base_calc == null)
            {
                return BaseDeduction.NotDefined;
            }

            return Enum.Parse<BaseDeduction>(base_calc.Pascalize()); 
        }
        protected set { base_calc = value == BaseDeduction.NotDefined ? null : value.ToString().Underscore(); }
    }

    public static Dictionary<BaseDeduction, string> BaseDeductions = new()
    {
        [BaseDeduction.Salary] = "Заработная плата",
        [BaseDeduction.Material] = "Материалы",
        [BaseDeduction.Person] = "Фикс. сумма",
        [BaseDeduction.NotDefined] = string.Empty
    };
}