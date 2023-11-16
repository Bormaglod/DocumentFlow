//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Резка")]
public class Cutting : Operation
{
    private int segmentLength;
    private decimal leftCleaning;
    private int leftSweep;
    private decimal rightCleaning;
    private int rightSweep;
    private int? programNumber;

    /// <summary>
    /// Возвращает или устанавливает длину провода.
    /// </summary>
    public int SegmentLength
    {
        get => segmentLength;
        set => SetProperty(ref segmentLength, value);
    }

    /// <summary>
    /// Возвращает или устанавливает длину зачистки с начала провода.
    /// </summary>
    public decimal LeftCleaning 
    { 
        get => leftCleaning;
        set => SetProperty(ref leftCleaning, value);
    }

    /// <summary>
    /// Возвращает или устанавливает ширинк окна на которое снимается изоляция в начале провода.
    /// </summary>
    public int LeftSweep 
    { 
        get => leftSweep;
        set => SetProperty(ref leftSweep, value);
    }

    /// <summary>
    /// Возвращает или устанавливает длину зачистки с конца провода.
    /// </summary>
    public decimal RightCleaning 
    { 
        get => rightCleaning;
        set => SetProperty(ref rightCleaning, value);
    }

    /// <summary>
    /// Возвращает или устанавливает ширину окна на которое снимается изоляция в конце провода.
    /// </summary>
    public int RightSweep 
    { 
        get => rightSweep;
        set => SetProperty(ref rightSweep, value);
    }

    /// <summary>
    /// Возвращает или устанавливает номер используемой программы.
    /// </summary>
    [DenyCopying]
    public int? ProgramNumber 
    { 
        get => programNumber;
        set => SetProperty(ref programNumber, value);
    }
}
