//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.06.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Interfaces;

public interface IBaseDocument : IDocumentInfo
{
    Guid OrganizationId { get; }
    string? OrganizationName { get; }
    DateTime? DocumentDate { get; set; }
    int? DocumentNumber { get; set; }
}
