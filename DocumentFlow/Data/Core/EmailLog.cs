//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.12.2019
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core;

public class EmailLog : Identifier<long>
{
    public long email_id { get; set; }
    public DateTime date_time_sending { get; set; }
    public string to_address { get; set; } = string.Empty;
    public Guid? document_id { get; set; }
}
