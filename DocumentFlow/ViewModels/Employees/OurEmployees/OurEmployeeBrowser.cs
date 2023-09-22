//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.ViewModels;

public class OurEmployeeBrowser : BaseEmployeeBrowser<OurEmployee>, IOurEmployeeBrowser
{
    public OurEmployeeBrowser(IServiceProvider services, IPageManager pageManager, IOurEmployeeRepository repository, IConfiguration configuration) 
        : base(services, pageManager, repository, configuration) 
    {
        AllowGrouping();
    }

    protected override GridColumn[] GetColumnsAfter(string mappingName)
    {
        if (mappingName == "PostName")
        {
            return new GridColumn[] { CreateText(x => x.OwnerName, "Организация", width: 200) };
        }

        return base.GetColumnsAfter(mappingName);
    }
}
