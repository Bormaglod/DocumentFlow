//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//
// Версия 2023.3.17
//  - перенесено из DocumentFlow.Data.Core в DocumentFlow.Data
// Версия 2022.6.16
//  - Атрибут DataOperation заменен на AllowOperation
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Data;

public class BaseDocument : DocumentInfo
{
    public Guid OrganizationId { get; protected set; }

    public string? OrganizationName { get; protected set; }

    [AllowOperation(DataOperation.Add | DataOperation.Update)]
    public DateTime? DocumentDate { get; set; }

    [AllowOperation(DataOperation.Add | DataOperation.Update)]
    public int? DocumentNumber { get; set; }

    public void SetOrganization(Guid orgId) => OrganizationId = orgId;

    public override string ToString() => $"№{DocumentNumber} от {DocumentDate:d}";
}
