//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.03.2019
// Time: 23:13
//-----------------------------------------------------------------------

namespace DocumentFlow.Core
{
    using System;
    using System.Windows.Forms;
    
    public static class ExceptionHelper
    {
        public static void MesssageBox(Exception exception)
        {
            MessageBox.Show(Message(exception), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static string Message(Exception exception)
        {
            return exception.InnerException == null ? exception.Message : exception.InnerException.Message;
        }
    }
}
