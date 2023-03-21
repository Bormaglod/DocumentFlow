//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.12.2019
//
// Версия 2023.3.17
//  - перенесено из DocumentFlow.Data.Core в DocumentFlow.Data
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Data;

public class EmailLog : Identifier<long>
{
    public long EmailId { get; set; }
    public DateTime DateTimeSending { get; set; }
    public string ToAddress { get; set; } = string.Empty;
    public Guid? DocumentId { get; set; }
}
