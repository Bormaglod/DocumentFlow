//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

using Humanizer;

using System.Globalization;

namespace DocumentFlow.Entities.Wages;

public class BillingDocumentEditor<T> : DocumentEditor<T>
    where T : BillingDocument, new()
{
    private readonly DfChoice<short> month;
    private readonly DfIntegerTextBox<int> year;

    public BillingDocumentEditor(IDocumentRepository<T> repository, IPageManager pageManager) : base(repository, pageManager, true)
    {
        month = new DfChoice<short>("billing_month", "Расчётный период: месяц", 170, 200)
        {
            Width = 370,
            Dock = DockStyle.Left
        };

        year = new DfIntegerTextBox<int>("billing_year", "год", 50, 100)
        {
            Width = 300,
            Dock = DockStyle.Left,
            HeaderTextAlign = ContentAlignment.MiddleRight,
            NumberGroupSeparator = string.Empty
        };

        var panel_calc_range = new Panel()
        {
            Dock = DockStyle.Top,
            Height = 32
        };

        var months = new Dictionary<short, string>();
        for (short i = 1; i < 13; i++)
        {
            months.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Humanize());
        }

        month.SetChoiceValues(months);

        panel_calc_range.Controls.AddRange(new Control[] { year, month });
    }

    protected int BillingYear => year.NumericValue == null ? DateTime.Now.Year : year.NumericValue.Value;

    protected short BilingMonth => month.ChoiceValue == null ? Convert.ToInt16(DateTime.Now.Year) : month.ChoiceValue.Value;
}
