//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

using System.Collections;
using System.Linq.Expressions;

namespace DocumentFlow.Infrastructure.Controls;

public interface IControls
{
    IList? Container { get; set; }
    IEditorPage? EditorPage { get; set; }
    C GetControl<C>(string name)
        where C : IControl;
    IEnumerable<C> GetControls<C>()
        where C : IControl;
}

public interface IControls<T> : IControls
    where T : class, IDocumentInfo, new()
{
    /*ICheckBoxControl CreateCheckBox(Expression<Func<T, object?>> memberExpression, string header);*/
    IControls<T> CreateTextBox(Expression<Func<T, object?>> memberExpression, string header, Action<ITextBoxControl>? props = null);
    IControls<T> CreateMaskedTextBox<P>(Expression<Func<T, object?>> memberExpression, string header, Action<IMaskedTextBoxControl>? props = null)
        where P : struct, IComparable<P>;
    IControls<T> CreateComboBox<P>(Expression<Func<T, object?>> memberExpression, string header, Action<IComboBoxControl<P>>? props = null)
        where P : class, IDocumentInfo;
    IControls<T> CreateDirectorySelectBox<P>(Expression<Func<T, object?>> memberExpression, string header, Action<IDirectorySelectBoxControl<P>>? props = null)
        where P : class, IDirectory;
    IControls<T> CreateChoice<P>(Expression<Func<T, object?>> memberExpression, string header, Action<IChoiceControl<P>>? props = null)
        where P : struct, IComparable;
}
