//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.08.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Enums;

public enum SummaryColumnFormat 
{ 
    None, 
    Currency 
}

[Flags]
public enum SelectOptions
{
    None = 0,
    IncludeDeleted = 1,
    IncludeNotAccepted = 2,
    All = 3
}

public enum ButtonIconSize 
{ 
    Small, 
    Large 
}

public enum ConstructDataMethod
{
    Create,
    Load,
    Save,
    Reload
}