//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.09.2020
// Time: 10:59
//-----------------------------------------------------------------------

using Npgsql.Logging;

namespace DocumentFlow.Core
{
    public class NLogLoggingProvider : INpgsqlLoggingProvider
    {
        public NpgsqlLogger CreateLogger(string name) => new NLogLogger(name);
    }
}
