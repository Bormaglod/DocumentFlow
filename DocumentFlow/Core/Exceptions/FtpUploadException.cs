//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.03.2020
//-----------------------------------------------------------------------

namespace DocumentFlow.Core.Exceptions;

public class FtpUploadException : Exception
{
    public FtpUploadException(string message) : base(message) { }
}
