//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//-----------------------------------------------------------------------

using System.Data;

namespace DocumentFlow.Data
{
    public interface IDatabase
    {
        string CurrentUser { get; }
        IDbConnection OpenConnection();
        IDbConnection OpenConnection(string userName, string password);
        void Login(string userName, string password);
    }
}
