//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.12.2019
//
// Версия 2022.9.3
//  - добавлена поддержка всплывающих уведомлений об отправке писем
//  - при ошибке аутетификации выдается соответствующее сообщение
//  - исправлена ошибка формирования имён получателей
// Версия 2022.11.16
//  - мелкие исправления
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Dialogs;

using DocumentFlow.Data;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Employees;

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

public partial class SelectEmailForm : Form
{
    private struct Attachment
    {
        public string FileName { get; set; }

        public override string ToString() => Path.GetFileName(FileName);
    }

    private class EmailAddress
    {
        public EmailAddress(string name, string email) => (Name, Email) = (name, email);
        public string Name { get; }
        public string Email { get; }
        public override string ToString() => $"{Name} <{Email}>";
    }

    private readonly BindingList<Attachment> attachments = new();
    private readonly Guid? id;

    private SelectEmailForm(Guid? documentId, IEnumerable<EmailAddress> from, IEnumerable<EmailAddress> to, string subject, string file)
    {
        InitializeComponent();

        comboFrom.DropDownControl.ShowButtons = true;
        comboTo.DropDownControl.ShowButtons = true;

        comboFrom.DataSource = from;
        comboTo.DataSource = to;
        textSubject.Text = subject;

        attachments.Add(new Attachment { FileName = file });
        listFiles.DataSource = attachments;

        id = documentId;
    }

    public static bool ShowWindow(Guid? documentId, string title, string file)
    {
        var orgRepo = Services.Provider.GetService<IOrganizationRepository>();
        var orgs = orgRepo!.GetAllValid(
            callback: q => q
                .WhereNotNull("item_name")
                .WhereNotNull("email"))
            .Select(x => new EmailAddress(x.item_name!, x.email!));

        var ourEmpRepo = Services.Provider.GetService<IOurEmployeeRepository>();
        var ourEmps = ourEmpRepo!.GetAllValid(
            callback: q => q
                .WhereNotNull("item_name")
                .WhereNotNull("email"))
            .Select(x => new EmailAddress(x.item_name!, x.email!));

        var from = orgs.Concat(ourEmps);

        var empRepo = Services.Provider.GetService<IEmployeeRepository>();
        var to = empRepo!.GetAllDefault(
            callback: q => q
                .WhereFalse("employee.deleted")
                .WhereNotNull("employee.item_name")
                .WhereNotNull("employee.email"))
            .Select(x => new EmailAddress($"{x.item_name} ({x.owner_name})", x.email!));

        SelectEmailForm window = new(documentId, from, to, title, file);
        return window.ShowDialog() == DialogResult.OK;
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
            email_id = email.Id,
            to_address = string.Join(";", emailTo.Select(x => x.Email)),
            document_id = id
        };

        // SslHandshakeException: An error occurred while attempting to establish an SSL or TLS connection
        // https://stackoverflow.com/questions/59026301/sslhandshakeexception-an-error-occurred-while-attempting-to-establish-an-ssl-or
        //
        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
        
        await client.ConnectAsync(email.mail_host, email.mail_port, SecureSocketOptions.Auto);

        try
        {
            await client.AuthenticateAsync(email.address, email.user_password);
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
        if (!string.IsNullOrEmpty(email.signature_plain))
        {
            builder.TextBody = string.Format("{0}\n--\n{1}", textMessage.Text, email.signature_plain);
        }

        if (!string.IsNullOrEmpty(email.signature_html))
        {
            builder.HtmlBody = string.Format("<p>{0}</p><br/>{1}", textMessage.Text, email.signature_html);
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
        log.date_time_sending = DateTime.Now;

        client.Disconnect(true);

        var logs = Services.Provider.GetService<IEmailLogRepository>();
        if (logs != null)
        {
            logs.Add(log);
        }

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
            var repo = Services.Provider.GetService<IEmailRepository>();
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
