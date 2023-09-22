//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.03.2019
//-----------------------------------------------------------------------

using DocumentFlow.Tools;
using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Dialogs;

public delegate bool GroupOperation(string code, string name);

[Dialog]
public partial class GroupDialog : Form
{
    private GroupOperation? groupOperation;

    public GroupDialog()
    {
        InitializeComponent();
    }

    public bool Create(GroupOperation funcCreate)
    {
        groupOperation = funcCreate;
        textCode.Text = string.Empty;
        textName.Text = string.Empty;
        return ShowDialog() == DialogResult.OK;
    }

    public bool Edit(IDirectory directory, GroupOperation funcEdit)
    {
        groupOperation = funcEdit;
        textCode.Text = directory.Code;
        textName.Text = directory.ItemName;
        return ShowDialog() == DialogResult.OK;
    }

    private void DoCommit()
    {
        if (groupOperation == null)
        {
            return;
        }

        if (!groupOperation(textCode.Text, textName.Text))
        {
            DialogResult = DialogResult.None;
        }
    }

    private void ButtonOk_Click(object sender, EventArgs e) => DoCommit();
}
