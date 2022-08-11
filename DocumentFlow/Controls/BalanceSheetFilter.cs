//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

using SqlKata;

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

    public void SetDateRange(DateRange range) => dateRangeControl1.SetRange(range);

    public Control Control => this;

    public Query? CreateQuery<T>() => null;
}
