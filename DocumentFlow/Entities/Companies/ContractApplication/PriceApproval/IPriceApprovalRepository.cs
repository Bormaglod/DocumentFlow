//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.01.2022
//
// Версия 2022.8.19
//  - добавлен метод GetPrice
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Products;

namespace DocumentFlow.Entities.Companies;

public interface IPriceApprovalRepository : IOwnedRepository<long, PriceApproval>
{
    PriceApproval? GetPrice(ContractApplication contractApplication, Product product);
}
