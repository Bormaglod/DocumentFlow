//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.06.2020
//
// Версия 2023.5.3
//  - в конструктор добавлены параметры IDatabase и соответственно
//    исправлена логика работы
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;

using Syncfusion.WinForms.ListView.Events;

using System.Data;
using System.Reflection;

namespace DocumentFlow;

public partial class AboutForm : Form, IAbout
{
    class Lib
    {
        private readonly string name;
        private readonly Version version;
        private readonly string fullName;

        public Lib(AssemblyName assemblyName) => (name, version, fullName) = (assemblyName?.Name ?? string.Empty, assemblyName?.Version ?? new Version(), assemblyName?.FullName ?? string.Empty);
        public string Name => name;
        public Version Version => version;
        public string FullName => fullName;
        public override string ToString() => $"{Name} - {Version}";
    }

    private readonly List<Lib> libs = new();

    public AboutForm(IDatabase database)
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
        labelDatabase.Text = string.Format(labelDatabase.Text, database.ConnectionName);
    }

    public void ShowWindow() => ShowDialog();

    private void ListLibs_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
    {
        if (listLibs.SelectedItem is Lib lib)
        {
            textDescr.Text = lib.FullName;
        }
    }
}
