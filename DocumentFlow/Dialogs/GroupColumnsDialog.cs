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
    private class ObservableLinkedListWarpper
    {
        private readonly ObservableCollection<IGroupColumn> list;
        private readonly LinkedList<IGroupColumn> orderedList;

        public ObservableLinkedListWarpper(IEnumerable<IGroupColumn> source)
        {
            list = new ObservableCollection<IGroupColumn>(source);
            orderedList = new LinkedList<IGroupColumn>(source);
        }

        public IEnumerable<IGroupColumn> List => list;
        public IEnumerable<IGroupColumn> OrderedList => orderedList;

        public void AddLast(IGroupColumn item)
        {
            orderedList.AddLast(item);
            list.Add(item);

            item.Order = list.Count - 1;
        }

        public void Remove(IGroupColumn item)
        {
            orderedList.Remove(item);
            list.Remove(item);
        }

        public void MoveToUp(IGroupColumn item)
        {
            var node = orderedList.Find(item);
            if (node != null && node.Previous != null)
            {
                var prevNode = node.Previous;
                orderedList.Remove(node);
                orderedList.AddBefore(prevNode, item);

                var prevIndex = RemoveCurrent(item, prevNode);
                list.Insert(prevIndex, item);

                UpdateOrderIndexes(item, prevNode);
            }
        }

        public void MoveDown(IGroupColumn item)
        {
            var node = orderedList.Find(item);
            if (node != null && node.Next != null)
            {
                var nextNode = node.Next;
                orderedList.Remove(node);
                orderedList.AddAfter(nextNode, item);

                var nextIndex = RemoveCurrent(item, nextNode);
                list.Insert(nextIndex + 1, item);

                UpdateOrderIndexes(item, nextNode);
            }
        }

        private int RemoveCurrent(IGroupColumn item, LinkedListNode<IGroupColumn> node)
        {
            list.Remove(item);
            return list.IndexOf(node.Value);
        }

        private void UpdateOrderIndexes(IGroupColumn item, LinkedListNode<IGroupColumn> node)
        {
            item.Order = list.IndexOf(item);
            node.Value.Order = list.IndexOf(node.Value);
        }
    }

    private readonly ObservableCollection<IGroupColumn> availables;
    private readonly ObservableLinkedListWarpper selected;

    public GroupColumnsDialog(IGroupColumnCollection groups)
    {
        InitializeComponent();

        availables = new ObservableCollection<IGroupColumn>(groups.AvailableGroups.Except(groups.SelectedGroups));
        listViewAll.DataSource = availables;
        listViewAll.DisplayMember = "Text";
        listViewAll.ValueMember = "Text";
        
        selected = new ObservableLinkedListWarpper(groups.SelectedGroups.OrderBy(x => x.Order));
        listViewSelected.DataSource = selected.List;
        listViewSelected.DisplayMember = "Text";
        listViewSelected.ValueMember = "Text";

        listViewAll.View.GroupDescriptors.Add(new GroupDescriptor()
        {
            PropertyName = "Description"
        });

        listViewSelected.View.SortDescriptors.Add(new SortDescriptor()
        {
            PropertyName = "Order"
        });
    }

    public IEnumerable<IGroupColumn> Selected => selected.OrderedList;

    private void MovetToSelected(IGroupColumn column)
    {
        selected.AddLast(column);
        availables.Remove(column);
    }

    private void RemoveFromSelected(IGroupColumn column)
    {
        selected.Remove(column);
        availables.Add(column);
    }

    private void SelectGroup()
    {
        if (listViewAll.SelectedItem is IGroupColumn column)
        {
            var col = selected.OrderedList.FirstOrDefault(x => x.ColumnName == column.ColumnName);
            if (col != null)
            {
                RemoveFromSelected(col);
            }

            MovetToSelected(column);
        }
    }

    private void RemoveGroup()
    {
        if (listViewSelected.SelectedItem is IGroupColumn column)
        {
            RemoveFromSelected(column);
        }
    }

    private void ButtonSelect_Click(object sender, EventArgs e) => SelectGroup();

    private void ButtonRemove_Click(object sender, EventArgs e) => RemoveGroup();

    private void ButtonUp_Click(object sender, EventArgs e)
    {
        if (listViewSelected.SelectedItem is IGroupColumn column)
        {
            selected.MoveToUp(column);
            listViewSelected.View.Refresh();
            listViewSelected.SelectedItem = column;
        }
    }

    private void ButtonDown_Click(object sender, EventArgs e)
    {
        if (listViewSelected.SelectedItem is IGroupColumn column)
        {
            selected.MoveDown(column);
            listViewSelected.View.Refresh();
            listViewSelected.SelectedItem = column;
        }
    }

    private void ListViewAll_DoubleClick(object sender, EventArgs e) => SelectGroup();

    private void ListViewSelected_DoubleClick(object sender, EventArgs e) => RemoveGroup();
}
