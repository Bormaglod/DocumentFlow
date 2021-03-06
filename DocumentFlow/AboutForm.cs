﻿//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
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
    using System.Windows.Forms;
    using Syncfusion.WinForms.ListView.Events;
    using DocumentFlow.Data;

    public partial class AboutForm : Form
    {
        class Lib
        {
            private readonly string name;
            private readonly Version version;
            private readonly string fullName;

            public Lib(AssemblyName assemblyName) => (name, version, fullName) = (assemblyName.Name, assemblyName.Version, assemblyName.FullName);
            public string Name => name;
            public Version Version => version;
            public string FullName => fullName;
            public override string ToString() => $"{Name} - {Version}";
        }

        private readonly List<Lib> libs = new();

        private AboutForm()
        {
            InitializeComponent();
            labelVersion.Text = string.Format(labelVersion.Text, Assembly.GetExecutingAssembly().GetName().Version);

            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (a == Assembly.GetExecutingAssembly() || a.IsDynamic)
                    continue;

                AssemblyName name = a.GetName();
                if (name.Version == new Version(0, 0, 0, 0))
                    continue;

                Lib lib = new(name);
                if (lib.Name.Contains("Anonymously") || lib.Name.Contains("Proxy"))
                    continue;

                libs.Add(lib);
            }

            listLibs.DataSource = libs.OrderBy(x => x.Name);
            labelDatabase.Text = string.Format(labelDatabase.Text, Db.ConnectionName);
        }

        public static void ShowWindow()
        {
            var form = new AboutForm();
            form.ShowDialog();
        }

        private void listLibs_SelectionChanging(object sender, ItemSelectionChangingEventArgs e)
        {
            
        }

        private void listLibs_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            if (listLibs.SelectedItem is Lib lib)
            {
                textDescr.Text = lib.FullName;
            }
        }
    }
}
