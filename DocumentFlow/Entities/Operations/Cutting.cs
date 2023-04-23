//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Controls.Core;

namespace DocumentFlow.Entities.Operations;

[Description("Резка")]
public class Cutting : Operation
{
    public int SegmentLength { get; set; }
    public decimal LeftCleaning { get; set; }
    public int LeftSweep { get; set; }
    public decimal RightCleaning { get; set; }
    public int RightSweep { get; set; }
    public int? ProgramNumber { get; set; }

    [Exclude]
    public Stripping Left 
    {
        get => new() { Cleaning = LeftCleaning, Sweep = LeftSweep };
        set => (LeftCleaning, LeftSweep) = (value.Cleaning, value.Sweep);
    }

    [Exclude]
    public Stripping Right
    {
        get => new() { Cleaning = RightCleaning, Sweep = RightSweep };
        set => (RightCleaning, RightSweep) = (value.Cleaning, value.Sweep);
    }
}
