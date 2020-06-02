//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.06.2018
// Time: 11:10
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls
{
    using System.Collections;

    public class SplitButtonItemsCollection : CollectionBase
    {
        private SplitButton Owner;

        public ToolSplitItem this[int index] => (ToolSplitItem)List[index];

        public SplitButtonItemsCollection(SplitButton sender) => Owner = sender;

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

        public ToolSplitItem FindByName(string name)
        {
            foreach (ToolSplitItem item in List)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }

            return null;
        }

        protected override void OnInsert(int index, object value)
        {
            if (string.IsNullOrEmpty(((ToolSplitItem)value).Name))
            {
                ((ToolSplitItem)value).Name = GetUniqueName();
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
                    if (!(this[num2].Name == "SplitButtonItems" + num.ToString()))
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
}