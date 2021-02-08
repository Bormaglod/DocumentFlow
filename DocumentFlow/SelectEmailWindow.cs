//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.12.2019
// Time: 0:25
//-----------------------------------------------------------------------

namespace DocumentFlow
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Dapper;
    using MimeKit;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using Syncfusion.Windows.Forms;
    using DocumentFlow.Data;
    using DocumentFlow.Data.Entities;

    public partial class SelectEmailWindow : MetroForm
    {
        private struct Attachment
        {
            public string FileName { get; set; }

            public override string ToString()
            {
                return Path.GetFileName(FileName);
            }
        }

        private class EmailAddress
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public override string ToString() => $"{Name} <{Email}>";
        }

        private readonly BindingList<Attachment> attachments = new BindingList<Attachment>();
        private Guid id;

        private SelectEmailWindow(Guid documentId, IEnumerable<EmailAddress> from, IEnumerable<EmailAddress> to, string subject, string file)
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

        public static bool ShowWindow(Guid documentId, string title, string file)
        {
            IEnumerable<EmailAddress> from;
            IEnumerable<EmailAddress> to;
            using (var conn = Db.OpenConnection())
            {
                var orgs = conn.Query<EmailAddress>("select name, email from organization");
                var emps = conn.Query<EmailAddress>("select p.name, e.email from employee e join organization o on (o.id = e.owner_id) join person p on (p.id = e.person_id) where e.email is not null");

                from = orgs.Concat(emps);

                to = conn.Query<EmailAddress>("select p.name || ' (' || c.short_name || ')' as name, e.email from employee e join contractor c on (e.owner_id = c.id) join person p on (e.person_id = p.id) where e.email is not null");
            }

            SelectEmailWindow window = new SelectEmailWindow(documentId, from, to, title, file);
            return window.ShowDialog() == DialogResult.OK;
        }

        private void ControlSizeChanged(object sender, EventArgs e)
        {
            if (sender is Control c)
            {
                int d = c.Height - 28;
                c.Parent.Height = 34 + d;
            }
        }

        private async Task SendEmailAsync(Email email, EmailAddress emailFrom, IEnumerable<EmailAddress> emailTo)
        {
            using (var client = new SmtpClient())
            {
                EmailLog log = new EmailLog
                {
                    email_id = email.id,
                    to_address = string.Join(";", emailTo.Select(x => x.Email)),
                    document_id = id
                };

                // SslHandshakeException: An error occurred while attempting to establish an SSL or TLS connection
                // https://stackoverflow.com/questions/59026301/sslhandshakeexception-an-error-occurred-while-attempting-to-establish-an-ssl-or
                //
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(email.host, email.port, SecureSocketOptions.Auto);
                await client.AuthenticateAsync(email.address, email.password);

                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress(emailFrom.Name, emailFrom.Email));
                message.Bcc.Add(new MailboxAddress(emailFrom.Name, emailFrom.Email));
                foreach (EmailAddress e in emailTo)
                {
                    message.To.Add(new MailboxAddress(e.Name, e.Email));
                }

                message.Subject = textSubject.Text;

                BodyBuilder builder = new BodyBuilder();
                if (!string.IsNullOrEmpty(email.signature_plain))
                {
                    builder.TextBody = string.Format("\n--\n{0}", email.signature_plain);
                }

                if (!string.IsNullOrEmpty(email.signature_html))
                {
                    builder.HtmlBody = string.Format("<br/>{0}", email.signature_html);
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

                using (var conn = Db.OpenConnection())
                {
                    using (var transaction = conn.BeginTransaction())
                    {
                        await conn.ExecuteAsync("insert into email_log (email_id, start_time_sending, end_time_sending, to_address, document_id) values (:email_id, :start_time_sending, :end_time_sending, :to_address, :document_id)", log, transaction);
                        transaction.Commit();
                    }
                }
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (!comboFrom.CheckedItems.Any())
            {
                MessageBox.Show("Не указан адрес отправителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            EmailAddress from = comboFrom.CheckedItems[0] as EmailAddress;

            using (var conn = Db.OpenConnection())
            {
                var email = conn.QuerySingleOrDefault<Email>("select * from email where address = :email", new { email = from.Email });
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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in openFileDialog1.FileNames)
                {
                    attachments.Add(new Attachment { FileName = file });
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listFiles.SelectedItem is Attachment file)
            {
                attachments.Remove(file);
            }
        }
    }
}
