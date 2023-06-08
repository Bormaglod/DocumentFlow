//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 8.06.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Dialogs;

public partial class InputCurrencyDialog : Form
{
    private InputCurrencyDialog()
    {
        InitializeComponent();
    }

    public static bool ShowDialog(out decimal value)
    {
        value = 0;

        var dialog = new InputCurrencyDialog();
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            value = dialog.currencyText.DecimalValue;
            return true;
        }

        return false;
    }
}
