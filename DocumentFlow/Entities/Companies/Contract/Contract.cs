//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//
// Версия 2023.1.21
//  - поле ContractorTypes заменено на свойство и поменяло тип на
//    IReadOnlyDictionary
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

using Humanizer;

namespace DocumentFlow.Entities.Companies;

public enum ContractorType { Seller, Buyer }

[Description("Договор")]
public class Contract : Directory
{
    private static readonly Dictionary<ContractorType, string> contractorTypes = new()
    {
        [ContractorType.Seller] = "С продавцом",
        [ContractorType.Buyer] = "С покупателем"
    };

    [Exclude]
    public Guid organization_id { get; set; }
    public string? contractor_name { get; protected set; }
    public bool tax_payer { get; set; }
    public bool is_default { get; set; }
    public DateTime document_date { get; set; }
    public DateTime date_start { get; set; }
    public DateTime? date_end { get; set; }
    public Guid? signatory_id { get; set; }
    public string? signatory_name { get; protected set; }
    public string? signatory_post { get; protected set; }
    public Guid? org_signatory_id { get; set; }
    public string? org_signatory_name { get; protected set; }
    public string? org_signatory_post { get; protected set; }

    [EnumType("contractor_type")]
    public string c_type { get; set; } = "buyer";

    public string c_type_text => ContractorTypes[ContractorType];

    public ContractorType ContractorType 
    { 
        get { return Enum.Parse<ContractorType>(c_type.Pascalize()); }
        protected set { c_type = value.ToString().Underscore(); }
    }

    public static IReadOnlyDictionary<ContractorType, string> ContractorTypes => contractorTypes;

    public override string ToString()
    {
        return $"{item_name} {code} от {document_date:d}";
    }
}
