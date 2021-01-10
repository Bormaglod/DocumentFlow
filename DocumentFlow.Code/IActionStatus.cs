//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.01.2021
// Time: 00:54
//-----------------------------------------------------------------------

namespace DocumentFlow.Code
{
    /// <summary>
    /// Интерфейс служит для определения действий возникающих после изменения статуса документа.
    /// </summary>
    public interface IActionStatus
    {
        /// <summary>
        /// Метод вызывается после изменения статуса документа и обновления всех данных на странице.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="database"></param>
        /// <param name="info"></param>
        void StatusValueChanged(IValueEditor editor, IDatabase database, IInformation info);
    }
}
