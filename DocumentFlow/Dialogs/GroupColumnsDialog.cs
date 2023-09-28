//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.11.2020
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;

using Syncfusion.DataSource;

using System.Collections.ObjectModel;

namespace DocumentFlow.Dialogs;

public partial class GroupColumnsDialog : Form
{
    private readonly ObservableCollection<IGroupColumn> availables;
    private readonly ObservableCollection<IGroupColumn> selected;

    public GroupColumnsDialog(IGroupColumnCollection groups)
    {
        InitializeComponent();

        availables = new ObservableCollection<IGroupColumn>(groups.AvailableGroups.Except(groups.SelectedGroups));
        listViewAll.DataSource = availables;
        listViewAll.DisplayMember = "Text";
        listViewAll.ValueMember = "Text";

        selected = new ObservableCollection<IGroupColumn>(groups.SelectedGroups);
        listViewSelected.DataSource = selected;
        listViewSelected.DisplayMember = "Text";
        listViewSelected.ValueMember = "Text";

        listViewAll.View.GroupDescriptors.Add(new GroupDescriptor()
        {
            PropertyName = "Description"
        });

        listViewSelected.View.GroupDescriptors.Add(new GroupDescriptor()
        {
            PropertyName = "Description"
        });

        listViewSelected.View.SortDescriptors.Add(new SortDescriptor()
        {
            PropertyName = "Order"
        });
    }

    public IEnumerable<IGroupColumn> Selected => selected;

    private void SelectGroup()
    {
        if (listViewAll.SelectedItem is IGroupColumn column)
        {
            column.Order = selected.Count;
            selected.Add(column);
            availables.Remove(column);
        }
    }

    private void RemoveGroup()
    {
        if (listViewSelected.SelectedItem is IGroupColumn column)
        {
            availables.Add(column);
            selected.Remove(column);
        }
    }

    private void ButtonSelect_Click(object sender, EventArgs e) => SelectGroup();

    private void ButtonRemove_Click(object sender, EventArgs e) => RemoveGroup();

    private void ButtonUp_Click(object sender, EventArgs e)
    {
        if (listViewSelected.SelectedItem is IGroupColumn column)
        {
            if (column.Order > 0)
            {
                var prev = selected.FirstOrDefault(x => x.Order == column.Order - 1);
                if (prev != null)
                {
                    prev.Order = column.Order;
                }

                column.Order--;
                listViewSelected.View.Refresh();
                listViewSelected.SelectedItem = column;
            }
        }
    }

    private void ButtonDown_Click(object sender, EventArgs e)
    {
        if (listViewSelected.SelectedItem is IGroupColumn column)
        {
            if (column.Order < selected.Count - 1)
            {
                var prev = selected.FirstOrDefault(x => x.Order == column.Order + 1);
                if (prev != null)
                {
                    prev.Order = column.Order;
                }

                column.Order++;
                listViewSelected.View.Refresh();
                listViewSelected.SelectedItem = column;
            }
        }
    }

    private void ListViewAll_DoubleClick(object sender, EventArgs e) => SelectGroup();

    private void ListViewSelected_DoubleClick(object sender, EventArgs e) => RemoveGroup();
}
