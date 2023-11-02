//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Settings;

using FluentDateTime;

using SqlKata;

using System.ComponentModel;

namespace DocumentFlow.Controls.Filters;

[ToolboxItem(false)]
public partial class BalanceSheetFilter : UserControl, IBalanceSheetFilter
{
    private class ViewInfo
    {
        public ViewInfo(BalanceSheetContent view, string name)
        {
            View = view;
            Name = name;
        }

        public BalanceSheetContent View { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
    }

    private readonly List<ViewInfo> list;

    private readonly BalanceSheetFilterSettings settings;

    public BalanceSheetFilter()
    {
        InitializeComponent();

        list = new List<ViewInfo>() {
            new ViewInfo(BalanceSheetContent.Material, "Материалы"),
            new ViewInfo(BalanceSheetContent.Goods, "Продукция")
        };

        comboView.DataSource = list;

        settings = new BalanceSheetFilterSettings()
        {
            DateFromEnabled = true,
            DateFrom = DateTime.Today.FirstDayOfYear(),
            DateToEnabled = true,
            DateTo = DateTime.Today.EndOfYear(),
            AmountVisible = true,
            SummaVisible = true,
            Content = BalanceSheetContent.Material,
            ShowGivingMaterial = true
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

    public bool AmountVisible
    {
        get => settings.AmountVisible;
        set
        {
            if (settings.AmountVisible != value)
            {
                settings.AmountVisible = value;
                checkAmount.Checked = value;
            }
        }
    }

    public bool SummaVisible
    {
        get => settings.SummaVisible;
        set
        {
            if (settings.SummaVisible != value)
            {
                settings.SummaVisible = value;
                checkSumma.Checked = value;
            }
        }
    }

    public BalanceSheetContent Content
    {
        get => settings.Content;
        set
        {
            if (settings.Content != value)
            {
                settings.Content = value;
                comboView.SelectedItem = list.First(x => x.View == value);
            }
        }
    }

    public bool ShowGivingMaterial 
    {
        get => settings.ShowGivingMaterial;
        set
        {
            if (settings.ShowGivingMaterial != value)
            {
                settings.ShowGivingMaterial = value;
                checkGivingMaterial.Checked = value;
            }
        }
    }

    public object? Settings => settings;

    public void SetDateRange(DateRange range) => dateRangeControl1.SetRange(range);
    public Query? CreateQuery<T>() => null;
    public Query? CreateQuery(string tableName) => null;

    public void SettingsLoaded()
    {
        dateRangeControl1.DateFrom = settings.DateFrom;
        dateRangeControl1.DateTo = settings.DateTo;
        dateRangeControl1.DateFromEnabled = settings.DateFromEnabled;
        dateRangeControl1.DateToEnabled = settings.DateToEnabled;
        checkAmount.Checked = settings.AmountVisible;
        checkSumma.Checked = settings.SummaVisible;
        comboView.SelectedItem = list.First(x => x.View == settings.Content);
        checkGivingMaterial.Checked = settings.ShowGivingMaterial;
    }

    private void DateRangeControl1_DateFromChanged(object sender, EventArgs e) => settings.DateFrom = dateRangeControl1.DateFrom;

    private void DateRangeControl1_DateToChanged(object sender, EventArgs e) => settings.DateTo = dateRangeControl1.DateTo;

    private void DateRangeControl1_DateFromEnabledChanged(object sender, EventArgs e) => settings.DateFromEnabled = dateRangeControl1.DateFromEnabled;

    private void DateRangeControl1_DateToEnabledChanged(object sender, EventArgs e) => settings.DateToEnabled = dateRangeControl1.DateToEnabled;

    private void CheckAmount_CheckedChanged(object sender, EventArgs e) => settings.AmountVisible = checkAmount.Checked;

    private void CheckSumma_CheckedChanged(object sender, EventArgs e) => settings.SummaVisible = checkSumma.Checked;

    private void ComboView_SelectedValueChanged(object sender, EventArgs e) => settings.Content = ((ViewInfo)comboView.SelectedItem).View;

    private void CheckGivingMaterial_CheckedChanged(object sender, EventArgs e) => settings.ShowGivingMaterial = checkGivingMaterial.Checked;
}
