//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Employees;

public class OrgEmployeeEditor : BaseEmployeeEditor<OurEmployee>, IOrgEmployeeEditor
{
    public OrgEmployeeEditor(IOurEmployeeRepository repository, IPageManager pageManager) : base(repository, pageManager) { }
}