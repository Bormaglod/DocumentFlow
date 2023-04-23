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

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

using Humanizer;

using System.Globalization;

namespace DocumentFlow.Entities.Wages;

public class BillingDocumentEditor<T> : DocumentEditor<T>
    where T : BillingDocument, new()
{
    public BillingDocumentEditor(IDocumentRepository<T> repository, IPageManager pageManager) 
        : base(repository, pageManager, true)
    {
        var months = new Dictionary<short, string>();
        for (short i = 1; i < 13; i++)
        {
            months.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Humanize());
        }

        EditorControls
            .AddPanel(panel =>
                panel
                    .SetHeight(32)
                    .SetDock(DockStyle.Top)
                    .AddControls(controls =>
                        controls
                            .AddChoice<short>(x => x.BillingMonth, "Расчётный период: месяц", choice =>
                                choice
                                    .SetChoiceValues(months)
                                    .SetHeaderWidth(170)
                                    .SetEditorWidth(200)
                                    .SetWidth(370)
                                    .SetDock(DockStyle.Left))
                            .AddIntergerTextBox<int>(x => x.BillingYear, "год", text =>
                                text
                                    .SetNumberGroupSeparator(string.Empty)
                                    .SetHeaderWidth(50)
                                    .SetWidth(300)
                                    .SetDock(DockStyle.Left)
                                    .SetHeaderTextAlign(ContentAlignment.MiddleRight))));
    }

    protected int BillingYear
    {
        get
        {
            var year = EditorControls.GetControl<IIntegerTextBoxControl<int>>(x => x.BillingYear);
            return year.NumericValue == null ? DateTime.Now.Year : year.NumericValue.Value;
        }
    }

    protected short BilingMonth
    {
        get
        {
            var month = EditorControls.GetControl<IChoiceControl<short>>(x => x.BillingMonth);
            return month.SelectedValue == null ? Convert.ToInt16(DateTime.Now.Year) : month.SelectedValue.Value;
        }
    }
}
