//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.07.2022
//
// Версия 2022.12.6
//  - добавлено свойство OwnerIdentifier
// Версия 2022.12.17
//  - добавлен метод CreateQuery(string tableName);
// Версия 2023.1.8
//  - добавлен метод Configure и WriteConfigure (реализация метода
//    интерфейса IBalanceContractorFilter
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Settings.Infrastructure;

using SqlKata;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DocumentFlow.Controls;

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

    public BalanceSheetFilter()
    {
        InitializeComponent();

        list = new List<ViewInfo>() {
            new ViewInfo(BalanceSheetContent.Material, "Материалы"),
            new ViewInfo(BalanceSheetContent.Goods, "Продукция")
        };

        comboView.DataSource = list;

        Content = BalanceSheetContent.Material;
    }

    public Guid? OwnerIdentifier { get; set; }

    public BalanceSheetContent Content
    {
        get
        {
            if (comboView.SelectedItem is ViewInfo view)
            {
                return view.View;
            }

            throw new Exception("Непредвиденная ошибка.");
        }

        set
        {
            var newValue = list.First(x => x.View == value);
            comboView.SelectedItem = newValue;
        }
    }

    public bool AmountVisible
    {
        get => checkAmount.Checked;
        set => checkAmount.Checked = value;
    }

    public bool SummaVisible
    {
        get => checkSumma.Checked;
        set => checkSumma.Checked = value;
    }

    public bool DateFromEnabled
    {
        get => dateRangeControl1.FromEnabled;
        set => dateRangeControl1.FromEnabled = value;
    }
    public bool DateToEnabled
    {
        get => dateRangeControl1.ToEnabled;
        set => dateRangeControl1.ToEnabled = value;
    }

    public DateTime? DateFrom
    {
        get => dateRangeControl1.From;
        set => dateRangeControl1.From = value;
    }

    public DateTime? DateTo
    {
        get => dateRangeControl1.To;
        set => dateRangeControl1.To = value;
    }

    public void Configure(IAppSettings appSettings)
    {
        var data = appSettings.Get<BalanceSheetFilterData>("balance_sheet:filter");

        DateFromEnabled = data.DateFromEnabled;
        DateToEnabled = data.DateToEnabled;
        DateFrom = data.DateFrom;
        DateTo = data.DateTo;
        Content = data.Content;
        AmountVisible = data.AmountVisible;
        SummaVisible = data.SummaVisible;
    }

    public void WriteConfigure(IAppSettings appSettings)
    {
        BalanceSheetFilterData data = new()
        {
            DateFromEnabled = DateFromEnabled,
            DateToEnabled = DateToEnabled,
            DateFrom = DateFrom,
            DateTo = DateTo,
            Content = Content,
            AmountVisible = AmountVisible,
            SummaVisible = SummaVisible
        };

        appSettings.Write("balance_sheet:filter", data);
    }

    public void SetDateRange(DateRange range) => dateRangeControl1.SetRange(range);

    public Control Control => this;

    public Query? CreateQuery<T>() => null;
    public Query? CreateQuery(string tableName) => null;
}
