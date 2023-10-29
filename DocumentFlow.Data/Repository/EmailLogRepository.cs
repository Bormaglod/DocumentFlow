//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.02.2022
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Data.Exceptions;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Data.Tools;

using SqlKata;
using SqlKata.Execution;

namespace DocumentFlow.Data.Repository;

public class EmailLogRepository : Repository<long, EmailLog>, IEmailLogRepository
{
    private readonly IDatabase database;

    public EmailLogRepository(IDatabase database) : base(database) 
    { 
        this.database = database;
    }

    public IReadOnlyList<EmailLog> GetEmails(Guid? group_id, Guid? contractorId)
    {
        using var conn = GetConnection();
        return GetUserDefinedQuery(conn)
            .When(group_id != null, q => q.Where("cp.id", group_id!.Value))
            .When(contractorId != null, q => q.Where("email_log.contractor_id", contractorId!.Value))
            .Get<EmailLog>()
            .ToList();
    }

    public async Task Log(EmailLog log)
    {
        using var conn = GetConnection();
        using var transaction = conn.BeginTransaction();
        
        try
        {
            log.DateTimeSending = DateTime.Now;
            
            await conn.ExecuteAsync("insert into email_log (email_id, date_time_sending, to_address, document_id, contractor_id) values (:EmailId, :DateTimeSending, :ToAddress, :DocumentId, :ContractorId)", log, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e, database), e);
        }
    }

    public async Task Log(IEnumerable<EmailLog> logs)
    {
        using var conn = GetConnection();
        using var transaction = conn.BeginTransaction();

        try
        {
            var dateTime = DateTime.Now;
            foreach (var item in logs)
            {
                item.DateTimeSending = dateTime;
                await conn.ExecuteAsync("insert into email_log (email_id, date_time_sending, to_address, document_id, contractor_id) values (:EmailId, :DateTimeSending, :ToAddress, :DocumentId, :ContractorId)", item, transaction);
            }

            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new RepositoryException(ExceptionHelper.Message(e, database), e);
        }
    }

    protected override Query GetUserDefinedQuery(Query query, IFilter? filter = null)
    {
        return query
            .Select("email_log.*")
            .Select("dt.{code, document_name}")
            .Select("bd.{document_date, document_number}")
            .Select("c.item_name as contractor_name")
            .Select("cp.item_name as contractor_group")
            .Select("c.parent_id as contractor_group_id")
            .Join("base_document as bd", "bd.id", "email_log.document_id")
            .LeftJoin("document_type as dt", callback: j => j.WhereRaw("dt.code = bd.tableoid::regclass::varchar"))
            .Join("contractor as c", "c.id", "email_log.contractor_id")
            .LeftJoin("contractor as cp", "cp.id", "c.parent_id");
    }
}