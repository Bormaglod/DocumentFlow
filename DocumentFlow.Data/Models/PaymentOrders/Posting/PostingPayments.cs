//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Платёж")]
public class PostingPayments : AccountingDocument, IDiscriminator
{
    private decimal transactionAmount;
    private Guid documentId;

    /// <summary>
    /// Возвращает или устанавливает сумму операции.
    /// </summary>
    public decimal TransactionAmount 
    { 
        get => transactionAmount;
        set => SetProperty(ref transactionAmount, value);
    }

    /// <summary>
    /// Возвращает или устанавливает идентификатор документа с которым будет связана распределяемая сумма операции <see cref="TransactionAmount"/>.
    /// </summary>
    public Guid DocumentId 
    { 
        get => documentId;
        set => SetProperty(ref documentId, value);
    }

    /// <summary>
    /// Возвращает наименование документа с которым будет связана распределяемая сумма операции <see cref="TransactionAmount"/>.
    /// </summary>
    public string? DocumentName { get; protected set; }

    /// <summary>
    /// Возвращает наименование контрагента из документа <see cref="DocumentId"/>.
    /// </summary>
    [Write(false)]
    public string? ContractorName { get; set; }

    [Write(false)]
    public string? Discriminator { get; set; }
}
