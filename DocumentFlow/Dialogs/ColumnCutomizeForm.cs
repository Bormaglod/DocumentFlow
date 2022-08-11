//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Settings;

using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Dialogs;

public partial class ColumnCutomizeForm : Form
{
    private readonly BrowserColumn column;

    public ColumnCutomizeForm(BrowserColumn column)
    {
        InitializeComponent();

        this.column = column;

        textName.Text = column.Header;
        comboAutoSize.SelectedIndex = column.AutoSizeMode switch
        {
            AutoSizeColumnsMode.None => 0,
            AutoSizeColumnsMode.AllCellsExceptHeader => 1,
            AutoSizeColumnsMode.ColumnHeader => 2,
            AutoSizeColumnsMode.Fill => 3,
            _ => throw new NotImplementedException()
        };
        textWidth.IntegerValue = column.Width ?? 0;
        buttonVisible.ToggleState = column.Visible ? ToggleButtonState.Active : ToggleButtonState.Inactive;
        buttonVisible.Enabled = column.Hidden;
    }

    private void ComboBoxAdv1_SelectedIndexChanging(object sender, SelectedIndexChangingArgs e)
    {
        textWidth.Enabled = e.NewIndex == 0;
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        if (comboAutoSize.SelectedIndex == 0 && textWidth.IntegerValue == 0)
        {
            MessageBox.Show("Укажите ширину колонки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
            return;
        }

        column.AutoSizeMode = comboAutoSize.SelectedIndex switch
        {
            0 => AutoSizeColumnsMode.None,
            1 => AutoSizeColumnsMode.AllCellsExceptHeader,
            2 => AutoSizeColumnsMode.ColumnHeader,
            3 => AutoSizeColumnsMode.Fill,
            _ => throw new NotImplementedException()
        };

        column.Header = textName.Text;
        column.Width = comboAutoSize.SelectedIndex == 0 ? Convert.ToInt32(textWidth.IntegerValue) : null;
        column.Visible = buttonVisible.ToggleState == ToggleButtonState.Active;
    }
}
