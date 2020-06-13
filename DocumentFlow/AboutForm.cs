//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.06.2020
// Time: 10:05
//-----------------------------------------------------------------------

namespace DocumentFlow
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using Syncfusion.Windows.Forms;
    using Syncfusion.WinForms.ListView.Events;
    using DocumentFlow.Data.Core;

    public partial class AboutForm : MetroForm
    {
        class Lib
        {
            public Lib(Assembly assembly)
            {
                AssemblyName name = assembly.GetName();
                Name = name.Name;
                Version = name.Version;
                FullName = name.FullName;
            }

            public string Name { get; set; }
            public Version Version { get; set; }
            public string FullName { get; set; }
            public override string ToString()
            {
                return $"{Name} - {Version}";
            }
        }

        private List<Lib> libs = new List<Lib>();

        public AboutForm()
        {
            InitializeComponent();
            labelVersion.Text = string.Format(labelVersion.Text, Assembly.GetExecutingAssembly().GetName().Version);

            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (a == Assembly.GetExecutingAssembly())
                    continue;

                Lib lib = new Lib(a);
                if (lib.Name.Contains("Anonymously") || lib.Name.Contains("Proxy"))
                    continue;

                libs.Add(lib);
            }

            listLibs.DataSource = libs.OrderBy(x => x.Name);
            labelDatabase.Text = string.Format(labelDatabase.Text, Db.ConnectionName);
        }

        private void listLibs_SelectionChanging(object sender, ItemSelectionChangingEventArgs e)
        {
            if (listLibs.SelectedItem is Lib lib)
            {
                textDescr.Text = lib.FullName;
            }
        }
    }
}
