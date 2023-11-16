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
        set => SetProperty(ref fullName, value);
    }

    public decimal? Inn
    {
        get => inn;
        set => SetProperty(ref inn, value);
    }

    public decimal? Kpp
    {
        get => kpp;
        set => SetProperty(ref kpp, value);
    }

    public decimal? Ogrn
    {
        get => ogrn;
        set => SetProperty(ref ogrn, value);
    }

    public decimal? Okpo
    {
        get => okpo;
        set => SetProperty(ref okpo, value);
    }

    public Guid? OkopfId
    {
        get => okopfId;
        set => SetProperty(ref okopfId, value);
    }

    [DenyCopying]
    public Guid? AccountId
    {
        get => accountId;
        set => SetProperty(ref accountId, value);
    }

    [Computed]
    public string? OkopfName { get; protected set; }
}
