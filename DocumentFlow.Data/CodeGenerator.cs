//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.10.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Data;

public class CodeGenerator : Identifier<int>
{
    public short CodeId { get; set; }
    public string CodeName { get; set; } = string.Empty;

    public override string ToString() => CodeName;
}
