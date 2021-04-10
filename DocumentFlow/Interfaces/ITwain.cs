//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.03.2020
// Time: 13:00
//-----------------------------------------------------------------------

using System;
using DocumentFlow.Core;
//using System.Drawing;

namespace DocumentFlow.Interfaces
{
    public interface ITwain
    {
        event EventHandler<CupturingImageEventArgs> CapturingImage;

        void Capture(Guid destinationId);
    }
}
