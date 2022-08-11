//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Entities.PurchaseRequestLib;

[Menu(MenuDestination.Document, "Заявка на приобретение материалов", 101000)]
public interface IPurchaseRequestBrowser : IBrowser<PurchaseRequest>
{
}
