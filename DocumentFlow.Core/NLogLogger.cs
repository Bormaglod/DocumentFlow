//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
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

        internal NLogLogger(string name)
        {
            log = LogManager.GetLogger(name);
        }

        public override bool IsEnabled(NpgsqlLogLevel level)
        {
            return log.IsEnabled(ToNLogLogLevel(level));
        }

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
            switch (level)
            {
                case NpgsqlLogLevel.Trace:
                    return LogLevel.Trace;
                case NpgsqlLogLevel.Debug:
                    return LogLevel.Debug;
                case NpgsqlLogLevel.Info:
                    return LogLevel.Info;
                case NpgsqlLogLevel.Warn:
                    return LogLevel.Warn;
                case NpgsqlLogLevel.Error:
                    return LogLevel.Error;
                case NpgsqlLogLevel.Fatal:
                    return LogLevel.Fatal;
                default:
                    throw new ArgumentOutOfRangeException("level");
            }
        }
    }
}
