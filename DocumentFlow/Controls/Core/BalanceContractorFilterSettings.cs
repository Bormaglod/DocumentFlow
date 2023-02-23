//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2023
//
// Версия 2023.2.23
//  - класс переименован из BalanceContractorFilterData в
//    BalanceContractorFilterSettings
//
//-----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DocumentFlow.Controls.Core
{
    public class BalanceContractorFilterSettings
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("contract_ids")]
        public IDictionary<Guid, Guid?>? ContractIdentifiers { get; set; }

        public Guid? GetContract(Guid contractorId)
        {
            if (ContractIdentifiers != null && ContractIdentifiers.TryGetValue(contractorId, out Guid? value)) 
            { 
                return value;
            }
            
            return null;
        }
    }
}
