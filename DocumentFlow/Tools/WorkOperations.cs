//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.10.2023
//-----------------------------------------------------------------------

using System.Diagnostics;
using System.IO;

namespace DocumentFlow.Tools;

public class WorkOperations
{
    public static void OpenFolder(string file)
    {
        if (!File.Exists(file))
        {
            return;
        }

        // combine the arguments together
        // it doesn't matter if there is a space after ','
        string argument = "/select, \"" + file + "\"";
        Process.Start("explorer.exe", argument);
    }

    public static void OpenFile(string file) 
    {
        if (!File.Exists(file))
        {
            return;
        }

        Process.Start(new ProcessStartInfo(file) { UseShellExecute = true });
    }
}
