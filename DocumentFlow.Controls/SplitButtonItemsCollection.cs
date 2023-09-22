//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.06.2018
//-----------------------------------------------------------------------

using System.Collections;

namespace DocumentFlow.Controls;

public class SplitButtonItemsCollection : CollectionBase
{
    public ToolSplitItem? this[int index] => List[index] as ToolSplitItem;

    public SplitButtonItemsCollection(SplitButton _) { }

    public bool Contains(ToolSplitItem itemType) => List.Contains(itemType);

    public int Add(ToolSplitItem itemType)
    {
        if (string.IsNullOrEmpty(itemType.Name))
        {
            itemType.Name = GetUniqueName();
        }

        return List.Add(itemType);
    }

    public void Remove(ToolSplitItem itemType) => List.Remove(itemType);

    public void Insert(int index, ToolSplitItem itemType)
    {
        if (string.IsNullOrEmpty(itemType.Name))
        {
            itemType.Name = GetUniqueName();
        }

        List.Insert(index, itemType);
    }

    public int IndexOf(ToolSplitItem itemType) => List.IndexOf(itemType);

    protected override void OnInsert(int index, object? value)
    {
        if (value is ToolSplitItem item)
        {
            if (item.Name == string.Empty)
            {
                item.Name = GetUniqueName();
            }
        }

        base.OnInsert(index, value);
    }

    private string GetUniqueName()
    {
        int num = 1;
        while (Count != 0)
        {
            bool flag = true;
            int num2 = 0;
            while (num2 < Count)
            {
                ToolSplitItem? item = this[num2];
                if (item == null)
                {
                    continue;
                }

                if (!(item.Name == "SplitButtonItems" + num.ToString()))
                {
                    num2++;
                    continue;
                }

                flag = false;
                break;
            }

            if (flag)
            {
                break;
            }

            num++;
        }

        return "SplitButtonItems" + num.ToString();
    }
}
