//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Entities.Calculations;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Dialogs;

public partial class PropertyForm : Form
{
    public PropertyForm()
    {
        InitializeComponent();

        var repo = Services.Provider.GetService<ICalculationOperationRepository>();
        if (repo != null)
        {
            comboName.DataSource = repo.GetListProperties();
        }
    }

    public Property? Parameter 
    { 
        get
        {
            if (comboName.SelectedItem is Property prop)
            {
                return prop;
            }

            return null;
        }

        set => comboName.SelectedItem = value;
    }

    public string Value 
    {
        get => textValue.Text;
        set => textValue.Text = value;
    }

    public static CalculationOperationProperty? Create()
    {
        PropertyForm f = new();
        if (f.ShowDialog() == DialogResult.OK && f.Parameter != null)
        {
            CalculationOperationProperty prop = new()
            {
                property_id = f.Parameter.id,
                Property = f.Parameter,
                property_value = f.Value
            };

            return prop;
        }

        return null;
    }

    public static bool Edit(CalculationOperationProperty prop)
    {
        PropertyForm f = new()
        {
            Parameter = prop.Property,
            Value = prop.property_value ?? string.Empty
        };

        if (f.ShowDialog() == DialogResult.OK && f.Parameter != null)
        {
            prop.property_id = f.Parameter.id;
            prop.Property = f.Parameter;
            prop.property_value = f.Value;

            return true;
        }

        return false;
    }
}
