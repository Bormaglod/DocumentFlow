//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.03.2020
// Time: 23:08
//-----------------------------------------------------------------------

namespace DocumentFlow.Core
{
    using System;

    public class FtpDownloadException : Exception
    {
        public FtpDownloadException(string message) : base(message) { }
    }
}
