//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

public class Company : Directory
{
    private string? fullName;
    private decimal? inn;
    private decimal? kpp;
    private decimal? ogrn;
    private decimal? okpo;
    private Guid? okopfId;
    private Guid? accountId;

    public string? FullName
    {
        get => fullName;
        set
        {
            if (fullName != value) 
            { 
                fullName = value;
                NotifyPropertyChanged();
            }
        }
    }

    public decimal? Inn
    {
        get => inn;
        set
        {
            if (inn != value)
            {
                inn = value;
                NotifyPropertyChanged();
            }
        }
    }

    public decimal? Kpp
    {
        get => kpp;
        set
        {
            if (kpp != value) 
            { 
                kpp = value;
                NotifyPropertyChanged();
            }
        }
    }

    public decimal? Ogrn
    {
        get => ogrn;
        set
        {
            if (ogrn != value)
            {
                ogrn = value;
                NotifyPropertyChanged();
            }
        }
    }

    public decimal? Okpo
    {
        get => okpo;
        set
        {
            if (okpo != value) 
            { 
                okpo = value;
                NotifyPropertyChanged();
            }
        }
    }

    public Guid? OkopfId
    {
        get => okopfId;
        set
        {
            if (okopfId != value)
            {
                okopfId = value;
                NotifyPropertyChanged();
            }
        }
    }

    [DenyCopying]
    public Guid? AccountId
    {
        get => accountId;
        set
        {
            if (accountId != value)
            {
                accountId = value;
                NotifyPropertyChanged();
            }
        }
    }

    [Computed]
    public string? OkopfName { get; protected set; }
}
