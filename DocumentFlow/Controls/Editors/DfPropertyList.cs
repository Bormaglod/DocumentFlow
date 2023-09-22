//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.07.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;

using Syncfusion.WinForms.DataGrid.Events;

using System.ComponentModel;

namespace DocumentFlow.Controls.Editors;

[ToolboxItem(true)]
public partial class DfPropertyList : DfControl, IAccess
{
    private bool enabledEditor = true;

    public DfPropertyList()
    {
        InitializeComponent();

        SetNestedControl(panelEdit);
    }

    public bool EnabledEditor
    {
        get => enabledEditor;
        set
        {
            if (enabledEditor != value)
            {
                enabledEditor = value;
                toolStrip1.Enabled = value;
            }
        }
    }

    private void GridParams_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
    {

    }
}
