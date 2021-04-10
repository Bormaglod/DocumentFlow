//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.09.2020
// Time: 10:43
//-----------------------------------------------------------------------

using System;
using NLog;
using Npgsql.Logging;

namespace DocumentFlow.Core
{
    public class NLogLogger : NpgsqlLogger
    {
        readonly Logger log;

        internal NLogLogger(string name) => log = LogManager.GetLogger(name);

        public override bool IsEnabled(NpgsqlLogLevel level) => log.IsEnabled(ToNLogLogLevel(level));

        public override void Log(NpgsqlLogLevel level, int connectorId, string msg, Exception exception = null)
        {
            var ev = new LogEventInfo(ToNLogLogLevel(level), "", msg);
            if (exception != null)
            {
                ev.Exception = exception;
            }

            if (connectorId != 0)
            {
                ev.Properties["ConnectorId"] = connectorId;
            }

            log.Log(ev);
        }

        static LogLevel ToNLogLogLevel(NpgsqlLogLevel level)
        {
            return level switch
            {
                NpgsqlLogLevel.Trace => LogLevel.Trace,
                NpgsqlLogLevel.Debug => LogLevel.Debug,
                NpgsqlLogLevel.Info => LogLevel.Info,
                NpgsqlLogLevel.Warn => LogLevel.Warn,
                NpgsqlLogLevel.Error => LogLevel.Error,
                NpgsqlLogLevel.Fatal => LogLevel.Fatal,
                _ => throw new ArgumentOutOfRangeException(nameof(level)),
            };
        }
    }
}
