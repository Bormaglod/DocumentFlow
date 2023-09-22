//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.09.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Tools;

[AttributeUsage(AttributeTargets.Class)]
public class EntityAttribute : Attribute
{
    public EntityAttribute(Type entityType, Type? repositoryType = null)
    {
        EntityType = entityType;
        RepositoryType = repositoryType;
    }

    public Type EntityType { get; set; }

    public Type? RepositoryType { get; set; }
}
