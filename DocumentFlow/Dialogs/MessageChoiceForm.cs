//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.07.2022
//-----------------------------------------------------------------------

using Syncfusion.WinForms.Controls;

namespace DocumentFlow.Dialogs;

public partial class MessageChoiceForm<T> : Form
    where T : struct
{
    private T choice;

    protected MessageChoiceForm()
    {
        InitializeComponent();
    }

    public string HeaderText
    {
        get => labelMessage.Text;
        set => labelMessage.Text = value;
    }

    public bool CanBeCanceled { get; set; }

    public void AddChoice(T key, string text)
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
            Dock= DockStyle.Top,
            Height = 36,
            Text = text,
            Tag = key
        };

        button.Click += Button_Click;

        Controls.Add(button);
        button.BringToFront();
    }

    public static T ShowDialog(string title, string header, Dictionary<T, string> choices, T? cancelChoice)
    {
        var form = new MessageChoiceForm<T>()
        {
            Text = title,
            HeaderText = header,
            Height = 104 + 40 * choices.Count,
            CanBeCanceled = cancelChoice != null
        };

        foreach (var item in choices)
        {
            form.AddChoice(item.Key, item.Value);
        }

        return form.ShowDialog() switch
        {
            DialogResult.Cancel => cancelChoice!.Value,
            DialogResult.OK => form.choice,
            _ => throw new Exception("Непредвидимая ошибка."),
        };
    }

    private void Button_Click(object? sender, EventArgs e)
    {
        if (sender is SfButton button && button.Tag is T c)
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
