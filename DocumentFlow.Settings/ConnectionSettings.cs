//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.06.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Settings;

public class ConnectionSettings
{
    public string Name { get; set; } = string.Empty;
    public string Server { get; set; } = "localhost";
    public int Port { get; set; } = 5432;
    public string Database { get; set; } = "erp_autokom";
    
    public override string ToString()
    {
        return $"Server={Server};Port={Port};Database={Database};";
    }
}
