//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.07.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Controls.Events;

public class DependentEntitySelectEventArgs : EventArgs
{
    public DependentEntitySelectEventArgs() { }

    public DependentEntitySelectEventArgs(IDependentEntity dependentEntity) => DependentEntity = dependentEntity;

    public IDependentEntity? DependentEntity { get; set; }

    public bool Accept { get; set; } = true;
}