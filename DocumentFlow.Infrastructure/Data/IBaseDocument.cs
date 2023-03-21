﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.06.2022
//
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Data.Infrastructure в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Data;

public interface IBaseDocument : IDocumentInfo
{
    Guid OrganizationId { get; }
    string? OrganizationName { get; }
    DateTime? DocumentDate { get; set; }
    int? DocumentNumber { get; set; }
    void SetOrganization(Guid orgId);
}
