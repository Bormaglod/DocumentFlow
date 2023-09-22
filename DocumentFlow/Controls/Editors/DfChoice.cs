//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.12.2019
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;

using Humanizer;

using System.ComponentModel;
using System.Reflection;
using System.Collections;

namespace DocumentFlow.Controls.Editors;

public enum KeyGenerationMethod 
{ 
    PostgresEnumValue, 
    EnumValue, 
    IntegerValue 
}

[ToolboxItem(true)]
public partial class DfChoice : DfControl, IAccess
{
    private bool enabledEditor = true;
    private bool showDeleteButton = true;

    public event EventHandler? DeleteButtonClick;

    private class ChoiceItem
    {
        public ChoiceItem(string key, string name)
        {
            Key = key;
            Name = name;
        }

        public string Key { get; set; }
        public string Name { get; set; }
    }

    private string choiceValue = string.Empty;

    public event EventHandler? ChoiceValueChanged;

    public DfChoice()
    {
        InitializeComponent();
        SetNestedControl(panelEdit);

        comboBox.DisplayMember = "Name";
        comboBox.ValueMember = "Key";

        comboBox.DataBindings.Add(nameof(comboBox.SelectedValue), this, nameof(ChoiceValue), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ChoiceValue
    {
        get => choiceValue;
        set
        {
            if (choiceValue != value)
            {
                choiceValue = value;
                ChoiceValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public bool EnabledEditor
    {
        get => enabledEditor;
        set
        {
            if (enabledEditor != value) 
            {
                enabledEditor = value;
                comboBox.Enabled = value;
                buttonDelete.Enabled = value;
            }
        }
    }

    public bool ShowDeleteButton
    {
        get => showDeleteButton;
        set
        {
            if (showDeleteButton != value) 
            { 
                showDeleteButton = value;
                panelDelete.Visible = value;
            }
        }
    }

    public void FromEnum<T>(KeyGenerationMethod method) where T : Enum
    {
        var dataSource = new List<ChoiceItem>();

        Type type = typeof(T);
        foreach (var item in type.GetFields())
        {
            var attr = item.GetCustomAttribute<DescriptionAttribute>();
            if (attr == null)
            {
                continue;
            }

            string key = method switch
            {
                KeyGenerationMethod.PostgresEnumValue => item.Name.Humanize(LetterCasing.LowerCase),
                KeyGenerationMethod.EnumValue => item.Name,
                KeyGenerationMethod.IntegerValue => Convert.ToInt32(Enum.Parse(type, item.Name)).ToString(),
                _ => throw new NotImplementedException()
            };

            ChoiceItem choice = new(key, attr.Description);
            dataSource.Add(choice);
        }

        comboBox.DataSource = dataSource;
    }

    public void From(IEnumerable values)
    {
        var dataSource = new List<ChoiceItem>();

        var dictTypes = new Dictionary<Type, (PropertyInfo Key, PropertyInfo Value)>();

        foreach (var item in values)
        {
            var type = item.GetType();
            if (type.Name.StartsWith("KeyValuePair"))
            {
                if (!dictTypes.TryGetValue(type, out var info))
                {
                    var propKey = type.GetProperty("Key");
                    if (propKey == null)
                    {
                        continue;
                    }

                    info.Key = propKey;

                    var propValue = type.GetProperty("Value");
                    if (propValue == null)
                    {
                        continue;
                    }

                    info.Value = propValue;

                    dictTypes.Add(type, info);
                }

                var key = info.Key.GetValue(item)?.ToString() ?? throw new NullReferenceException();
                var value = info.Value!.GetValue(item)?.ToString() ?? throw new NullReferenceException();

                dataSource.Add(new ChoiceItem(key, value));
            }
            else
            {
                var key = item.ToString();
                if (key == null)
                {
                    continue;
                }

                dataSource.Add(new ChoiceItem(key, key));
            }
        }

        comboBox.DataSource = dataSource;
    }

    private void ButtonDelete_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ChoiceValue))
        {
            ChoiceValue = string.Empty;
            comboBox.SelectedIndex = -1;
            DeleteButtonClick?.Invoke(this, EventArgs.Empty);
        }
    }
}
