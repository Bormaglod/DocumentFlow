//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;
using DocumentFlow.Settings;

using FluentDateTime;

using Humanizer;

using Microsoft.Extensions.Configuration;

using SqlKata;

using System.ComponentModel;

namespace DocumentFlow.Controls.Filters;

[ToolboxItem(false)]
public partial class DocumentFilter : UserControl, IDocumentFilter
{
    private readonly DocumentFilterSettings settings;

    public DocumentFilter(IOrganizationRepository orgs)
    {
        InitializeComponent();

        var list = orgs.GetList();

        comboOrg.DataSource = list;
        comboOrg.SelectedItem = list.FirstOrDefault(x => x.DefaultOrg);

        settings = new DocumentFilterSettings()
        {
            DateFromEnabled = true,
            DateFrom = DateTime.Today.FirstDayOfYear(),
            DateToEnabled = true,
            DateTo = DateTime.Today.EndOfYear()
        };
    }

    public Guid? OwnerId { get; set; }

    public bool DateFromEnabled
    {
        get => settings.DateFromEnabled;
        set
        {
            if (settings.DateFromEnabled != value)
            {
                settings.DateFromEnabled = value;
                dateRangeControl1.DateFromEnabled = value;
            }
        }
    }

    public bool DateToEnabled
    {
        get => settings.DateToEnabled;
        set
        {
            if (settings.DateToEnabled != value)
            {
                settings.DateToEnabled = value;
                dateRangeControl1.DateToEnabled = value;
            }
        }
    }

    public DateTime DateFrom
    {
        get => settings.DateFrom;
        set
        {
            if (settings.DateFrom != value)
            {
                settings.DateFrom = value;
                dateRangeControl1.DateFrom = value;
            }
        }
    }

    public DateTime DateTo
    {
        get => settings.DateTo;
        set
        {
            if (settings.DateTo != value)
            {
                settings.DateTo = value;
                dateRangeControl1.DateTo = value;
            }
        }
    }

    public object? Settings => settings;

    public void SetDateRange(DateRange range) => dateRangeControl1.SetRange(range);

    public Query? CreateQuery<T>() => CreateQuery(typeof(T).Name.Underscore());

    public virtual Query? CreateQuery(string tableName)
    {
        var query = new Query();
        if (comboOrg.SelectedItem is Organization org)
        {
            query.Where($"{tableName}.organization_id", org.Id);
        }

        if (DateFromEnabled || DateToEnabled)
        {
            if (DateToEnabled && DateFromEnabled)
            {
                query.WhereBetween($"{tableName}.document_date", DateFrom, DateTo);
            }
            else if (DateToEnabled)
            {
                query.Where($"{tableName}.document_date", "<=", DateTo);
            }
            else if (DateFromEnabled)
            {
                query.Where($"{tableName}.document_date", ">=", DateFrom);
            }
        }

        if (query.Clauses.Count > 0)
        {
            return query;
        }

        return null;
    }

    public void SettingsLoaded()
    {
        dateRangeControl1.DateFrom = settings.DateFrom;
        dateRangeControl1.DateTo = settings.DateTo;
        dateRangeControl1.DateFromEnabled = settings.DateFromEnabled;
        dateRangeControl1.DateToEnabled = settings.DateToEnabled;
    }

    private void ButtonClearOrg_Click(object sender, EventArgs e) => comboOrg.SelectedIndex = -1;

    private void DateRangeControl1_DateFromChanged(object sender, EventArgs e) => settings.DateFrom = dateRangeControl1.DateFrom;

    private void DateRangeControl1_DateToChanged(object sender, EventArgs e) => settings.DateTo = dateRangeControl1.DateTo;

    private void DateRangeControl1_DateFromEnabledChanged(object sender, EventArgs e) => settings.DateFromEnabled = dateRangeControl1.DateFromEnabled;

    private void DateRangeControl1_DateToEnabledChanged(object sender, EventArgs e) => settings.DateToEnabled = dateRangeControl1.DateToEnabled;
}
