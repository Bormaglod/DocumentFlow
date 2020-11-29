//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.03.2020
// Time: 23:08
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Core.Exceptions
{
    public class FtpUploadException : Exception
    {
        public FtpUploadException(string message) : base(message) { }
    }
}
