//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.12.2019
// Time: 22:33
//-----------------------------------------------------------------------

namespace DocumentFlow.Printing.Core
{
    using System.Windows.Input;

    public class PrintCommands
    {
        static PrintCommands()
        {
            SendEmail = new RoutedCommand("SendEmail", typeof(Preview));
        }

        public static RoutedCommand SendEmail { get; set; }
    }
}
