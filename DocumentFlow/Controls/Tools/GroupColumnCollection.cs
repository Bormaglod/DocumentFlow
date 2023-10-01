//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.11.2020
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;

using Syncfusion.WinForms.DataGrid;

using System.Collections.Specialized;

namespace DocumentFlow.Controls.Tools;

public class GroupColumnCollection : IGroupColumnCollection
{
    private readonly SfDataGrid grid;
    private readonly List<GroupColumn> availableGroups = new();
    private readonly List<GroupColumn> selectedGroups = new();

    private bool lockManual = false;

    private class GroupColumn : IGroupColumn, IEquatable<GroupColumn>
    {
        private readonly GroupColumnDescription column;

        public GroupColumn(GridColumn gridColumn)
        {
            column = new GroupColumnDescription()
            {
                ColumnName = gridColumn.MappingName,
                SortGroupRecords = false
            };

            ColumnName = gridColumn.MappingName;
            Name = gridColumn.MappingName;
            Text = gridColumn.HeaderText;
            Description = "Все столбцы";
        }

        public GroupColumn(GridColumn gridColumn, string name, string description, Func<string, object, object> keySelector)
        {
            column = new GroupColumnDescription()
            {
                ColumnName = gridColumn.MappingName,
                SortGroupRecords = false,
                KeySelector = keySelector
            };

            ColumnName = gridColumn.MappingName;
            Name = name;
            Text = $"{gridColumn.HeaderText}: {description}";
            Description = gridColumn.HeaderText;
        }

        public GroupColumn(GroupColumnDescription groupColumn)
        {
            column = groupColumn;
            ColumnName = groupColumn.ColumnName;
            Name = groupColumn.ColumnName;
            Text = groupColumn.ColumnName;
            Description = "Все столбцы";
        }

        public int Order { get; set; } = -1;
        public string ColumnName { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public GroupColumnDescription Column => column;

        public bool Equals(GroupColumn? other)
        {
            if (other is null)
            {
                return false;
            }

            return Name == other.Name;
        }

        public override bool Equals(object? obj) => Equals(obj as GroupColumn);
        public override int GetHashCode() => Name.GetHashCode();
        public override string ToString() => Text;
    }

    public GroupColumnCollection(SfDataGrid grid)
    {
         this.grid = grid;

        foreach (GridColumn column in grid.Columns) 
        { 
            if (column is GridUnboundColumn)
            {
                continue;
            }

            availableGroups.Add(new GroupColumn(column));
        }

        grid.GroupColumnDescriptions.CollectionChanged += GroupColumnDescriptions_CollectionChanged;
    }

    public IReadOnlyList<IGroupColumn> AvailableGroups => availableGroups;
    public IReadOnlyList<IGroupColumn> SelectedGroups => selectedGroups;

    public IGroupColumnCollection Add(GridColumn column)
    {
        if (grid.AllowGrouping)
        {
            var grp = availableGroups.FirstOrDefault(x => x.Column.ColumnName == column.MappingName && x.Column.KeySelector == null);
            if (grp != null)
            {
                AddOrderGroup(grp);
            }
        }

        return this;
    }

    public IGroupColumnCollection Add(string name)
    {
        if (grid.AllowGrouping)
        {
            var grp = availableGroups.FirstOrDefault(x => x.Name == name);
            if (grp != null)
            {
                AddOrderGroup(grp);
            }
        }

        return this;
    }

    public IGroupColumnCollection Register(GridColumn column, string name, string description, Func<string, object, object> keySelector)
    {
        if (grid.AllowGrouping)
        {
            var grp = availableGroups.FirstOrDefault(x => x.Column.ColumnName == column.MappingName && x.Column.KeySelector == null);
            if (grp != null) 
            {
                grp.Description = column.HeaderText;
            }

            grp = new GroupColumn(column, name, description, keySelector);
            availableGroups.Add(grp);
        }

        return this;
    }

    public void SetSelectedGroups(IEnumerable<IGroupColumn> groups)
    {
        lockManual = true;
        try
        {
            grid.GroupColumnDescriptions.Clear();
            selectedGroups.Clear();
            foreach (var group in groups.OrderBy(x => x.Order).OfType<GroupColumn>())
            {
                AddGroup(group);
            }
        }
        finally
        { 
            lockManual = false; 
        }
    }

    public void SetSelectedGroups(IEnumerable<string> groups)
    {
        lockManual = true;
        try
        {
            grid.GroupColumnDescriptions.Clear();
            selectedGroups.Clear();
            foreach (var group in groups)
            {
                Add(group);
            }
        }
        finally
        {
            lockManual = false;
        }
    }

    private void AddOrderGroup(GroupColumn grp) 
    {
        lockManual = true;
        try
        {
            grp.Order = selectedGroups.Count;
            AddGroup(grp);
        }
        finally
        {
            lockManual = false;
        }
    }

    private void AddGroup(GroupColumn grp)
    {
        selectedGroups.Add(grp);
        grid.GroupColumnDescriptions.Add(grp.Column);
    }

    private void GroupColumnDescriptions_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (lockManual)
        {
            return;
        }

        switch (e.Action) 
        { 
            case NotifyCollectionChangedAction.Add:
                if (e.NewItems != null && e.NewItems.Count > 0 && e.NewItems[0] is GroupColumnDescription newGroupColumnDescription)
                {
                    var newGroup = new GroupColumn(newGroupColumnDescription)
                    {
                        Order = selectedGroups.Count
                    };

                    var grp = availableGroups.FirstOrDefault(x => x.ColumnName == newGroupColumnDescription.ColumnName);
                    if (grp != null)
                    {
                        newGroup.Text = grp.Text;
                        availableGroups.Remove(grp);
                    }

                    availableGroups.Add(newGroup);
                    selectedGroups.Add(newGroup);
                }

                break;
            case NotifyCollectionChangedAction.Remove:
                if (e.OldItems != null && e.OldItems.Count > 0 && e.OldItems[0] is GroupColumnDescription oldGroupColumnDescription)
                {
                    var grp = selectedGroups.FirstOrDefault(x => x.ColumnName == oldGroupColumnDescription.ColumnName);
                    if (grp != null) 
                    {
                        selectedGroups.Remove(grp);
                    }
                }

                break;
        }

        for (int i = 0; i < grid.GroupColumnDescriptions.Count; i++)
        {
            var grp = selectedGroups.FirstOrDefault(x => x.ColumnName == grid.GroupColumnDescriptions[i].ColumnName);
            if (grp != null) 
            {
                grp.Order = i;
            }
        }
    }
}
