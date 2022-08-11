//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Operations;

[Description("Резка")]
public class Cutting : Operation
{
    public int segment_length { get; set; }
    public decimal left_cleaning { get; set; }
    public int left_sweep { get; set; }
    public decimal right_cleaning { get; set; }
    public int right_sweep { get; set; }
    public int? program_number { get; set; }

    [Exclude]
    public Stripping Left 
    {
        get => new() { Cleaning = left_cleaning, Sweep = left_sweep };
        set => (left_cleaning, left_sweep) = (value.Cleaning, value.Sweep);
    }

    [Exclude]
    public Stripping Right
    {
        get => new() { Cleaning = right_cleaning, Sweep = right_sweep };
        set => (right_cleaning, right_sweep) = (value.Cleaning, value.Sweep);
    }
}
