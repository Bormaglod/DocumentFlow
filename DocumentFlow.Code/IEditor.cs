//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.10.2020
// Time: 23:26
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using DocumentFlow.Code.Core;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Code
{
    public delegate IEnumerable<IIdentifier> ChoiceItems(IDbConnection connection);
    public delegate IEnumerable<IIdentifier> СriterionChoiceItems(IValueEditor editor, IDbConnection connection);
    public delegate IEnumerable<IIdentifier> СriterionChoiceItems<T>(T value, IDbConnection connection);

    public interface IEditor : IValueEditor
    {
        IContainer Container { get; }
        IUserActionCollection Commands { get; }
        IBrowser Browser { get; }
        IToolBar ToolBar { get; }
        IBrowserParameters Parameters { get; }
        IControl this[string name] { get; }
        IDataCollection Data { get; }
        IContainer CreateContainer();
        IContainer CreateContainer(int height);
        IValueControl CreateLabel(string name, string labelText);
        IBindingControl CreateTextBox(string fieldName, string label, bool multiline = false);
        IBindingControl CreateSelectBox(string fieldName, string label, ChoiceItems getItems, bool showOnlyFolder = false);
        IBindingControl CreateSelectBox(string fieldName, string label, СriterionChoiceItems getItems, bool showOnlyFolder = false);
        IBindingControl CreateSelectBox<T>(string fieldName, string label, СriterionChoiceItems<T> getItems, bool showOnlyFolder = false);
        IBindingControl CreateComboBox(string fieldName, string label, ChoiceItems getItems);
        IBindingControl CreateComboBox(string fieldName, string label, СriterionChoiceItems getItems);
        IBindingControl CreateComboBox<T>(string fieldName, string label, СriterionChoiceItems<T> getItems);
        IBindingControl CreateChoice(string fieldName, string label, IDictionary<int, string> keyValues);
        IBindingControl CreateChoice(string fieldName, string label, ChoiceItems getItems);
        IBindingControl CreateInteger(string fieldName, string label, IntegerLength length = IntegerLength.Int32);
        IBindingControl CreateNumeric(string fieldName, string label, int numberDecimalDigits = 3);
        IBindingControl CreateCurrency(string fieldName, string label);
        IBindingControl CreatePercent(string fieldName, string label, int percentDecimalDigits = 0, double minValue = 0, double maxValue = 100);
        IBindingControl CreateCheckBox(string fieldName, string label);
        IBindingControl CreateDateTimePicker(string fieldName, string label, DateTimeFormat dateTimeFormat = DateTimeFormat.Custom, string customFormat = "dd.MM.yyyy HH:mm:ss", bool showCheck = false);
        IBindingControl CreateImageBox(string fieldName, string label);
        IBindingControl CreateMaskedText<T>(string fieldName, string label, string mask, char promtCharacter = '_') where T : IComparable<T>;
        IDataGrid CreateDataGrid(string name, Func<IDbConnection, IList> getItems);
    }
}
