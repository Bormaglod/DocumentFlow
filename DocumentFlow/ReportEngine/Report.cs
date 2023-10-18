//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Data.Tools;
using DocumentFlow.Dialogs.Interfaces;
using DocumentFlow.Interfaces;
using DocumentFlow.Settings;

using FastReport;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using System.Reflection;

namespace DocumentFlow.ReportEngine;

public abstract class Report<T> : IReport
    where T : IDocumentInfo
{
    private readonly IServiceProvider services;
    private readonly Report report = new();
    private readonly string? reportText;

    public Report(IServiceProvider services)
    {
        this.services = services;

        var r = services.GetRequiredService<IReportRepository>().Get(Id);
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

            var db = services.GetRequiredService<IDatabase>();

            var conn = report.Dictionary.Connections[0];
            conn.ConnectionString = db.ConnectionString;
            report.Prepare();
        }
    }

    private void ShowReport(T entity)
    {
        var ls = services.GetRequiredService<IOptions<LocalSettings>>().Value;

        string file = BaseReport.CreatePdfDocument(report, ls.Report.Resolution);

        var attr = typeof(T).GetCustomAttribute<EntityNameAttribute>();
        string title = $"{attr?.Name ?? string.Empty} {entity?.ToString() ?? string.Empty}";

        services
            .GetRequiredService<IPreviewReportForm>()
            .ShowReport(entity?.Id, file, title);
    }

    void IReport.Show(IDocumentInfo document)
    {
        if (document is T doc)
        {
            Show(doc);
        }
    }
}
