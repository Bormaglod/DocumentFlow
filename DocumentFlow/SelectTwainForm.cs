//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2021
// Time: 22:32
//-----------------------------------------------------------------------

using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using NTwain;

namespace DocumentFlow
{
    public partial class SelectTwainForm : Form
    {
        private class DS
        {
            public string Name => DataSource.Name;
            public DataSource DataSource { get; set; }
            public override string ToString() => Name;
        }

        private SelectTwainForm(BindingList<DS> list)
        {
            InitializeComponent();
            comboBoxData.DataSource = list;
        }

        public static DataSource ShowDialog(TwainSession twain, string dataSource)
        {
            BindingList<DS> list = new();
            if (twain.State >= 3)
            {
                foreach (var ds in twain)
                {
                    list.Add(new DS() { DataSource = ds });
                }
            }

            DataSource src = list.FirstOrDefault(x => x.Name == dataSource)?.DataSource;
            if (src != null)
            {
                return src;
            }

            SelectTwainForm form = new(list);
            
            if (form.ShowDialog() == DialogResult.OK && form.comboBoxData.SelectedItem != null)
            {
                return (form.comboBoxData.SelectedItem as DS).DataSource;
            }

            return null;
        }
    }
}
