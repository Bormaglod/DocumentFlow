//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.12.2019
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core;

public class Email : Identifier<long>
{
    public string? address { get; set; }
    public string? mail_host { get; set; }
    public short mail_port { get; set; }
    public string? user_password { get; set; }
    public string? signature_plain { get; set; }
    public string? signature_html { get; set; }
}
