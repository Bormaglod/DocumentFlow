//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.10.2021
//
// Версия 2023.5.20
//  - добавлен тип MaterialKind, свойство MaterialKind и мктоды для
//    работы с ними
//  - удалено поле WireGroup
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

using Humanizer;

namespace DocumentFlow.Entities.Products;

public enum MaterialKind { Undefined, Wire, Terminal, Housing, Seal }

[Description("Материал")]
public class Material : Product
{
    public static readonly Dictionary<MaterialKind, string> kinds = new()
    {
        [Products.MaterialKind.Undefined] = "Не определён",
        [Products.MaterialKind.Wire] = "Провод",
        [Products.MaterialKind.Terminal] = "Контакт",
        [Products.MaterialKind.Housing] = "Колодка",
        [Products.MaterialKind.Seal] = "Уплотнитель"
    };

    public string? CrossName { get; protected set; }
    public decimal MinOrder { get; set; }
    public string? ExtArticle { get; set; }
    public bool MaterialUsing { get; protected set; }
    public int PriceStatus { get; protected set; }
    public Guid? WireId { get; set; }
    public string? WireName { get; protected set; }
    
    [EnumType("material_kind")]
    public string MaterialKind { get; set; } = "undefined";

    public MaterialKind Kind
    {
        get { return Enum.Parse<MaterialKind>(MaterialKind.Pascalize()); }
        protected set { MaterialKind = value.ToString().Underscore(); }
    }

    public string MaterialKindName => Kinds[Kind];

    public static IReadOnlyDictionary<MaterialKind, string> Kinds => kinds;
}
