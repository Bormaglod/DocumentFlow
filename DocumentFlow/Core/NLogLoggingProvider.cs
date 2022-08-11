//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.09.2020
//-----------------------------------------------------------------------

using Npgsql.Logging;

namespace DocumentFlow.Core;

public class NLogLoggingProvider : INpgsqlLoggingProvider
{
    public NpgsqlLogger CreateLogger(string name) => new NLogLogger(name);
}
