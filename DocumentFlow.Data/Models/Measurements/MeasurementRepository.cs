//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Repository;
using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Data.Models;

public class MeasurementRepository : Repository<Guid, Measurement>, IMeasurementRepository
{
    public MeasurementRepository(IDatabase database) : base(database) {  }
}
