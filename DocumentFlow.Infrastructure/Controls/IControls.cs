//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//
// Версия 2023.5.5
//  - добавлен метод SetHeaderWidth
//  - добавлен метод SetEditorWidth
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls.Core;
using DocumentFlow.Infrastructure.Data;

using System.Collections;
using System.Linq.Expressions;

namespace DocumentFlow.Infrastructure.Controls;

public interface IControls
{
    IList? Container { get; set; }
    IEditorPage? EditorPage { get; set; }
    C GetControl<C>()
        where C : IControl;
    C GetControl<C>(string name)
        where C : IControl;
    IEnumerable<C> GetControls<C>();
    void Initialize(object value);
}

public interface IControls<T> : IControls
    where T : class, new()
{
    C GetControl<C>(Expression<Func<T, object?>> memberExpression)
        where C : IControl;
    T Get();
    IContainer<T> GetContainer(string name);

    IDirectorySelectBoxControl<P> CreateDirectorySelectBox<P>(Expression<Func<T, Guid?>> memberExpression, string header, Action<IDirectorySelectBoxControl<P>>? props = null)
        where P : class, IDirectory;
    INumericTextBoxControl CreateNumericTextBox(Expression<Func<T, decimal?>> memberExpression, string header, Action<INumericTextBoxControl>? props = null);
    ICurrencyTextBoxControl CreateCurrencyTextBox(Expression<Func<T, decimal?>> memberExpression, string header, Action<ICurrencyTextBoxControl>? props = null);
    IChoiceControl<P> CreateChoice<P>(Expression<Func<T, P?>> memberExpression, string header, Action<IChoiceControl<P>>? props = null)
        where P : struct, IComparable;

    IControl GetControl(Expression<Func<T, object?>> memberExpression);
    IControls<T> SetHeaderWidth(int width);
    IControls<T> SetEditorWidth(int width);
    IControls<T> Select(Expression<Func<T, object?>> memberExpression);
    IControls<T> If(bool condition, Action<IControls<T>> trueAction, Action<IControls<T>>? falseAction = null);
    IControls<T> AddCheckBox(Expression<Func<T, object?>> memberExpression, string header, Action<ICheckBoxControl>? props = null);
    IControls<T> AddToggleButton(Expression<Func<T, object?>> memberExpression, string header, Action<IToggleButtonControl>? props = null);
    IControls<T> AddTextBox(Expression<Func<T, object?>> memberExpression, string header, Action<ITextBoxControl>? props = null);
    IControls<T> AddDateTimePicker(Expression<Func<T, object?>> memberExpression, string header, Action<IDateTimePickerControl>? props = null);
    IControls<T> AddMaskedTextBox<P>(Expression<Func<T, P?>> memberExpression, string header, Action<IMaskedTextBoxControl<P>>? props = null)
        where P : struct, IComparable<P>;
    IControls<T> AddComboBox<P>(Expression<Func<T, Guid?>> memberExpression, string header, Action<IComboBoxControl<P>>? props = null)
        where P : class, IDocumentInfo;
    IControls<T> AddDirectorySelectBox<P>(Expression<Func<T, Guid?>> memberExpression, string header, Action<IDirectorySelectBoxControl<P>>? props = null)
        where P : class, IDirectory;
    IControls<T> AddDocumentSelectBox<P>(Expression<Func<T, Guid?>> memberExpression, string header, Action<IDocumentSelectBoxControl<P>>? props = null)
        where P : class, IAccountingDocument;
    IControls<T> AddChoice<P>(Expression<Func<T, P?>> memberExpression, string header, Action<IChoiceControl<P>>? props = null)
        where P : struct, IComparable;
    IControls<T> AddNumericTextBox(Expression<Func<T, decimal?>> memberExpression, string header, Action<INumericTextBoxControl>? props = null);
    IControls<T> AddIntergerTextBox<P>(Expression<Func<T, P?>> memberExpression, string header, Action<IIntegerTextBoxControl<P>>? props = null)
        where P : struct, IComparable<P>;
    IControls<T> AddPercentTextBox(Expression<Func<T, decimal?>> memberExpression, string header, Action<IPercentTextBoxControl>? props = null);
    IControls<T> AddCurrencyTextBox(Expression<Func<T, decimal?>> memberExpression, string header, Action<ICurrencyTextBoxControl>? props = null);
    IControls<T> AddState(Expression<Func<T, CalculationState>> memberExpression, string header, Action<IStateControl>? props = null);
    IControls<T> AddMultiSelectionComboBox(Expression<Func<T, IEnumerable<string>?>> memberExpression, string header, Action<IMultiSelectionComboBoxControl>? props = null);
    IControls<T> AddOperationProperty(string header, Action<IOperationPropertyControl>? props = null);
    IControls<T> AddDataGrid<P>(Action<IDataGridControl<P>>? props = null)
        where P : IEntity<long>, IEntityClonable, ICloneable, new();
    IControls<T> AddPanel(Action<IContainer<T>>? props = null);
    IControls<T> AddLine();
    IControls<T> AddWireStripping(Expression<Func<T, Stripping>> memberExpression, string header, Action<IWireStrippingControl>? props = null);
    IControls<T> AddProductionLot(Action<IProductionLotControl>? props = null);
    IControls<T> AddLabel(string header, Action<ILabelControl>? props = null);
}
