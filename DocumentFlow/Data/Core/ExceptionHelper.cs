//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.03.2019
//
// Версия 2023.1.2
//  - добавлена поддержка для json-сообщений
// Версия 2023.3.17
//  - перенесено из DocumentFlow.Data в DocumentFlow.Data.Core
//
//-----------------------------------------------------------------------

using Dapper;

using DocumentFlow.Core;

using Npgsql;

using System.Text;
using System.Text.Json;

namespace DocumentFlow.Data.Core;

public static class ExceptionHelper
{
    public static void MesssageBox(Exception exception) => MessageBox.Show(Message(exception), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public static string Message(Exception exception)
    {
        StringBuilder stringBuilder = new();
        CreateMessage(stringBuilder, exception);
        return stringBuilder.ToString();
    }

    private static void CreateMessage(StringBuilder strings, Exception exception)
    {
        if (exception is PostgresException pgException)
        {
            if (pgException.SqlState == "P0001")
            {
                if (pgException.MessageText.IsJson())
                {
                    var message = JsonSerializer.Deserialize<ExceptionData>(pgException.MessageText);
                    if (message != null)
                    {
                        strings.AppendLine(message.Text);
                    }
                    else
                    {
                        strings.AppendLine(pgException.MessageText);
                    }
                }
                else
                {
                    strings.AppendLine(pgException.MessageText);
                }
            }
            else if (pgException.SqlState == "23514")
            {
                using var conn = new Database().OpenConnection();
                string msg = conn.QuerySingleOrDefault<string>($"select d.description from pg_catalog.pg_constraint c join pg_catalog.pg_description d on (d.objoid = c.oid) where conname = '{pgException.ConstraintName}'");
                if (string.IsNullOrEmpty(msg))
                {
                    strings.AppendLine(DefaultMessage(pgException));
                }
                else
                {
                    strings.AppendLine(msg);
                }
            }
            else
            {
                strings.AppendLine(DefaultMessage(pgException));
            }
        }
        else if (exception is AggregateException aggregateException)
        {
            foreach (Exception e in aggregateException.InnerExceptions)
            {
                CreateMessage(strings, e);
            }
        }
        else
        {
            strings.AppendLine(exception.Message);
            if (exception.InnerException != null)
            {
                CreateMessage(strings, exception.InnerException);
            }
        }
    }

    private static string DefaultMessage(PostgresException exception)
    {
        // TODO: Из PostgresException удалено свойство NpgsqlStatement? Statement. Чем заменить?
        return string.Format("{0}\nSQL: {1}", exception.MessageText, exception.Detail);
    }
}
