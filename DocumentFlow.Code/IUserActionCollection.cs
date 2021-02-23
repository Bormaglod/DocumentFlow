//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.10.2020
// Time: 21:03
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using DocumentFlow.Code.Core;

namespace DocumentFlow.Code
{
    public interface IUserActionCollection : IEnumerable, IEnumerable<IUserAction>
    {
        /// <summary>
        /// Функция возвращает пользовательскую команду, добавленную с помощью функции Add по имени name.
        /// Если для команды указан псевдоним (alias), то при совпадении его с параметром name, будет возвращен именно он.
        /// Что бы отключить возможность сравнения name с псевдонимом команды, параметр withoutAlias необходимо установить в true.
        /// Однако, если в наборе команд окажутся несколько команд с одинаковым именем, то будет возвращена любая из из них.
        /// </summary>
        /// <param name="name">Наименование искомой команды.</param>
        /// <param name="withoutAlias">true, если необходимо осуществлять поиск без учёта псевдонимов команд и false - иначе.</param>
        /// <returns></returns>
        IUserAction Get(string name, bool withoutAlias = false);

        /// <summary>
        /// Функция возвращает добавляемую команду в набор пользовательских команд.
        /// </summary>
        /// <param name="method">Метод запуска команды определяемый с помощью перечисления <see cref="CommandMethod"/></param>
        /// <param name="name">Наименование команды.</param>
        /// <param name="skipNotFound">true - игнорировать добавление команды, false - при отсутствии команды вызывается ошибка <see cref="DocumentFlow.Core.Exceptions.CommandNotFoundException"/>.</param>
        /// <returns></returns>
        IUserAction Add(CommandMethod method, string name, bool skipNotFound = false);

        /// <summary>
        /// Функция возвращает добавляемую команду в набор пользовательских команд.
        /// </summary>
        /// <param name="method">Метод запуска команды определяемый с помощью перечисления <see cref="CommandMethod"/></param>
        /// <param name="name">Наименование команды.</param>
        /// <param name="alias">Псевдоним командыб который используется при поиске команды с помощью функции <see cref="IUserActionCollection.Get(string, bool)"/>.</param>
        /// <param name="skipNotFound">true - игнорировать добавление команды, false - при отсутствии команды вызывается ошибка <see cref="DocumentFlow.Core.Exceptions.CommandNotFoundException"/>.</param>
        /// <returns></returns>
        IUserAction Add(CommandMethod method, string name, string alias, bool skipNotFound = false);

        void OpenDocument(Guid id);

        void OpenDiagram(Guid id);
    }
}
