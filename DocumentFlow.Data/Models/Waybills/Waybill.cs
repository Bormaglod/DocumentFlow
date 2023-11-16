//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Models;

public class Waybill : ShipmentDocument
{
    private string? waybillNumber;
    private DateTime? waybillDate = DateTime.Now;
    private string? invoiceNumber;
    private DateTime? invoiceDate = DateTime.Now;
    private bool upd;

    /// <summary>
    /// Возвращает или устанавливает номер накладной (1С).
    /// </summary>
    public string? WaybillNumber 
    { 
        get => waybillNumber;
        set => SetProperty(ref waybillNumber, value);
    }

    /// <summary>
    /// Возвращает или устанавливает дату выдачи накладной (1С).
    /// </summary>
    public DateTime? WaybillDate 
    { 
        get => waybillDate;
        set => SetProperty(ref waybillDate, value);
    }

    /// <summary>
    /// Возвращает или устанавливает номер счёт-фактуры (1С).
    /// </summary>
    public string? InvoiceNumber 
    { 
        get => invoiceNumber;
        set => SetProperty(ref invoiceNumber, value);
    }

    /// <summary>
    /// Возвращает или устанавливает дату выдачи счёт-фактуры (1С).
    /// </summary>
    public DateTime? InvoiceDate 
    { 
        get => invoiceDate;
        set => SetProperty(ref invoiceDate, value);
    }

    /// <summary>
    /// Возвращает или устанавливает флаг, определяющий является ли документ универсальным передаточным документом или нет.
    /// </summary>
    public bool Upd 
    { 
        get => upd;
        set => SetProperty(ref upd, value);
    }

    public decimal ProductCost { get; protected set; }
    public bool TaxPayer { get; protected set; }
    public int Tax { get; protected set; }
    public decimal TaxValue { get; protected set; }
    public decimal FullCost { get; protected set; }
    public decimal Paid { get; protected set; }
}
