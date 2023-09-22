//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.07.2022
//-----------------------------------------------------------------------

using Syncfusion.WinForms.Controls;

namespace DocumentFlow.Dialogs;

public partial class MessageChoiceDialog : Form
{
    private int choice = -1;

    protected MessageChoiceDialog()
    {
        InitializeComponent();
    }

    public string HeaderText
    {
        get => labelMessage.Text;
        set => labelMessage.Text = value;
    }

    public bool CanBeCanceled { get; set; }

    public static int ShowDialog(string title, string header, IList<string> choices, int cancelChoice = -1)
    {
        var form = new MessageChoiceDialog()
        {
            Text = title,
            HeaderText = header,
            Height = 104 + 40 * choices.Count,
            CanBeCanceled = cancelChoice != -1
        };

        for (int i = 0; i < choices.Count; i++)
        {
            form.AddChoice(i, choices[i]);
        }

        return form.ShowDialog() switch
        {
            DialogResult.Cancel => cancelChoice,
            DialogResult.OK => form.choice,
            _ => throw new Exception("Непредвидимая ошибка."),
        };
    }

    private void AddChoice(int index, string text)
    {
        var panel = new Panel()
        {
            Dock = DockStyle.Top,
            Height = 4
        };

        Controls.Add(panel);
        panel.BringToFront();

        var button = new SfButton()
        {
            Dock = DockStyle.Top,
            Height = 36,
            Text = text,
            Tag = index
        };

        button.Click += Button_Click;

        Controls.Add(button);
        button.BringToFront();
    }

    private void Button_Click(object? sender, EventArgs e)
    {
        if (sender is SfButton button && button.Tag is int c)
        {
            choice = c;
            DialogResult = DialogResult.OK;
        }
    }

    private void MessageChoiceForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyData == Keys.Escape && CanBeCanceled)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
