//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.11.2020
// Time: 22:35
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFlow.Code;
using DocumentFlow.Code.Implementation;
using DocumentFlow.Code.System;
using DocumentFlow.Controls.Editor;
using DocumentFlow.Core;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Controls.Code
{
    public class EditorData : IEditor
    {
        private IContainer controlContainer;
        private IBrowserParameters parameters;
        private IBrowser ownerBrowser;

        public EditorData(IContainer container, IBrowser browser, IBrowserParameters browserParameters)
        {
            controlContainer = container;
            parameters = browserParameters;
            ownerBrowser = browser;
        }

        public object Entity { get; set; }

        IContainer IEditor.Container => controlContainer;

        IBrowser IEditor.Browser => ownerBrowser;

        IBrowserParameters IEditor.Parameters => parameters;

        IBindingControl IEditor.this[string name] => controlContainer.ControlsAll.OfType<IBindingControl>().First(x => x.FieldName == name);

        IContainer IEditor.CreateContainer() => ((IEditor)this).CreateContainer(100);

        IContainer IEditor.CreateContainer(int height)
        {
            Panel panel = new Panel()
            {
                Padding = new Padding(1),
                Height = height,
                Dock = DockStyle.Top
            };

            ContainerData container = new ContainerData(panel);
            return container;
        }

        IDbConnection IEditor.CreateConnection() => Db.OpenConnection();

        IBindingControl IEditor.CreateTextBox(string fieldName, string label, bool multiline)
        {
            L_TextBox textBox = new L_TextBox
            {
                Multiline = multiline,
                Height = 32,
                Dock = DockStyle.Top
            };

            IBindingControl controlData = new BindingControlData(textBox)
            {
                LabelText = label,
                FieldName = fieldName
            };

            return controlData;
        }

        IBindingControl IEditor.CreateSelectBox(string fieldName, string label, ChoiceItems getItems, bool showOnlyFolder)
        {
            L_SelectBox selectBox = new L_SelectBox()
            {
                ShowOnlyFolder = showOnlyFolder,
                Height = 32,
                Dock = DockStyle.Top
            };

            IBindingControl selectBoxData = new ListControlData(selectBox, getItems, selectBox.AddItems)
            {
                LabelText = label,
                FieldName = fieldName
            };

            return selectBoxData;
        }

        IBindingControl IEditor.CreateSelectBox(string fieldName, string label, СriterionChoiceItems getItems, bool showOnlyFolder)
        {
            L_SelectBox selectBox = new L_SelectBox()
            {
                ShowOnlyFolder = showOnlyFolder,
                Height = 32,
                Dock = DockStyle.Top
            };

            IBindingControl selectBoxData = new ListControlData(this, selectBox, getItems, selectBox.AddItems)
            {
                LabelText = label,
                FieldName = fieldName
            };

            return selectBoxData;
        }

        IBindingControl IEditor.CreateComboBox(string fieldName, string label, СriterionChoiceItems getItems)
        {
            L_ComboBox comboBox = new L_ComboBox()
            {
                Height = 32,
                Dock = DockStyle.Top
            };

            IBindingControl selectBoxData = new ListControlData(this, comboBox, getItems, comboBox.AddItems)
            {
                LabelText = label,
                FieldName = fieldName
            };

            return selectBoxData;
        }

        IBindingControl IEditor.CreateComboBox(string fieldName, string label, ChoiceItems getItems)
        {
            L_ComboBox comboBox = new L_ComboBox()
            {
                Height = 32,
                Dock = DockStyle.Top
            };

            IBindingControl selectBoxData = new ListControlData(comboBox, getItems, comboBox.AddItems)
            {
                LabelText = label,
                FieldName = fieldName
            };

            return selectBoxData;
        }

        IBindingControl IEditor.CreateCurrency(string fieldName, string label)
        {
            L_CurrencyTextBox currencyTextBox = new L_CurrencyTextBox()
            {
                Height = 32,
                Dock = DockStyle.Top
            };

            IBindingControl controlData = new BindingControlData(currencyTextBox)
            {
                LabelText = label,
                FieldName = fieldName
            };

            return controlData;
        }

        IBindingControl IEditor.CreateChoice(string fieldName, string label, IDictionary<int, string> keyValues)
        {
            L_Choice choice = new L_Choice(keyValues)
            {
                Height = 32,
                Dock = DockStyle.Top
            };

            IBindingControl controlData = new BindingControlData(choice)
            {
                LabelText = label,
                FieldName = fieldName
            };

            return controlData;
        }

        IBindingControl IEditor.CreateInteger(string fieldName, string label, IntegerLength length)
        {
            L_IntegerTextBox integerTextBox = new L_IntegerTextBox()
            {
                Height = 32,
                Dock = DockStyle.Top
            };

            IBindingControl controlData = new BindingControlData(integerTextBox)
            {
                LabelText = label,
                FieldName = fieldName
            };

            return controlData;
        }

        IBindingControl IEditor.CreateNumeric(string fieldName, string label, int numberDecimalDigits)
        {
            L_NumericTextBox numericTextBox = new L_NumericTextBox()
            {
                NumberDecimalDigits = numberDecimalDigits,
                Height = 32,
                Dock = DockStyle.Top
            };

            IBindingControl controlData = new BindingControlData(numericTextBox)
            {
                LabelText = label,
                FieldName = fieldName
            };

            return controlData;
        }

        IBindingControl IEditor.CreateCheckBox(string fieldName, string label)
        {
            L_CheckBox checkBox = new L_CheckBox()
            {
                Height = 32,
                Dock = DockStyle.Top
            };

            IBindingControl controlData = new BindingControlData(checkBox)
            {
                LabelText = label,
                FieldName = fieldName
            };

            return controlData;
        }

        IBindingControl IEditor.CreatePercent(string fieldName, string label, int percentDecimalDigits, double minValue, double maxValue)
        {
            L_PercentTextBox percentTextBox = new L_PercentTextBox()
            {
                PercentDecimalDigits = percentDecimalDigits,
                MinValue = minValue,
                MaxValue = maxValue,
                Height = 32,
                Dock = DockStyle.Top
            };

            IBindingControl controlData = new BindingControlData(percentTextBox)
            {
                LabelText = label,
                FieldName = fieldName
            };

            return controlData;
        }

        IBindingControl IEditor.CreateDateTimePicker(string fieldName, string label, DateTimeFormat dateTimeFormat, string customFormat, bool showCheck)
        {
            L_DateTimePicker dateTimePicker = new L_DateTimePicker()
            {
                Format = EnumHelper.TransformEnum<DateTimePickerFormat, DateTimeFormat>(dateTimeFormat),
                CustomFormat = customFormat,
                ShowCheckBox = showCheck,
                Height = 32,
                Dock = DockStyle.Top
            };

            IBindingControl controlData = new BindingControlData(dateTimePicker)
            {
                LabelText = label,
                FieldName = fieldName
            };

            return controlData;
        }

        IBindingControl IEditor.CreateImageBox(string fieldName, string label)
        {
            L_ImageViewerBox imageViewerBox = new L_ImageViewerBox()
            {
                Height = 32,
                Dock = DockStyle.Top
            };

            IBindingControl controlData = new BindingControlData(imageViewerBox)
            {
                LabelText = label,
                FieldName = fieldName
            };

            return controlData;
        }

        IDataGrid IEditor.CreateDataGrid(string name, Func<IDbConnection, IList> getItems)
        {
            if (Entity is IIdentifier identifier)
            {
                L_DataGrid dataGrid = new L_DataGrid(identifier.id, getItems)
                {
                    Height = 100,
                    Dock = DockStyle.Top
                };

                IDataGrid controlData = new GridControlData(dataGrid, name, dataGrid.Columns);

                return controlData;
            }

            return null;
        }

        T IEditor.ExecuteSqlCommand<T>(string sql, object param)
        {
            using (var conn = Db.OpenConnection())
            {
                return conn.QuerySingleOrDefault<T>(sql, param);
            }
        }
    }
}
