//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.11.2023
//
// An introduction to using Windows Image Acquisition (WIA) via C#
// https://www.cyotek.com/blog/an-introduction-to-using-windows-image-acquisition-wia-via-csharp
//-----------------------------------------------------------------------

using WIA;

namespace DocumentFlow.Scanner;

internal partial class ScannerPropertiesDialog : Form
{
    private enum ColumnType { Name, Id, Value, Min, Max, Step, Values }

    private readonly WIA.Properties properties;

    public ScannerPropertiesDialog(WIA.Properties properties)
    {
        InitializeComponent();

        this.properties = properties;
    }

    public static void ShowPropertiesDialog(WIA.Properties properties)
    {
        using ScannerPropertiesDialog dialog = new(properties);
        dialog.ShowDialog();
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);

        listView.BeginUpdate();

        for (int i = 0; i < properties.Count; i++)
        {
            Property property;
            ListViewItem item;
            string value;
            WiaPropertyType type;

            property = properties[i + 1];
            type = (WiaPropertyType)property.Type;

            value = property.GetValueString();

            item = new ListViewItem();

            for (int j = 0; j < listView.Columns.Count; j++)
            {
                item.SubItems.Add(string.Empty);
            }

            item.SubItems[(int)ColumnType.Name].Text = property.Name;
            item.SubItems[(int)ColumnType.Id].Text = property.PropertyID.ToString();
            item.SubItems[(int)ColumnType.Value].Text = value;

            if (type > WiaPropertyType.UnsupportedPropertyType && type <= WiaPropertyType.CurrencyPropertyType)
            {
                if (property.SubType == WiaSubType.RangeSubType)
                {
                    item.SubItems[(int)ColumnType.Min].Text = property.SubTypeMin.ToString();
                    item.SubItems[(int)ColumnType.Max].Text = property.SubTypeMax.ToString();
                    item.SubItems[(int)ColumnType.Step].Text = property.SubTypeStep.ToString();
                }
                else if (property.SubType == WiaSubType.ListSubType)
                {
                    item.SubItems[(int)ColumnType.Values].Text = property.SubTypeValues.ToSeparatedString();
                }
            }

            listView.Items.Add(item);
        }

        listView.EndUpdate();
    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
        Close();
    }
}