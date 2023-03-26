//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.07.2022
//
// Версия 2022.11.26
//  - добавлен метод RefreshDataSourceOnLoad
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Dialogs;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;

using System.Collections.ObjectModel;
using System.Data;

namespace DocumentFlow.Controls.Editors;

public partial class DfOperationProperty : BaseControl, IDataSourceControl, IGridDataSource, IAccess
{
    private Guid? ownerId;
    private ObservableCollection<CalculationOperationProperty>? properties;
    private readonly List<CalculationOperationProperty> deleted = new();
    private readonly List<CalculationOperationProperty> created = new();
    private readonly List<CalculationOperationProperty> updated = new();

    public DfOperationProperty(string header, int headerWidth, int editorWidth) : base(string.Empty)
    {
        InitializeComponent();

        Dock = DockStyle.Top;
        Header = header;
        HeaderWidth = headerWidth;
        EditorWidth = editorWidth;
    }

    public string Header { get => label1.Text; set => label1.Text = value; }

    public int HeaderWidth { get => label1.Width; set => label1.Width = value; }

    public bool HeaderAutoSize { get => label1.AutoSize; set => label1.AutoSize = value; }

    public ContentAlignment HeaderTextAlign { get => label1.TextAlign; set => label1.TextAlign = value; }

    public bool HeaderVisible { get => label1.Visible; set => label1.Visible = value; }

    public int EditorWidth { get => panel1.Width; set => panel1.Width = value; }

    public bool ReadOnly
    {
        get => toolStrip1.Enabled;
        set => toolStrip1.Enabled = value;
    }

    public bool EditorFitToSize
    {
        get => panel1.Dock == DockStyle.Fill;
        set => panel1.Dock = value ? DockStyle.Fill : panel1.Dock = DockStyle.Left;
    }

    public void SetOwner(Guid ownerId) => this.ownerId = ownerId;

    public void UpdateData(IDbTransaction transaction)
    {
        if (ownerId == null)
        {
            throw new ArgumentNullException(nameof(ownerId), "Не определено значение owner_id.");
        }

        var repository = Services.Provider.GetService<ICalculationOperationRepository>();
        if (repository != null)
        {
            foreach (var item in created)
            {
                item.OperationId = ownerId.Value;
                repository.AddProperty(item, transaction);
            }

            foreach (var item in updated)
            {
                repository.UpdateProperty(item, transaction);
            }

            foreach (var item in deleted)
            {
                repository.DeleteProperty(item, transaction);
            }

            created.Clear();
            updated.Clear();
            deleted.Clear();
        }
    }

    #region IDataSourceControl interface

    public void RefreshDataSource()
    {
        properties = null;
        if (ownerId != null)
        {
            var repo = Services.Provider.GetService<ICalculationOperationRepository>();
            if (repo != null)
            {
                properties = new ObservableCollection<CalculationOperationProperty>(repo.GetProperties(ownerId.Value));
            }
        }

        gridParams.DataSource = properties;

        UpdateGridHeight();
    }

    public void RefreshDataSourceOnLoad() => RefreshDataSource();

    #endregion

    private void UpdateGridHeight()
    {
        var count = properties?.Count ?? 0;
        if (count == 0)
        {
            Height = 32;
        }
        else
        {
            Height = 32 + (properties?.Count ?? 0) * gridParams.RowHeight + 3;
        }
    }

    private void ButtonAdd_Click(object sender, EventArgs e)
    {
        if (properties != null && ownerId != null)
        {
            var prop = PropertyForm.Create();
            if (prop != null)
            {
                properties.Add(prop);
                created.Add(prop);
                UpdateGridHeight();
            }
        }
    }

    private void ButtonEdit_Click(object sender, EventArgs e)
    {
        if (properties != null && gridParams.SelectedItem is CalculationOperationProperty prop && PropertyForm.Edit(prop))
        {
            if (!created.Contains(prop) && !updated.Contains(prop))
            {
                updated.Add(prop);
            }

            gridParams.Refresh();
        }
    }

    private void ButtonDelete_Click(object sender, EventArgs e)
    {
        if (properties != null && gridParams.SelectedItem is CalculationOperationProperty prop)
        {
            properties.Remove(prop);
            if (created.Contains(prop))
            {
                created.Remove(prop);
            }
            else
            {
                if (updated.Contains(prop))
                {
                    updated.Remove(prop);
                }

                deleted.Add(prop);
            }

            UpdateGridHeight();
        }
    }

    private void GridParams_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
    {
        switch (e.Column.MappingName)
        {
            case "PropertyName":
                e.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.AllCells;
                break;
            case "PropertyValue":
                e.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                break;
        }
    }
}
