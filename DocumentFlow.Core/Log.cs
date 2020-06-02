//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.11.2014
// Time: 12:19
//-----------------------------------------------------------------------

namespace DocumentFlow.Core
{
    using NLog;

    public class LogHelper
    {
        static public Logger Logger { get; } = LogManager.GetCurrentClassLogger();
    }
}
