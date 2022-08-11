//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.09.2020
// Time: 17:44
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

    public ISummary AsCount(GridColumn column, string format, bool includeDeleted = false)
    {
        return AsCount(column.MappingName, format, includeDeleted);
    }

    public ISummary AsCount(string columnName, string format, bool includeDeleted = false)
    {
        return AddColumn(
            columnName,
            includeDeleted ? SummaryType.CountAggregate : SummaryType.Custom,
            format,
            includeDeleted ? "Count" : "CountExceptDeleted");
    }

    public ISummary AsCount(GridColumn column, SummaryColumnFormat format = SummaryColumnFormat.None, bool includeDeleted = false)
    {
        return AsCount(column.MappingName, format, includeDeleted);
    }

    public ISummary AsCount(string columnName, SummaryColumnFormat format = SummaryColumnFormat.None, bool includeDeleted = false)
    {
        return AddColumn(
            columnName,
            includeDeleted ? SummaryType.CountAggregate : SummaryType.Custom,
            format,
            includeDeleted ? "Count" : "CountExceptDeleted");
    }

    public ISummary AsSummary(GridColumn column, string format, bool includeDeleted = false)
    {
        return AsSummary(column.MappingName, format, includeDeleted);
    }

    public ISummary AsSummary(string columnName, string format, bool includeDeleted = false)
    {
        return AddColumn(
            columnName,
            includeDeleted ? SummaryType.DoubleAggregate : SummaryType.Custom,
            format,
            includeDeleted ? "Sum" : "SumExceptDeleted");
    }

    public ISummary AsSummary(GridColumn column, SummaryColumnFormat format = SummaryColumnFormat.None, bool includeDeleted = false)
    {
        return AsSummary(column.MappingName, format, includeDeleted);
    }

    public ISummary AsSummary(string columnName, SummaryColumnFormat format = SummaryColumnFormat.None, bool includeDeleted = false)
    {
        return AddColumn(
            columnName,
            includeDeleted ? SummaryType.DoubleAggregate : SummaryType.Custom,
            format,
            includeDeleted ? "Sum" : "SumExceptDeleted");
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
}
