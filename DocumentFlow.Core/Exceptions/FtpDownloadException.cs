//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.09.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Core.Exceptions;

public class FtpDownloadException : Exception
{
    public FtpDownloadException(string message) : base(message) { }
}
