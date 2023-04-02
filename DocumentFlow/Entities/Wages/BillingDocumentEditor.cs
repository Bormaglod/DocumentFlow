//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.08.2022
//
// Версия 2022.9.8
//  - не отображалась панель panel_calc_range. Исправлено.
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;

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
        var months = new Dictionary<short, string>();
        for (short i = 1; i < 13; i++)
        {
            months.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Humanize());
        }

        month = CreateChoice(x => x.BillingMonth, "Расчётный период: месяц", 170, 200, choices: months);
        month.Width = 370;
        month.Dock = DockStyle.Left;

        year = CreateIntegerTextBox<int>(x => x.BillingYear, "год", 50, 100);
        year.Width = 300;
        year.Dock = DockStyle.Left;
        year.HeaderTextAlign = ContentAlignment.MiddleRight;
        year.NumberGroupSeparator = string.Empty;

        var panel_calc_range = new Panel()
        {
            Dock = DockStyle.Top,
            Height = 32
        };

        panel_calc_range.Controls.AddRange(new Control[] { year, month });

        AddControls(new Control[] { panel_calc_range });
    }

    protected int BillingYear => year.NumericValue == null ? DateTime.Now.Year : year.NumericValue.Value;

    protected short BilingMonth => month.ChoiceValue == null ? Convert.ToInt16(DateTime.Now.Year) : month.ChoiceValue.Value;
}
