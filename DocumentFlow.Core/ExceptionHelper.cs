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

    using Npgsql;
    
    public static class ExceptionHelper
    {
        public static void MesssageBox(Exception exception)
        {
            MessageBox.Show(Message(exception), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static string Message(Exception exception)
        {
            if (exception.InnerException is PostgresException pgException)
            {
                if (pgException.SqlState == "P0001")
                {
                    return pgException.MessageText;
                }
                else
                {
                    return string.Format("{0}\nSQL: {1}", pgException.MessageText, pgException.Statement.SQL);
                }
            }
            else
                return exception.Message;
        }
    }
}
