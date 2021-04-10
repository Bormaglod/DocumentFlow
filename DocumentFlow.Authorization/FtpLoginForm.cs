//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.10.2020
// Time: 21:14
//-----------------------------------------------------------------------

using System;
using System.Windows.Forms;

namespace DocumentFlow.Authorization
{
    public partial class FtpLoginForm : Form
    {
        public FtpLoginForm(string userName, string password)
        {
            InitializeComponent();
            textUser.Text = userName;
            textPassword.Text = password;
        }

        public string UserName => textUser.Text;

        public string Password => textPassword.Text;

        private void buttonOk_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(textUser.Text))
            {
                message = "Необходимо указать имя пользователя";
            }
            else if (string.IsNullOrEmpty(textPassword.Text))
            {
                message = "Необходимо указать пароль";
            }

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
        }
    }
}
