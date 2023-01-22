//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.02.2022
//
// Версия 2023.1.22
//  - DocumentFlow.ReportEngine.Infrastructure перемещено в DocumentFlow.Infrastructure.ReportEngine
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Infrastructure.ReportEngine;

public interface IReport
{
    string Name { get; }
    string Title { get; }
    void Show(IDocumentInfo document);
}
