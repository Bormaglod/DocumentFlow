//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.09.2021
//
// Версия 2022.12.30
//  - добавлен метод RegisterReport (перенесен из IPage)
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Controls.Infrastructure в DocumentFlow.Infrastructure.Controls
//  - DocumentFlow.ReportEngine.Infrastructure перемещено в DocumentFlow.Infrastructure.ReportEngine
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.ReportEngine;

namespace DocumentFlow.Infrastructure.Controls;

public interface IEditorPage : IPage
{
    Guid? Id { get; }
    bool Save();
    void SetEntityParameters(Guid? id, Guid? owner_id, Guid? parent_id, bool readOnly);
    void RegisterReport(IReport report);
}
