//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Entities.Productions.Order;

[Menu(MenuDestination.Document, "Заказ на изготовление", 102010, "Производство")]
public interface IProductionOrderBrowser : IBrowser<ProductionOrder>
{
}
