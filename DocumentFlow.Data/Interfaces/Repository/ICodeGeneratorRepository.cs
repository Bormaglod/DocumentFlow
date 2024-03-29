﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.10.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Interfaces.Repository;

public interface ICodeGeneratorRepository
{
    IReadOnlyList<CodeGenerator> GetTypes();
    IReadOnlyList<CodeGenerator> GetBrands();
    IReadOnlyList<CodeGenerator> GetModels(CodeGenerator brand);
    IReadOnlyList<CodeGenerator> GetEngines();
}
