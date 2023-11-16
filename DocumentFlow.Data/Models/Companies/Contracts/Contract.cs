//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Tools;
using DocumentFlow.Tools;

using Humanizer;

namespace DocumentFlow.Data.Models;

[EntityName("Договор")]
public class Contract : Directory
{
    private bool taxPayer;
    private bool isDefault;
    private DateTime documentDate = DateTime.Today;
    private DateTime dateStart = DateTime.Today;
    private DateTime? dateEnd;
    private Guid? signatoryId;
    private Guid? orgSignatoryId;
    private string cType = "buyer";
    private short? paymentPeriod;

    [Write(false)]
    public Guid OrganizationId { get; set; }

    [Computed]
    public string? ContractorName { get; set; }

    public bool TaxPayer 
    {
        get => taxPayer;
        set => SetProperty(ref taxPayer, value);
    }

    public bool IsDefault 
    { 
        get => isDefault;
        set => SetProperty(ref isDefault, value);
    }

    public DateTime DocumentDate 
    { 
        get => documentDate;
        set => SetProperty(ref documentDate, value);
    }

    public DateTime DateStart
    {
        get => dateStart;
        set => SetProperty(ref dateStart, value);
    }

    public DateTime? DateEnd
    {
        get => dateEnd;
        set => SetProperty(ref dateEnd, value);
    }

    public Guid? SignatoryId 
    { 
        get => signatoryId;
        set => SetProperty(ref signatoryId, value);
    }

    public Guid? OrgSignatoryId 
    { 
        get => orgSignatoryId;
        set => SetProperty(ref orgSignatoryId, value);
    }

    [EnumType("contractor_type")]
    public string CType 
    { 
        get => cType;
        set => SetProperty(ref cType, value);
    }

    public short? PaymentPeriod 
    { 
        get => paymentPeriod;
        set => SetProperty(ref paymentPeriod, value);
    }

    [Computed]
    public string? SignatoryName { get; protected set; }

    [Computed]
    public string? SignatoryPost { get; protected set; }

    [Computed]
    public string? OrgSignatoryName { get; protected set; }

    [Computed]
    public string? OrgSignatoryPost { get; protected set; }

    [Computed]
    public string CTypeText => ContractorType.Description();

    [Computed]
    public ContractorType ContractorType 
    { 
        get { return Enum.Parse<ContractorType>(CType.Pascalize()); }
        protected set { CType = value.ToString().Underscore(); }
    }

    public override string ToString()
    {
        return $"{ItemName} {Code} от {DocumentDate:d}";
    }
}
