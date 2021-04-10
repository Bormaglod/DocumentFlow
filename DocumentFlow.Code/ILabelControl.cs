//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.12.2019
// Time: 17:49
//-----------------------------------------------------------------------

using System.Drawing;

namespace DocumentFlow.Code
{
    /// <summary>
    /// Интерфейс определяюший свойства элемента управления имеющего подпись.
    /// </summary>
    public interface ILabelControl
    {
        /// <summary>
        /// Возвращает или устанавливает текст подписи.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Возвращает или устанавливает ширину подписи элемента управления.
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// Возвращает или устанавливает флаг который определяет способ определения ширины подписи элемента управления.
        /// </summary>
        /// <value>true, если ширина подписи элемента управления расчитывается автоматически и false, если ширина определяется с помощью <see cref="LabelWidth"/></value>
        bool AutoSize { get; set; }

        /// <summary>
        /// Возвращает или устанавливает способ выравнивания подписи элемента управления.
        /// </summary>
        ContentAlignment TextAlign { get; set; }

        /// <summary>
        /// Возвращает или устанавливает флаг видимости подписи.
        /// </summary>
        bool Visible { get; set; }
    }
}
