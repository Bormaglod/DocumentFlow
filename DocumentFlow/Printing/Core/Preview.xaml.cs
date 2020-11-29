//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.10.2012
// Time: 7:59
//-----------------------------------------------------------------------

namespace DocumentFlow.Printing.Core
{
    using System.Windows;
    using System.Windows.Input;

    public partial class Preview : Window
    {
        public Preview()
        {
            InitializeComponent();
        }

        private void SendEmail_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SelectEmailWindow win = new SelectEmailWindow();
            win.ShowWindow(((Preview)sender).Title);
        }
    }
}
