//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.04.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Tools.Reflection;

using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;

using System.Linq.Expressions;

namespace DocumentFlow.Controls.Tools;

public class DataGridSummary<T> : IDataGridSummary<T>
    where T : IEntity<long>
{
    private readonly GridSummaryRow gridSummaryRow;

    public DataGridSummary(GridSummaryRow gridRow) => gridSummaryRow = gridRow;

    public IDataGridSummary<T> AsSummary(Expression<Func<T, object?>> memberExpression, string format, SelectOptions options = SelectOptions.None)
    {
        return AddColumn(
            memberExpression.ToMember().Name,
            options == SelectOptions.All ? SummaryType.DoubleAggregate : SummaryType.Custom,
            format,
            GetNameFunction(SummaryType.DoubleAggregate, options));
    }

    public IDataGridSummary<T> AsSummary(Expression<Func<T, object?>> memberExpression, SummaryColumnFormat format = SummaryColumnFormat.None, SelectOptions options = SelectOptions.None)
    {
        return AddColumn(
            memberExpression.ToMember().Name,
            options == SelectOptions.All ? SummaryType.DoubleAggregate : SummaryType.Custom,
            format,
            GetNameFunction(SummaryType.DoubleAggregate, options));
    }

    private IDataGridSummary<T> AddColumn(string columnName, SummaryType type, string format, string formatType)
    {
        string formatSummary = format.Replace("?", formatType);

        return CreateSummaryColumn(columnName, type, formatSummary);
    }

    private IDataGridSummary<T> AddColumn(string columnName, SummaryType type, SummaryColumnFormat format, string formatType)
    {
        string formatSummary = format switch
        {
            SummaryColumnFormat.Currency => $"{{{formatType}:c}}",
            _ => $"{{{formatType}}}"
        };

        return CreateSummaryColumn(columnName, type, formatSummary);
    }

    private IDataGridSummary<T> CreateSummaryColumn(string columnName, SummaryType type, string format)
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

        gridSummaryRow.SummaryColumns.Add(summaryColumn);

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