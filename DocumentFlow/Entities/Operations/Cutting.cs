//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2021
//
// Версия 2023.6.16
//  - Атрибут Exclude заменен на AllowOperation(DataOperation.None)
//
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

    [AllowOperation(DataOperation.None)]
    public Stripping Left 
    {
        get => new() { Cleaning = LeftCleaning, Sweep = LeftSweep };
        set => (LeftCleaning, LeftSweep) = (value.Cleaning, value.Sweep);
    }

    [AllowOperation(DataOperation.None)]
    public Stripping Right
    {
        get => new() { Cleaning = RightCleaning, Sweep = RightSweep };
        set => (RightCleaning, RightSweep) = (value.Cleaning, value.Sweep);
    }
}
