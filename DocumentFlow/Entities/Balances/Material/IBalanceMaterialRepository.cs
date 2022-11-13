//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//
// Версия 2022.11.13
//  - наследование интерфейса изменено на IBalanceProductRepository
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.Balances;


public interface IBalanceMaterialRepository : IBalanceProductRepository<BalanceMaterial>
{
}
