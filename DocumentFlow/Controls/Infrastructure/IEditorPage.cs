//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.09.2021
//
// Версия 2022.12.30
//  - добавлен метод RegisterReport (перенесен из IPage)
//
//-----------------------------------------------------------------------

using DocumentFlow.ReportEngine.Infrastructure;

namespace DocumentFlow.Controls.Infrastructure;

public interface IEditorPage : IPage
{
    Guid? Id { get; }
    bool Save();
    void SetEntityParameters(Guid? id, Guid? owner_id, Guid? parent_id, bool readOnly);
    void RegisterReport(IReport report);
}
