//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.12.2019
//-----------------------------------------------------------------------

namespace DocumentFlow.Dialogs;

using DocumentFlow.Data;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Data.Models;
using DocumentFlow.Dialogs.Interfaces;
using DocumentFlow.Tools;

using MailKit.Net.Smtp;
using MailKit.Security;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp.Notifications;

using MimeKit;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class EmailSendDialog : Form, IEmailSendDialog
{
    private struct Attachment
    {
        public string FileName { get; set; }

        public override readonly string ToString() => Path.GetFileName(FileName);
    }

    private class EmailAddress
    {
        public EmailAddress(string name, string email) => (Name, Email) = (name, email);
        public string Name { get; }
        public string Email { get; }
        public override string ToString() => $"{Name} <{Email}>";
    }

    private readonly IServiceProvider services;
    private readonly BindingList<Attachment> attachments = new();
    private Guid? id;

    public EmailSendDialog(IServiceProvider services)
    {
        InitializeComponent();

        this.services = services;
    }

    bool IEmailSendDialog.Send(Guid? documentId, string title, string file)
    {
        var orgRepo = services.GetRequiredService<IOrganizationRepository>();
        var orgs = orgRepo!.GetListExisting(
            callback: q => q
                .WhereNotNull("item_name")
                .WhereNotNull("email"))
            .Select(x => new EmailAddress(x.ItemName!, x.Email!));

        var ourEmpRepo = services.GetRequiredService<IOurEmployeeRepository>();
        var ourEmps = ourEmpRepo!.GetListExisting(
            callback: q => q
                .WhereNotNull("item_name")
                .WhereNotNull("email"))
            .Select(x => new EmailAddress(x.ItemName!, x.Email!));

        var from = orgs.Concat(ourEmps);

        var empRepo = services.GetRequiredService<IEmployeeRepository>();
        var to = empRepo!.GetListUserDefined(
            callback: q => q
                .WhereFalse("employee.deleted")
                .WhereNotNull("employee.item_name")
                .WhereNotNull("employee.email"))
            .Select(x => new EmailAddress($"{x.ItemName} ({x.OwnerName})", x.Email!));

        comboFrom.DropDownControl.ShowButtons = true;
        comboTo.DropDownControl.ShowButtons = true;

        comboFrom.DataSource = from;
        comboTo.DataSource = to;
        textSubject.Text = title;

        attachments.Add(new Attachment { FileName = file });
        listFiles.DataSource = attachments;

        id = documentId;

        return ShowDialog() == DialogResult.OK;
    }

    private void ControlSizeChanged(object sender, EventArgs e)
    {
        if (sender is Control c && c.Parent != null)
        {
            int d = c.Height - 28;
            c.Parent.Height = 34 + d;
        }
    }

    private async Task SendEmailAsync(Email email, EmailAddress emailFrom, IEnumerable<EmailAddress> emailTo)
    {
        using var client = new SmtpClient();
        EmailLog log = new()
        {
            EmailId = email.Id,
            ToAddress = string.Join(";", emailTo.Select(x => x.Email)),
            DocumentId = id
        };

        // SslHandshakeException: An error occurred while attempting to establish an SSL or TLS connection
        // https://stackoverflow.com/questions/59026301/sslhandshakeexception-an-error-occurred-while-attempting-to-establish-an-ssl-or
        //
        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
        
        await client.ConnectAsync(email.MailHost, email.MailPort, SecureSocketOptions.Auto);

        try
        {
            await client.AuthenticateAsync(email.Address, email.UserPassword);
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return;
        }

        MimeMessage message = new();
        message.From.Add(new MailboxAddress(emailFrom.Name, emailFrom.Email));
        message.Bcc.Add(new MailboxAddress(emailFrom.Name, emailFrom.Email));
        foreach (EmailAddress e in emailTo)
        {
            message.To.Add(new MailboxAddress(e.Name, e.Email));
        }

        message.Subject = textSubject.Text;

        BodyBuilder builder = new();
        if (!string.IsNullOrEmpty(email.SignaturePlain))
        {
            builder.TextBody = string.Format("{0}\n--\n{1}", textMessage.Text, email.SignaturePlain);
        }

        if (!string.IsNullOrEmpty(email.SignatureHtml))
        {
            builder.HtmlBody = string.Format("<p>{0}</p><br/>{1}", textMessage.Text, email.SignatureHtml);
        }

        if (attachments.Any())
        {
            foreach (Attachment attachment in attachments)
            {
                builder.Attachments.Add(attachment.FileName);
            }
        }

        message.Body = builder.ToMessageBody();

        await client.SendAsync(message);
        log.DateTimeSending = DateTime.Now;

        client.Disconnect(true);

        var logs = services.GetRequiredService<IEmailLogRepository>();
        logs?.Add(log);

        new ToastContentBuilder()
            .AddArgument("action", "viewConversation")
            .AddArgument("conversationId", 9813)
            .AddText("Документ отправлен")
            .AddText(textSubject.Text)
            .AddText($"Получатели: {string.Join(", ", emailTo.Select(x => x.Name))}")
            .Show(toast => 
            {
                toast.ExpirationTime = DateTime.Now.AddMinutes(1);
            });
    }

    private void ButtonSend_Click(object sender, EventArgs e)
    {
        if (!comboFrom.CheckedItems.Any())
        {
            MessageBox.Show("Не указан адрес отправителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
            return;
        }

        if (comboFrom.CheckedItems[0] is EmailAddress from)
        {
            var repo = services.GetRequiredService<IEmailRepository>();
            var email = repo!.Get(from.Email);
            if (email == null)
            {
                MessageBox.Show($"Для адреса <{from.Email}> не указаны параметры SMTP-сревера", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            IEnumerable<EmailAddress> to = comboTo.CheckedItems.Cast<EmailAddress>();
            if (!to.Any())
            {
                MessageBox.Show("Не указан адрес получателя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            SendEmailAsync(email, from, to).GetAwaiter();
        }
    }

    private void ButtonAdd_Click(object sender, EventArgs e)
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            foreach (string file in openFileDialog1.FileNames)
            {
                attachments.Add(new Attachment { FileName = file });
            }
        }
    }

    private void ButtonDelete_Click(object sender, EventArgs e)
    {
        if (listFiles.SelectedItem is Attachment file)
        {
            attachments.Remove(file);
        }
    }
}
