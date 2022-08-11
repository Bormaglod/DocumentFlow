//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.ReportEngine.Infrastructure;

namespace DocumentFlow.Controls.Infrastructure;

public interface IPage
{
    string Text { get; }
    void OnPageClosing();
    void RegisterReport(IReport report);
}
