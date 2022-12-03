//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.09.2020
//
// Версия 2022.12.3
//  - добавлено перечисление SelectOptions
//  - параметр includeDeleted в методах заменён на options
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;

using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Controls.Core;

public class SummaryRowData : ISummary
{
    private readonly GridTableSummaryRow tableSummaryRow;
    private readonly GridSummaryRow? groupSummaryRow;

    public SummaryRowData(GridTableSummaryRow tableRow, GridSummaryRow? groupRow) => (tableSummaryRow, groupSummaryRow) = (tableRow, groupRow);

    public ISummary AsCount(GridColumn column, string format, SelectOptions options = SelectOptions.None)
    {
        return AsCount(column.MappingName, format, options);
    }

    public ISummary AsCount(string columnName, string format, SelectOptions options = SelectOptions.None)
    {
        return AddColumn(
            columnName,
            options == SelectOptions.All ? SummaryType.CountAggregate : SummaryType.Custom,
            format,
            GetNameFunction(SummaryType.CountAggregate, options));
    }

    public ISummary AsCount(GridColumn column, SummaryColumnFormat format = SummaryColumnFormat.None, SelectOptions options = SelectOptions.None)
    {
        return AsCount(column.MappingName, format, options);
    }

    public ISummary AsCount(string columnName, SummaryColumnFormat format = SummaryColumnFormat.None, SelectOptions options = SelectOptions.None)
    {
        return AddColumn(
            columnName,
            options == SelectOptions.All ? SummaryType.CountAggregate : SummaryType.Custom,
            format,
            GetNameFunction(SummaryType.CountAggregate, options));
    }

    public ISummary AsSummary(GridColumn column, string format, SelectOptions options = SelectOptions.None)
    {
        return AsSummary(column.MappingName, format, options);
    }

    public ISummary AsSummary(string columnName, string format, SelectOptions options = SelectOptions.None)
    {
        return AddColumn(
            columnName,
            options == SelectOptions.All ? SummaryType.DoubleAggregate : SummaryType.Custom,
            format,
            GetNameFunction(SummaryType.DoubleAggregate, options));
    }

    public ISummary AsSummary(GridColumn column, SummaryColumnFormat format = SummaryColumnFormat.None, SelectOptions options = SelectOptions.None)
    {
        return AsSummary(column.MappingName, format, options);
    }

    public ISummary AsSummary(string columnName, SummaryColumnFormat format = SummaryColumnFormat.None, SelectOptions options = SelectOptions.None)
    {
        return AddColumn(
            columnName,
            options == SelectOptions.All ? SummaryType.DoubleAggregate : SummaryType.Custom,
            format,
            GetNameFunction(SummaryType.DoubleAggregate, options));
    }

    private ISummary AddColumn(string columnName, SummaryType type, string format, string formatType)
    {
        string formatSummary = format.Replace("?", formatType);

        return CreateSummaryColumn(columnName, type, formatSummary);
    }

    private ISummary AddColumn(string columnName, SummaryType type, SummaryColumnFormat format, string formatType)
    {
        string formatSummary = format switch
        {
            SummaryColumnFormat.Currency => $"{{{formatType}:c}}",
            _ => $"{{{formatType}}}"
        };

        return CreateSummaryColumn(columnName, type, formatSummary);
    }

    private ISummary CreateSummaryColumn(string columnName, SummaryType type, string format)
    {
        var summaryColumn = new GridSummaryColumn
        {
            SummaryType = type,
            Format = format,
            MappingName = columnName,
            Name = columnName
        };

        if (type == SummaryType.Custom)
        {
            summaryColumn.CustomAggregate = new DocumentInfoAggregate();
        }

        tableSummaryRow.SummaryColumns.Add(summaryColumn);
        if (groupSummaryRow != null)
        {
            groupSummaryRow.SummaryColumns.Add(summaryColumn);
        }

        return this;
    }

    private static string GetNameFunction(SelectOptions options)
    {
        // если установлены флаги для подсчёта и удалённых и не проведённых записей, то считать надо всё
        if (options.HasFlag(SelectOptions.IncludeDeleted) && options.HasFlag(SelectOptions.IncludeNotAccepted))
        {
            return string.Empty;
        }

        // если установлен флаг для включения в расчёт не проведённых записей, то считаем все записи, исключая удалённые
        if (options.HasFlag(SelectOptions.IncludeNotAccepted))
        {
            return "ExceptDeleted";
        }

        // если установлен флаг для включения в расчёт удалённых записей, то считаем все записи, исключая не проведённые
        if (options.HasFlag(SelectOptions.IncludeDeleted))
        {
            return "OnlyAccepted";
        }

        // считаем все не удалённые и только проведённые (для документов) записи
        return "LegalRows";
    }

    private static string GetNameFunction(SummaryType summaryType, SelectOptions options)
    {
        string functionPrefix = summaryType == SummaryType.CountAggregate ? "Count" : "Sum";
        return $"{functionPrefix}{GetNameFunction(options)}";
    }
}
