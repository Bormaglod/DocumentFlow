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
    public Guid OrganizationId { get; set; }
    public string? ContractorName { get; protected set; }
    public bool TaxPayer { get; set; }
    public bool IsDefault { get; set; }
    public DateTime DocumentDate { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public Guid? SignatoryId { get; set; }
    public string? SignatoryName { get; protected set; }
    public string? SignatoryPost { get; protected set; }
    public Guid? OrgSignatoryId { get; set; }
    public string? OrgSignatoryName { get; protected set; }
    public string? OrgSignatoryPost { get; protected set; }

    [EnumType("contractor_type")]
    public string CType { get; set; } = "buyer";

    public string CTypeText => ContractorTypes[ContractorType];

    public ContractorType ContractorType 
    { 
        get { return Enum.Parse<ContractorType>(CType.Pascalize()); }
        protected set { CType = value.ToString().Underscore(); }
    }

    public static IReadOnlyDictionary<ContractorType, string> ContractorTypes => contractorTypes;

    public override string ToString()
    {
        return $"{ItemName} {Code} от {DocumentDate:d}";
    }
}
