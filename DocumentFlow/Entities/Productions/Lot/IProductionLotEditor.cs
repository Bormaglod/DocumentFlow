﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Entities.Productions.Lot;

public interface IProductionLotEditor : IEditor<ProductionLot, IProductionLotBrowser>
{
}
