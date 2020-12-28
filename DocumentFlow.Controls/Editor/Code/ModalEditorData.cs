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
using System.Linq;
using System.Windows.Forms;
using DocumentFlow.Code;
using DocumentFlow.Code.Implementation;
using DocumentFlow.Code.System;
using DocumentFlow.Core;

namespace DocumentFlow.Controls.Editor.Code
{
    public class ModalEditorData : IEditor
    {
        private readonly IContainer controlContainer;
        private readonly IBrowserParameters parameters;
        private readonly IDataCollection dataCollection;
        private readonly IPopulateCollection populateCollection;

        public ModalEditorData(IContainer container, IBrowserParameters browserParameters)
        {
            controlContainer = container;
            parameters = browserParameters;
            dataCollection = new DataCollection(container);
            populateCollection = new PopulateCollection(container);
        }

        public IIdentifier Entity { get; set; }

        public IToolBar ToolBar => GetToolBar();

        IContainer IEditor.Container => controlContainer;

        ICommandCollection IEditor.Commands => GetCommandCollection();

        IBrowser IEditor.Browser => GetBrowser();

        IBrowserParameters IEditor.Parameters => parameters;

        IControl IEditor.this[string name] => controlContainer.ControlsAll.OfType<IDataName>().First(x => x.Name == name) as IControl;

        IDataCollection IEditor.Data => dataCollection;

        IPopulateCollection IEditor.Populates => populateCollection;

        IContainer IEditor.CreateContainer() => ((IEditor)this).CreateContainer(100);

        IContainer IEditor.CreateContainer(int height)
        {
            var panel = new Panel
            {
                Padding = new Padding(1),
                Height = height,
                Dock = DockStyle.Top
            };

            ContainerData container = new ContainerData(panel);
            return container;
        }

        IBindingControl IEditor.CreateTextBox(string fieldName, string label, bool multiline)
        {
            var textBox = new L_TextBox
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
            var selectBox = new L_SelectBox
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
            var selectBox = new L_SelectBox
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
            var comboBox = new L_ComboBox
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
            var comboBox = new L_ComboBox
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
            var currencyTextBox = new L_CurrencyTextBox
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
            var choice = new L_Choice(keyValues)
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

        IBindingControl IEditor.CreateChoice(string fieldName, string label, ChoiceItems getItems)
        {
            var choice = new L_Choice
            {
                Height = 32,
                Dock = DockStyle.Top
            };

            IBindingControl controlData = new ListControlData(choice, getItems, choice.AddItems)
            {
                LabelText = label,
                FieldName = fieldName
            };

            return controlData;
        }

        IBindingControl IEditor.CreateInteger(string fieldName, string label, IntegerLength length)
        {
            var integerTextBox = new L_IntegerTextBox
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
            var numericTextBox = new L_NumericTextBox
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
            var checkBox = new L_CheckBox
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
            var percentTextBox = new L_PercentTextBox
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
            var dateTimePicker = new L_DateTimePicker
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
            var imageViewerBox = new L_ImageViewerBox
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

        IBindingControl IEditor.CreateMaskedText<T>(string fieldName, string label, string mask, char promtCharacter)
        {
            var maskedTextBox = new L_MaskedTextBox<T>
            {
                Height = 32,
                Dock = DockStyle.Top,
                Mask = mask,
                PromptCharacter = promtCharacter
            };

            IBindingControl controlData = new BindingControlData(maskedTextBox)
            {
                LabelText = label,
                FieldName = fieldName
            };

            return controlData;
        }

        IDataGrid IEditor.CreateDataGrid(string name, Func<IDbConnection, IList> getItems)
        {
            if (Entity is IIdentifier<Guid> identifier)
            {
                var dataGrid = new L_DataGrid(identifier.id, getItems)
                {
                    Height = 100,
                    Dock = DockStyle.Top
                };

                IDataGrid controlData = new GridControlData(dataGrid, name, dataGrid.Columns);

                return controlData;
            }

            return null;
        }

        protected virtual IToolBar GetToolBar() => null;

        protected virtual ICommandCollection GetCommandCollection() => null;

        protected virtual IBrowser GetBrowser() => null;
    }
}
