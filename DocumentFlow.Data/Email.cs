//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.12.2019
//-----------------------------------------------------------------------

namespace DocumentFlow.Data;

public class Email : Identifier<long>
{
    public string? Address { get; set; }
    public string? MailHost { get; set; }
    public short MailPort { get; set; }
    public string? UserPassword { get; set; }
    public string? SignaturePlain { get; set; }
    public string? SignatureHtml { get; set; }
}
