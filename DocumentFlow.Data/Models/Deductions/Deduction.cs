﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Tools;
using DocumentFlow.Tools;

using Humanizer;

using System.ComponentModel;

namespace DocumentFlow.Data.Models;

[Description("Удержание")]
public class Deduction : Directory
{
    private string baseCalc = "material";
    private Guid? personId;
    private decimal decimalValue;

    [EnumType("base_deduction")]
    public string BaseCalc 
    { 
        get => baseCalc;
        set => SetProperty(ref baseCalc, value);
    }

    public Guid? PersonId 
    { 
        get => personId;
        set => SetProperty(ref personId, value);
    }

    public decimal Value 
    { 
        get => decimalValue;
        set => SetProperty(ref decimalValue, value);
    }

    [Computed]
    public decimal? FixValue { get; protected set; }
    
    [Computed]
    public decimal? PercentValue { get; protected set; }

    public string BaseCalcText => BaseDeduction.Description();

    [Write(false)]
    public BaseDeduction BaseDeduction
    {
        get => Enum.Parse<BaseDeduction>(BaseCalc.Pascalize());
        set => BaseCalc = value.ToString().Underscore();
    }
}
