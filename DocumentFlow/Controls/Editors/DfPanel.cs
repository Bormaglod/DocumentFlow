//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.11.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Editors;

public partial class DfPanel : UserControl
{
    public DfPanel()
    {
        InitializeComponent();

        Dock = DockStyle.Top;
        Font = new Font("Segoe UI", 10, FontStyle.Regular, GraphicsUnit.Point);
    }

    public string Header { get => labelHeader.Text; set => labelHeader.Text = value; }

    public void AddControls(IList<Control> controls)
    {
        SuspendLayout();
        for (int i = controls.Count - 1; i >= 0; i--)
        {
            controls[i].TabIndex = i;
            panelControls.Controls.Add(controls[i]);
        }

        ResumeLayout(false);
    }
}
