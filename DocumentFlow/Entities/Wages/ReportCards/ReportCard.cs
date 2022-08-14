//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Wages;


[Description("Табель")]
public class ReportCard : BillingDocument
{
    private static readonly ReportCardLabelList labels = new()
    {
        new ReportCardLabel("Я", "Продолжительность работы в дневное время", false),
        new ReportCardLabel("В", "Выходные дни и нерабочие праздничные"),
        new ReportCardLabel("ОТ", "Ежегодные основной оплачиваемый отпуск"),
        new ReportCardLabel("Б", "Временная нетрудоспособность"),
        new ReportCardLabel("РВ", "Продолжительность работы в выходные и нерабочие праздничные дни", false),
        new ReportCardLabel("ДО", "Отпуск без сохранения заработной платы"),
        new ReportCardLabel("РП", "Время простоя по вине работодателя", false)
    };

    public static IReportCardLabelList Labels => labels;
}
