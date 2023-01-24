//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//
// Версия 2023.1.24
//  - перенесено из DocumentFlow.Data в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using System.Data;

namespace DocumentFlow.Infrastructure.Data
{
    public interface IDatabase
    {
        string CurrentUser { get; }
        IDbConnection OpenConnection();
        IDbConnection OpenConnection(string userName, string password);
        void Login(string userName, string password);
    }
}
