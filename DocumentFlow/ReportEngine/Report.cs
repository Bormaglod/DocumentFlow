//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.02.2022
//
// Версия 2022.9.3
//  - тема отчёта теперь формируется с использованием описания
//    класса (DescriptionAttribute)
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.ReportEngine.Infrastructure перемещено в DocumentFlow.Infrastructure.ReportEngine
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Dialogs;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.ReportEngine;

using FastReport;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace DocumentFlow.ReportEngine;

public abstract class Report<T> : IReport
    where T : IDocumentInfo
{
    private readonly Report report = new();
    private readonly string? reportText;

    public Report()
    {
        var r = Services.Provider.GetService<IReportRepository>()!.Get(Id);
        Name = r.Code;
        Title = r.Title;
        reportText = r.SchemaReport;
    }

    public void Show(T entity)
    {
        LoadAndPrepare(entity);
        ShowReport(entity);
    }

    public string Name { get; }

    public string Title { get; }

    protected abstract Guid Id { get; }

    protected Report GetReport() => report;

    protected virtual void SetParameterValues(T document) { }

    private void LoadAndPrepare(T entity)
    {
        if (reportText != null)
        {
            string header = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            report.LoadFromString(header + reportText);
            SetParameterValues(entity);
            report.Prepare();
        }
    }

    private void ShowReport(T entity)
    {
        string file = BaseReport.CreatePdfDocument(report);

        var attr = typeof(T).GetCustomAttribute<Data.Core.DescriptionAttribute>();
        string title = $"{attr?.Name ?? string.Empty} {entity?.ToString() ?? string.Empty}";

        PreviewReportForm.ShowReport(entity?.Id, file, title);
    }

    void IReport.Show(IDocumentInfo document)
    {
        if (document is T doc)
        {
            Show(doc);
        }
    }
}
