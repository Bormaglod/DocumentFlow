﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.10.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Dialogs.Interfaces;

public interface IPreviewReportForm : IDialog
{
    void ShowReport(string pdf, string title);
    void ShowReport(Guid? documentId, string pdf, string title);
}