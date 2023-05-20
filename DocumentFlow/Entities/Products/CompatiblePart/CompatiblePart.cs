//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.05.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Entities.Products;

public class CompatiblePart : Entity<long>, IReferenceItem, ICloneable, IEntityClonable
{
    [Display(AutoGenerateField = false)]
    public Guid CompatibleId { get; set; }

    [Display(AutoGenerateField = false)]
    public Guid ReferenceId => CompatibleId;

    [Display(Name = "Артикул")]
    [ColumnMode(Width = 150)]
    public string Code { get; protected set; } = string.Empty;

    [Display(Name = "Наименование")]
    [ColumnMode(AutoSizeColumnsMode = AutoSizeColumnsMode.Fill)]
    public string Name { get; protected set; } = string.Empty;

    public object Clone() => MemberwiseClone();

    public object Copy()
    {
        var copy = Clone();
        ((CompatiblePart)copy).Id = 0;

        return copy;
    }

    public void SetData(IDirectory directory)
    {
        CompatibleId = directory.Id;
        Code = directory.Code;
        Name = directory.ItemName ?? string.Empty;
    }

    public void Clear() => (Code, Name) = (string.Empty, string.Empty);
}
