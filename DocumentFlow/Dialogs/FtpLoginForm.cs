//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.09.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Dialogs;

public partial class FtpLoginForm : Form
{
    public FtpLoginForm()
    {
        InitializeComponent();
    }

    public string User => textUser.Text;

    public string Password => textPassword.Text;
}
