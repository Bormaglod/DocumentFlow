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
        set
        {
            if (segmentLength != value) 
            { 
                segmentLength = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает длину зачистки с начала провода.
    /// </summary>
    public decimal LeftCleaning 
    { 
        get => leftCleaning; 
        set
        {
            if (leftCleaning != value) 
            { 
                leftCleaning = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает ширинк окна на которое снимается изоляция в начале провода.
    /// </summary>
    public int LeftSweep 
    { 
        get => leftSweep; 
        set
        {
            if (leftSweep != value)
            {
                leftSweep = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает длину зачистки с конца провода.
    /// </summary>
    public decimal RightCleaning 
    { 
        get => rightCleaning; 
        set
        {
            if (rightCleaning != value)
            {
                rightCleaning = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает ширину окна на которое снимается изоляция в конце провода.
    /// </summary>
    public int RightSweep 
    { 
        get => rightSweep; 
        set
        {
            if (rightSweep != value)
            {
                rightSweep = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает номер используемой программы.
    /// </summary>
    [DenyCopying]
    public int? ProgramNumber 
    { 
        get => programNumber; 
        set
        {
            if (programNumber != value)
            {
                programNumber = value;
                NotifyPropertyChanged();
            }
        }
    }

    /*[AllowOperation(DataOperation.None)]
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
    }*/
}
