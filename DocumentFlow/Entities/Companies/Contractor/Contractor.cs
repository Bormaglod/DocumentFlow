//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.09.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Companies;

[Description("Контрагент")]
public class Contractor : Company
{
    public static readonly Guid CompanyGroup = new("{AEE39994-7BFE-46C0-828B-AC6296103CD1}");
    public static readonly Guid PersonGroup = new("{A9799032-2C6A-46DA-AB8A-CF6423E3BEB6}");
}
