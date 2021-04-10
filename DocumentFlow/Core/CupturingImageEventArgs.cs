//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.03.2020
// Time: 20:42
//-----------------------------------------------------------------------

using System;
using System.Drawing;

namespace DocumentFlow.Core
{
    public class CupturingImageEventArgs : EventArgs
    {
        public CupturingImageEventArgs(Guid destinationId, Image image) => (DestinationId, Image) = (destinationId, image);

        public Guid DestinationId { get; }
        public Image Image { get; }
    }
}
