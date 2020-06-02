//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.06.2019
// Time: 11:28
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.Windows.Forms;
    using Flee.PublicTypes;
    using NHibernate;

    public interface IEditorControl
    {
        string Name { get; }
        Control Control { get; }
        bool Enabled { get; set; }
        bool Visible { get; set; }

        /// <summary>
        /// Создает элемент управления.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="container"></param>
        /// <param name="context">Контекст для вычисления выражений содержащий список переменных 
        /// совпадающих с полями текущей записи и соответствующими значениями на момент создания 
        /// элемента управления.</param>
        void CreateControl(ISession session, Control container, ExpressionContext context);
    }
}
