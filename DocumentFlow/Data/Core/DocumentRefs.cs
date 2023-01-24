//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.08.2019
//
// Версия 2023.1.24
//  - добавлено наследование от IDocumentRefs
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data.Core;

public class DocumentRefs : Entity<long>, IDocumentRefs
{

    [Display(Name = "Имя файла", Order = 2)]
    public string? file_name { get; set; }

    [Display(Name = "Описание", Order = 1)]
    public string? note { get; set; }

    [Display(Name = "Размер", Order = 3)]
    public long file_length { get; set; }

    [Display(AutoGenerateField = false)]
    public string? thumbnail { get; set; }

    [Display(Name = "Галлерея", Order = 4)]
    public bool thumbnail_exist => !string.IsNullOrEmpty(thumbnail);

    [Display(AutoGenerateField = false)]
    public byte[]? file_content { get; set; }
}
