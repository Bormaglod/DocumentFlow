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
using DocumentFlow.Code.System;

namespace DocumentFlow.Code
{
    public delegate IEnumerable<IIdentifier> ChoiceItems(IDbConnection connection);
    public delegate IEnumerable<IIdentifier> СriterionChoiceItems(IEditor editor, IDbConnection connection);

    public interface IEditor
    {
        IContainer Container { get; }
        IBrowser Browser { get; }
        IBrowserParameters Parameters { get; }
        object Entity { get; }
        IBindingControl this[string name] { get; }
        IContainer CreateContainer();
        IContainer CreateContainer(int height);
        IDbConnection CreateConnection();
        IBindingControl CreateTextBox(string fieldName, string label, bool multiline = false);
        IBindingControl CreateSelectBox(string fieldName, string label, ChoiceItems getItems, bool showOnlyFolder = false);
        IBindingControl CreateSelectBox(string fieldName, string label, СriterionChoiceItems getItems, bool showOnlyFolder = false);
        IBindingControl CreateComboBox(string fieldName, string label, ChoiceItems getItems);
        IBindingControl CreateComboBox(string fieldName, string label, СriterionChoiceItems getItems);
        IBindingControl CreateChoice(string fieldName, string label, IDictionary<int, string> keyValues);
        IBindingControl CreateInteger(string fieldName, string label, IntegerLength length = IntegerLength.Int32);
        IBindingControl CreateNumeric(string fieldName, string label, int numberDecimalDigits = 3);
        IBindingControl CreateCurrency(string fieldName, string label);
        IBindingControl CreatePercent(string fieldName, string label, int percentDecimalDigits = 0, double minValue = 0, double maxValue = 100);
        IBindingControl CreateCheckBox(string fieldName, string label);
        IBindingControl CreateDateTimePicker(string fieldName, string label, DateTimeFormat dateTimeFormat = DateTimeFormat.Custom, string customFormat = "dd.MM.yyyy HH:mm:ss", bool showCheck = false);
        IBindingControl CreateImageBox(string fieldName, string label);
        IBindingControl CreateMaskedText<T>(string fieldName, string label, string mask, char promtCharacter = '_') where T : IComparable<T>;
        IDataGrid CreateDataGrid(string name, Func<IDbConnection, IList> getItems);
        T ExecuteSqlCommand<T>(string sql, object param = null);
    }
}
