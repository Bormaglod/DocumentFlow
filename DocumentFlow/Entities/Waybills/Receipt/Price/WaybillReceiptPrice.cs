//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Products;
using DocumentFlow.Entities.Products.Core;

namespace DocumentFlow.Entities.Waybills;

[ProductContent(ProductContent.Materials)]
public class WaybillReceiptPrice : ProductPrice
{
}
