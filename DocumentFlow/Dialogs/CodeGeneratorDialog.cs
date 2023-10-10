//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.10.2023
//-----------------------------------------------------------------------
using DocumentFlow.Data;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Tools;

using Syncfusion.Windows.Forms.Tools;

using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace DocumentFlow.Dialogs;

[Dialog]
public partial class CodeGeneratorDialog : Form
{
    [GeneratedRegex("^(\\d{1})\\.(\\d{3,4})\\.(\\d{7}).(\\d{2})$")]
    private static partial Regex CodeRegex();

    private readonly ICodeGeneratorRepository repository;

    private short productType = -1;
    private short brand = -1;
    private short model = -1;
    private short engineFrom = -1;
    private short engineTo = -1;
    private short number = 0;
    private short mod = 0;

    public event EventHandler? ProductTypeChanged;
    public event EventHandler? BrandChanged;
    public event EventHandler? ModelChanged;
    public event EventHandler? EngineFromChanged;
    public event EventHandler? EngineToChanged;
    public event EventHandler? NumberChanged;
    public event EventHandler? ModChanged;

    public CodeGeneratorDialog(ICodeGeneratorRepository repository)
    {
        InitializeComponent();

        this.repository = repository;

        InitializeComboControl(comboType, nameof(ProductType), repository.GetTypes);
        InitializeComboControl(comboBrand, nameof(Brand), repository.GetBrands);
        InitializeComboControl(comboModel, nameof(Model));
        InitializeComboControl(comboEngineFrom, nameof(EngineFrom), repository.GetEngines);
        InitializeComboControl(comboEngineTo, nameof(EngineTo), repository.GetEngines);

        numericNumber.DataBindings.Add(nameof(numericNumber.Value), this, nameof(Number), false, DataSourceUpdateMode.OnPropertyChanged);
        numericMod.DataBindings.Add(nameof(numericMod.Value), this, nameof(Mod), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    public short ProductType
    {
        get => productType;
        set
        {
            if (productType != value)
            {
                productType = value;
                ProductTypeChanged?.Invoke(this, EventArgs.Empty);

                UpdatePanelsVisible();
                UpdateCodeText();
            }
        }
    }

    public short Brand
    {
        get => brand;
        set
        {
            if (brand != value)
            {
                brand = value;
                BrandChanged?.Invoke(this, EventArgs.Empty);

                var b = comboBrand.Items.OfType<CodeGenerator>().FirstOrDefault(x => x.CodeId == brand);
                if (b != null) 
                {
                    comboModel.DataSource = repository.GetModels(b);
                }

                UpdateCodeText();
            }
        }
    }

    public short Model
    {
        get => model;
        set
        {
            if (model != value)
            {
                model = value;
                ModelChanged?.Invoke(this, EventArgs.Empty);

                UpdateCodeText();
            }
        }
    }

    public short EngineFrom
    {
        get => engineFrom;
        set
        {
            if (engineFrom != value)
            {
                engineFrom = value;
                EngineFromChanged?.Invoke(this, EventArgs.Empty);

                UpdateCodeText();
            }
        }
    }

    public short EngineTo
    {
        get => engineTo;
        set
        {
            if (engineTo != value)
            {
                engineTo = value;
                EngineToChanged?.Invoke(this, EventArgs.Empty);

                UpdateCodeText();
            }
        }
    }

    public short Number
    {
        get => number;
        set
        {
            if (number != value)
            {
                number = value;
                NumberChanged?.Invoke(this, EventArgs.Empty);

                UpdateCodeText();
            }
        }
    }

    public short Mod
    {
        get => mod;
        set
        {
            if (mod != value)
            {
                mod = value;
                ModChanged?.Invoke(this, EventArgs.Empty);

                UpdateCodeText();
            }
        }
    }

    public bool Get(string source, [MaybeNullWhen(false)] out string code)
    {
        var match = CodeRegex().Match(source);

        if (!match.Success)
        {
            throw new Exception("Код должен имет вид 0.000.0000000.00");
        }

        ProductType = short.Parse(match.Groups[1].Value);

        if (productType == 4)
        {
            EngineFrom = short.Parse(match.Groups[2].Value.AsSpan(0, 2));
            EngineTo = short.Parse(match.Groups[2].Value.AsSpan(2, 2));
        }
        else
        {
            Brand = short.Parse(match.Groups[2].Value.AsSpan(0, 1));
            Model = short.Parse(match.Groups[2].Value.AsSpan(1, 2));
        }

        Number = short.Parse(match.Groups[3].Value.AsSpan(4, 3));
        Mod = short.Parse(match.Groups[4].Value);

        if (ShowDialog() == DialogResult.OK)
        {
            code = labelCode.Text;
            return true;
        }

        code = default;
        return false;
    }

    private void UpdateCodeText()
    {
        labelCode.Text = ProductType switch
        {
            1 => $"1.000.3724{Number:000}.{Mod:00}",
            2 or 3 => $"{ProductType}.{Brand}{Model:00}.3724{Number:000}.{Mod:00}",
            4 => $"4.{EngineFrom:00}{EngineTo:00}.3724{Number:000}.{Mod:00}",
            _ => "0.000.0000000.00",
        };
    }

    private void InitializeComboControl(ComboBoxAdv combo, string propertyName, Func<IEnumerable<CodeGenerator>>? codes = null)
    {
        combo.DataBindings.Add(nameof(combo.SelectedValue), this, propertyName, true, DataSourceUpdateMode.OnPropertyChanged);

        if (codes != null)
        {
            combo.DataSource = codes?.Invoke();
        }

        combo.DisplayMember = nameof(CodeGenerator.CodeName);
        combo.ValueMember = nameof(CodeGenerator.CodeId);
    }


    private void UpdatePanelsVisible()
    {
        switch (ProductType)
        {
            case 1:
                panelBrand.Visible = false;
                panelModel.Visible = false;
                panelEngineFrom.Visible = false;
                panelEngineTo.Visible = false;

                Height = 226;
                break;
            case 2:
            case 3:
                panelModel.Visible = true;
                panelBrand.Visible = true;
                panelEngineFrom.Visible = false;
                panelEngineTo.Visible = false;

                Height = 284;
                break;
            case 4:
                panelBrand.Visible = false;
                panelModel.Visible = false;
                panelEngineTo.Visible = true;
                panelEngineFrom.Visible = true;

                Height = 284;
                break;
        }
    }
}
