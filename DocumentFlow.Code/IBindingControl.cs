//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.10.2020
// Time: 18:26
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code
{
    /// <summary>
    /// Интерфейс обеспечивающий управление элементом управления имеющим подпись и связанным с указанным полем таблицы БД.
    /// </summary>
    public interface IBindingControl : IControl
    {
        event EventHandler<ValueChangedEventArgs> ValueChanged;

        /// <summary>
        /// Возвращает или устанавливает подпись элемента управления.
        /// </summary>
        string LabelText { get; set; }

        /// <summary>
        /// Возвращает или устанавливает наименование поля таблицы БД связанного с данным элементом управления.
        /// </summary>
        string FieldName { get; set; }

        /// <summary>
        /// Возвращает или устанавливает флаг который определяет способ определения ширины подписи элемента управления.
        /// </summary>
        /// <value>true, если ширина подписи элемента управления расчитывается автоматически и false, если ширина определяется с помощью <see cref="LabelWidth"/></value>
        bool LabelAutoSize { get; set; }

        /// <summary>
        /// Возвращает или устанавливает способ выравнивания подписи элемента управления.
        /// </summary>
        ContentAlignment LabelTextAlignment { get; set; }

        /// <summary>
        /// Возвращает или устанавливает ширину подписи элемента управления.
        /// </summary>
        int LabelWidth { get; set; }

        /// <summary>
        /// Возвращает или устанавливает ширину элемента управления.
        /// </summary>
        int ControlWidth { get; set; }

        /// <summary>
        /// Возвращает или устанавливает возможность принимать данными значение null.
        /// </summary>
        /// <value>true, если данные могут иметь значение null. Значение по умолчанию - true - для ссылочных типов и false - для остальных.</value>
        bool Nullable { get; set; }

        object Value { get; set; }

        object DefaultValue { get; set; }

        IPopulate AsPopulateControl();

        /// <summary>
        /// Устанавливает флаг который определяет способ определения ширины подписи элемента управления.
        /// </summary>
        /// <param name="autoSize">true, если ширина подписи элемента управления расчитывается автоматически и false, если ширина определяется с помощью <see cref="LabelWidth"/></param>
        /// <returns></returns>
        IBindingControl SetLabelAutoSize(bool autoSize);

        /// <summary>
        /// Устанавливает способ выравнивания подписи элемента управления.
        /// </summary>
        /// <param name="contentAlignment">Способ выравнивания подписи.</param>
        /// <returns></returns>
        IBindingControl SetLabelTextAlignment(ContentAlignment contentAlignment);

        /// <summary>
        /// Устанавливает ширину текста предваряющего элемент управления. Если этот метод не вызывается, то ширина текста будет равна 100.
        /// </summary>
        /// <param name="labelWidth">Ширина текста предваряющего элемент управления.</param>
        /// <returns></returns>
        IBindingControl SetLabelWidth(int labelWidth);

        /// <summary>
        /// Устанавливает ширину элемента управления. Если этот метод не вызывается, то ширина жлемента управления будет равна 100.
        /// </summary>
        /// <param name="controlWidth">Ширина элемента управления.</param>
        /// <returns></returns>
        IBindingControl SetControlWidth(int controlWidth);

        IBindingControl AsNullable();

        IBindingControl AsRequired();

        IBindingControl SetDefaultValue(object defaultValue);

        IBindingControl ValueChangedAction(EventHandler<ValueChangedEventArgs> valueChanged);
    }
}
