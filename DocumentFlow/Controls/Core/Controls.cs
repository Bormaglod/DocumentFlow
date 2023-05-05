//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//
// Версия 2023.5.5
//  - добавлен метод SetHeaderWidth
//  - добавлен метод SetEditorWidth
//  - в методе AddControl реализована установка значения HeaderWidth и 
//    EditorWidth
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Core.Reflection;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Controls.Core;
using DocumentFlow.Infrastructure.Data;

using System.Collections;
using System.Linq.Expressions;

namespace DocumentFlow.Controls.Core;

public class Controls<T> : IControls<T>
    where T : class, new()
{
    private int tabIndex = 0;
    private int? headerWidth;
    private int? editorWidth;

    public IList? Container { get; set; }
    public IEditorPage? EditorPage { get; set; }

    public C GetControl<C>()
        where C : IControl
    {
        var control = GetControls<C>().FirstOrDefault();
        return control != null ? control : throw new Exception($"Элемент управления {typeof(C).Name} не найден.");
    }

    public C GetControl<C>(string name)
        where C : IControl
    {
        var control = GetControls<C>().FirstOrDefault(x => x.PropertyName == name);
        return control != null ? control : throw new Exception($"Элемент управления {name} не найден.");
    }

    public IEnumerable<C> GetControls<C>()
    {
        List<C> list = new();
        if (Container != null)
        {
            list.AddRange(Container.OfType<C>());

            foreach (var containers in Container.OfType<IContainer<T>>())
            {
                list.AddRange(containers.Controls.GetControls<C>());
            }
        }

        return list;
    }

    public C GetControl<C>(Expression<Func<T, object?>> memberExpression)
        where C : IControl
    {
        return GetControl<C>(memberExpression.ToMember().Name);
    }

    public IControl GetControl(Expression<Func<T, object?>> memberExpression)
    {
        return GetControl<IControl>(memberExpression.ToMember().Name);
    }

    public IContainer<T> GetContainer(string name)
    {
        if (Container != null)
        {
            var containers = GetContainers(Container);
            var container = containers.FirstOrDefault(x => x.Name == name);
            if (container != null)
            {
                return container;
            }
        }

        throw new Exception($"Container {name} not found.");
    }

    private IEnumerable<IContainer<T>> GetContainers(IList list)
    {
        var containers = list.OfType<IContainer<T>>();
        foreach (var container in containers) 
        {
            if (container.Controls.Container != null)
            {
                containers = containers.Union(GetContainers(container.Controls.Container));
            }
        }

        return containers;
    }

    public void Initialize(object value)
    {
        var controls = GetControls<IBindingControl>();
        foreach (var item in value.GetType().GetProperties())
        {
            var control = controls.FirstOrDefault(x => x.PropertyName == item.Name);
            if (control == null)
            {
                continue;
            }

            control.Value = item.GetValue(value);
        }
    }

    public T Get()
    {
        var controls = GetControls<IBindingControl>();
        T value = new();
        foreach (var item in value.GetType().GetProperties())
        {
            var control = controls.FirstOrDefault(x => x.PropertyName == item.Name);
            if (control == null)
            {
                continue;
            }

            item.SetValue(value, control.Value);
        }

        return value;
    }

    public IDirectorySelectBoxControl<P> CreateDirectorySelectBox<P>(Expression<Func<T, Guid?>> memberExpression, string header, Action<IDirectorySelectBoxControl<P>>? props = null)
        where P : class, IDirectory
    {
        var control = new DfDirectorySelectBox<P>(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(control);
        AddControl(control);

        return control;
    }

    public INumericTextBoxControl CreateNumericTextBox(Expression<Func<T, decimal?>> memberExpression, string header, Action<INumericTextBoxControl>? props = null)
    {
        var text = new DfNumericTextBox(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(text);
        AddControl(text);

        return text;
    }

    public ICurrencyTextBoxControl CreateCurrencyTextBox(Expression<Func<T, decimal?>> memberExpression, string header, Action<ICurrencyTextBoxControl>? props = null)
    {
        var text = new DfCurrencyTextBox(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(text);
        AddControl(text);

        return text;
    }

    public IChoiceControl<P> CreateChoice<P>(Expression<Func<T, P?>> memberExpression, string header, Action<IChoiceControl<P>>? props = null)
        where P : struct, IComparable
    {
        var choice = new DfChoice<P>(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(choice);
        AddControl(choice);

        return choice;
    }

    public IControls<T> SetHeaderWidth(int width)
    {
        headerWidth = width;
        return this;
    }

    public IControls<T> SetEditorWidth(int width)
    {
        editorWidth = width;
        return this;
    }

    public IControls<T> Select(Expression<Func<T, object?>> memberExpression)
    {
        var control = GetControls<IControl>().FirstOrDefault(x => x.PropertyName == memberExpression.ToMember().Name);
        if (control != null && control is Control c)
        {
            c.Select();
        }

        return this;
    }

    public IControls<T> If(bool condition, Action<IControls<T>> trueAction, Action<IControls<T>>? falseAction = null)
    {
        if (condition)
        {
            trueAction(this);
        }
        else
        {
            falseAction?.Invoke(this);
        }

        return this;
    }

    public IControls<T> AddCheckBox(Expression<Func<T, object?>> memberExpression, string header, Action<ICheckBoxControl>? props = null)
    {
        var check = new DfCheckBox(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(check);
        AddControl(check);

        return this;
    }

    public IControls<T> AddToggleButton(Expression<Func<T, object?>> memberExpression, string header, Action<IToggleButtonControl>? props = null)
    {
        var toggle = new DfToggleButton(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(toggle);
        AddControl(toggle);

        return this;
    }

    public IControls<T> AddTextBox(Expression<Func<T, object?>> memberExpression, string header, Action<ITextBoxControl>? props = null)
    {
        var textBox = new DfTextBox(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(textBox);
        AddControl(textBox);

        return this;
    }

    public IControls<T> AddDateTimePicker(Expression<Func<T, object?>> memberExpression, string header, Action<IDateTimePickerControl>? props = null)
    {
        var dateTime = new DfDateTimePicker(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(dateTime);
        AddControl(dateTime);

        return this;
    }

    public IControls<T> AddMaskedTextBox<P>(Expression<Func<T, P?>> memberExpression, string header, Action<IMaskedTextBoxControl<P>>? props = null)
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

    public IControls<T> AddComboBox<P>(Expression<Func<T, Guid?>> memberExpression, string header, Action<IComboBoxControl<P>>? props = null)
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

    public IControls<T> AddDirectorySelectBox<P>(Expression<Func<T, Guid?>> memberExpression, string header, Action<IDirectorySelectBoxControl<P>>? props = null)
        where P : class, IDirectory
    {
        CreateDirectorySelectBox<P>(memberExpression, header, props);
        return this;
    }

    public IControls<T> AddDocumentSelectBox<P>(Expression<Func<T, Guid?>> memberExpression, string header, Action<IDocumentSelectBoxControl<P>>? props = null)
        where P : class, IAccountingDocument
    {
        var box = new DfDocumentSelectBox<P>(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(box);
        AddControl(box);

        return this;
    }

    public IControls<T> AddChoice<P>(Expression<Func<T, P?>> memberExpression, string header, Action<IChoiceControl<P>>? props = null)
        where P : struct, IComparable
    {
        CreateChoice(memberExpression, header, props);
        return this;
    }

    public IControls<T> AddNumericTextBox(Expression<Func<T, decimal?>> memberExpression, string header, Action<INumericTextBoxControl>? props = null)
    {
        CreateNumericTextBox(memberExpression, header, props);
        return this;
    }

    public IControls<T> AddIntergerTextBox<P>(Expression<Func<T, P?>> memberExpression, string header, Action<IIntegerTextBoxControl<P>>? props = null)
        where P : struct, IComparable<P>
    {
        var text = new DfIntegerTextBox<P>(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(text);
        AddControl(text);

        return this;
    }

    public IControls<T> AddPercentTextBox(Expression<Func<T, decimal?>> memberExpression, string header, Action<IPercentTextBoxControl>? props = null)
    {
        var text = new DfPercentTextBox(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(text);
        AddControl(text);

        return this;
    }

    public IControls<T> AddCurrencyTextBox(Expression<Func<T, decimal?>> memberExpression, string header, Action<ICurrencyTextBoxControl>? props = null)
    {
        CreateCurrencyTextBox(memberExpression, header, props);
        return this;
    }

    public IControls<T> AddState(Expression<Func<T, CalculationState>> memberExpression, string header, Action<IStateControl>? props = null)
    {
        var state = new DfState(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(state);
        AddControl(state);

        return this;
    }

    public IControls<T> AddMultiSelectionComboBox(Expression<Func<T, IEnumerable<string>?>> memberExpression, string header, Action<IMultiSelectionComboBoxControl>? props = null)
    {
        var combo = new DfMultiSelectionComboBox(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(combo);
        AddControl(combo);

        return this;
    }

    public IControls<T> AddOperationProperty(string header, Action<IOperationPropertyControl>? props = null)
    {
        var prop = new DfOperationProperty(header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(prop);
        AddControl(prop);

        return this;
    }

    public IControls<T> AddDataGrid<P>(Action<IDataGridControl<P>>? props = null)
        where P : IEntity<long>, IEntityClonable, ICloneable, new()
    {
        var grid = new DfDataGrid<P>()
        {
            TabIndex = tabIndex
        };

        props?.Invoke(grid);
        AddControl(grid);

        return this;
    }

    public IControls<T> AddPanel(Action<IContainer<T>>? props = null)
    {
        var panel = new DfPanel<T>()
        {
            TabIndex = tabIndex
        };

        tabIndex += 10;

        props?.Invoke(panel);
        Container?.Add(panel);
        panel.BringToFront();

        return this;
    }

    public IControls<T> AddLine()
    {
        var line = new DfLine();
        Container?.Add(line);
        line.BringToFront();

        return this;
    }

    public IControls<T> AddWireStripping(Expression<Func<T, Stripping>> memberExpression, string header, Action<IWireStrippingControl>? props = null)
    {
        var combo = new DfWireStripping(memberExpression.ToMember().Name, header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(combo);
        AddControl(combo);

        return this;
    }

    public IControls<T> AddProductionLot(Action<IProductionLotControl>? props = null)
    {
        var lot = new DfProductionLot()
        {
            TabIndex = tabIndex
        };

        props?.Invoke(lot);
        AddControl(lot);

        return this;
    }

    public IControls<T> AddLabel(string header, Action<ILabelControl>? props = null)
    {
        var label = new DfLabel(header)
        {
            TabIndex = tabIndex
        };

        props?.Invoke(label);
        AddControl(label);

        return this;
    }

    private IControls<T> AddControl(IControl control)
    {
        tabIndex += 10;

        
        if (!control.HeaderWidth.HasValue && headerWidth.HasValue) 
        {
            control.SetHeaderWidth(headerWidth.Value);
        }

        if (!control.EditorWidth.HasValue)
        {
            if (editorWidth.HasValue)
            {
                control.SetEditorWidth(editorWidth.Value);
            }
            else
            {
                control.SetDefaultEditorWidth();
            }
        }

        Container?.Add(control);

        if (control is BaseControl c)
        {
            c.BringToFront();
            c.EditorPage = EditorPage;
        }

        return this;
    }
}
