//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.12.2019
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data;

public class EmailLog : Identifier<long>
{
    [Display(AutoGenerateField = false)]
    public long EmailId { get; set; }

    [Display(Name = "Дата и время отправки")]
    public DateTime DateTimeSending { get; set; }

    [Display(Name = "Кому")]
    public string ToAddress { get; set; } = string.Empty;
    
    [Display(AutoGenerateField = false)]
    public Guid ContractorId { get; set; }

    [Display(Name = "Контрагент")]
    public string ContractorName { get; protected set; } = string.Empty;

    [Display(AutoGenerateField = false)]
    public Guid? ContractorGroupId { get; protected set; }

    [Display(AutoGenerateField = false)]
    public string? ContractorGroup { get; protected set; }

    [Display(AutoGenerateField = false)]
    public Guid? DocumentId { get; set; }

    [Display(AutoGenerateField = false)]
    public string? Code { get; protected set; }

    [Display(AutoGenerateField = false)]
    public string? DocumentName { get; protected set; }

    [Display(AutoGenerateField = false)]
    public DateTime DocumentDate { get; protected set; }

    [Display(AutoGenerateField = false)]
    public int DocumentNumber { get; protected set; }

    [Display(Name = "Отправленный документ")]
    public string Document => $"{DocumentName} № {DocumentNumber} от {DocumentDate:d}";
}
