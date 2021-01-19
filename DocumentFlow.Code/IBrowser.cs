//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.09.2020
// Time: 13:13
//-----------------------------------------------------------------------

using System;
using DocumentFlow.Code.Core;
using DocumentFlow.Core;

namespace DocumentFlow.Code
{
    public interface IBrowser
    {
        event EventHandler<ChangeParentEventArgs> ChangeParent;

        BrowserMode Mode { get; }

        /// <summary>
        /// Возвращает объект содержащий данные выделенной строки.
        /// </summary>
        object CurrentRow { get; }

        /// <summary>
        /// Возвращает ссылку на интерфейс определяющий вид строки кнопок.
        /// </summary>
        IToolBar ToolBar { get; }

        IContextMenu ContextMenuRecord { get; }

        IContextMenu ContextMenuRow { get; }

        IContextMenu ContextMenuGrid { get; }

        /// <summary>
        /// Возвращает ссылку на нтерфейс содержащий список доступных команд и определяющий работу с ними.
        /// </summary>
        IUserActionCollection Commands { get; }

        /// <summary>
        /// Возвращает или задает значение, указывающее, может ли пользователь перетаскивать столбец в область группировки.
        /// </summary>
        /// <value>true, если пользователь может перетащить столбец в область группировки; в противном случае false.
        /// Значение по умолчанию - false.</value>
        bool AllowGrouping { get; set; }

        /// <summary>
        /// Возвращает или задает значение, указывающее, может ли пользователь сортировать данные, щелкнув по его ячейке заголовка столбца.
        /// </summary>
        /// <value>true, если сортировка включена ; в противном случае - false. Значение по умолчанию - true.</value>
        bool AllowSorting { get; set; }

        /// <summary>
        /// Возвращает или задает значение которое определяет тип данных окна <see cref="DataType"/>. Для успешного создания окна необходимо обязательно указать
        /// одно из значений: Directory, Document или Report.
        /// Значение по умолчанию - None. 
        /// </summary>
        DataType DataType { get; set; }
        DateRanges FromDate { get; set; }
        DateRanges ToDate { get; set; }
        bool CommandBarVisible { get; set; }
        IBrowserParameters Parameters { get; }
        IColumnCollection Columns { get; }

        void CreateColumns(Action<IColumnCollection> createColumnsAction);

        /// <summary>
        /// Создает обработчик позволяющий в RowHeader отображать изображения состояний документов.
        /// </summary>
        void CreateStatusColumnRenderer();
        IGroupColumnCollection CreateGroups();
        void DefineDoubleClickCommand(string name);
        void MoveToEnd();
        T ExecuteSqlCommand<T>(string sql, object param = null);
    }
}
