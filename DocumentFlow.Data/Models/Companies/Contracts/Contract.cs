//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;
using DocumentFlow.Tools;

using Humanizer;

using System.ComponentModel;

namespace DocumentFlow.Data.Models;

public enum ContractorType 
{
    [Description("С продавцом")]
    Seller,

    [Description("С покупателем")]
    Buyer 
}

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
        set
        {
            if (taxPayer != value) 
            { 
                taxPayer = value;
                NotifyPropertyChanged();
            }
        }
    }

    public bool IsDefault 
    { 
        get => isDefault;
        set
        {
            if (isDefault != value) 
            {
                isDefault = value;
                NotifyPropertyChanged();
            }
        }
    }

    public DateTime DocumentDate 
    { 
        get => documentDate; 
        set
        {
            if (documentDate != value) 
            { 
                documentDate = value;
                NotifyPropertyChanged();
            }
        }
    }

    public DateTime DateStart
    {
        get => dateStart;
        set
        {
            if (dateStart != value) 
            { 
                dateStart = value;
                NotifyPropertyChanged();
            }
        }
    }

    public DateTime? DateEnd
    {
        get => dateEnd;
        set
        {
            if (dateEnd != value)
            {
                dateEnd = value;
                NotifyPropertyChanged();
            }
        }
    }

    public Guid? SignatoryId 
    { 
        get => signatoryId; 
        set
        {
            if (signatoryId != value)
            {
                signatoryId = value;
                NotifyPropertyChanged();
            }
        }
    }

    public Guid? OrgSignatoryId 
    { 
        get => orgSignatoryId; 
        set
        {
            if (orgSignatoryId != value)
            {
                orgSignatoryId = value;
                NotifyPropertyChanged();
            }
        }
    }

    [EnumType("contractor_type")]
    public string CType 
    { 
        get => cType; 
        set
        {
            if (cType != value)
            {
                cType = value;
                NotifyPropertyChanged();
            }
        }
    }

    public short? PaymentPeriod 
    { 
        get => paymentPeriod; 
        set
        {
            if (paymentPeriod != value)
            {
                paymentPeriod = value;
                NotifyPropertyChanged();
            }
        }
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
