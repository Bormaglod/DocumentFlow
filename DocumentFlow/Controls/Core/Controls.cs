//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Core.Reflection;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

using System.Collections;
using System.Linq.Expressions;

namespace DocumentFlow.Controls.Core;

public class Controls<T> : IControls<T>
    where T : class, IDocumentInfo, new()
{
    private int tabIndex = 0;

    public IList? Container { get; set; }
    public IEditorPage? EditorPage { get; set; }

    public C GetControl<C>(string name)
        where C : IControl
    {
        if (Container != null)
        {
            var control = Container.OfType<C>().FirstOrDefault(x => x.PropertyName == name);
            if (control != null)
            {
                return control;
            }

            foreach (var controls in Container.OfType<IControls>())
            {
                control = controls.GetControl<C>(name);
                if (control != null)
                {
                    return control;
                }
            }
        }

        throw new Exception($"Элемент упроавления {name} не найден.");
    }

    public IEnumerable<C> GetControls<C>()
        where C : IControl
    {
        List<C> list = new();
        if (Container != null)
        {
            list.AddRange(Container.OfType<C>());

            foreach (var controls in Container.OfType<IControls>())
            {
                list.AddRange(controls.GetControls<C>());
            }
        }

        return list;
    }

    /*ICheckBoxControl IControls<T>.CreateCheckBox(Expression<Func<T, object?>> memberExpression, string header)
    {
        return new DfCheckBox(memberExpression.ToMember().Name, header);
    }
*/

    public IControls<T> CreateTextBox(Expression<Func<T, object?>> memberExpression, string header, Action<ITextBoxControl>? props = null)
    {
        var textBox = new DfTextBox(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(textBox);
        AddControl(textBox);

        return this;
    }

    public IControls<T> CreateMaskedTextBox<P>(Expression<Func<T, object?>> memberExpression, string header, Action<IMaskedTextBoxControl<P>>? props = null)
        where P : struct, IComparable<P>
    {
        var textBox = new DfMaskedTextBox<P>(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(textBox);
        AddControl(textBox);

        return this;
    }

    public IControls<T> CreateComboBox<P>(Expression<Func<T, object?>> memberExpression, string header, Action<IComboBoxControl<P>>? props = null)
        where P : class, IDocumentInfo
    {
        var combo = new DfComboBox<P>(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(combo);
        AddControl(combo);

        return this;
    }

    public IControls<T> CreateDirectorySelectBox<P>(Expression<Func<T, object?>> memberExpression, string header, Action<IDirectorySelectBoxControl<P>>? props = null)
        where P : class, IDirectory
    {
        var box = new DfDirectorySelectBox<P>(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(box);
        AddControl(box);

        return this;
    }

    public IControls<T> CreateChoice<P>(Expression<Func<T, object?>> memberExpression, string header, Action<IChoiceControl<P>>? props = null)
        where P : struct, IComparable
    {
        var choice = new DfChoice<P>(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(choice);
        AddControl(choice);

        return this;
    }

    public IControls<T> CreateNumericTextBox(Expression<Func<T, object?>> memberExpression, string header, Action<INumericTextBoxControl>? props = null)
    {
        var text = new DfNumericTextBox(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(text);
        AddControl(text);

        return this;
    }

    public IControls<T> CreatePercentTextBox(Expression<Func<T, object?>> memberExpression, string header, Action<IPercentTextBoxControl>? props = null)
    {
        var text = new DfPercentTextBox(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(text);
        AddControl(text);

        return this;
    }

    public IControls<T> CreateCurrencyTextBox(Expression<Func<T, object?>> memberExpression, string header, Action<ICurrencyTextBoxControl>? props = null)
    {
        var text = new DfCurrencyTextBox(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(text);
        AddControl(text);

        return this;
    }

    private void AddControl(IControl control)
    {
        tabIndex += 10;

        Container?.Add(control);

        if (control is BaseControl c)
        {
            c.BringToFront();
            c.EditorPage = EditorPage;
        }
    }
}
