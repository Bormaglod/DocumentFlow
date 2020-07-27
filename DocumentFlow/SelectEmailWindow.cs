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
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using MimeKit;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using NHibernate;
    using NHibernate.Transform;
    using Syncfusion.Windows.Forms;
    using DocumentFlow.Data.Core;
    using DocumentFlow.Data.Entities;

    public partial class SelectEmailWindow : MetroForm
    {
        private ISession session;
        private IEnumerable<string> attachments = null;
        private class EmailAddress
        {
            public string Name { get; set; }
            public string Email { get; set; }

            public override string ToString()
            {
                return $"{Name} <{Email}>";
            }
        }

        public SelectEmailWindow()
        {
            InitializeComponent();

            comboFrom.DropDownControl.ShowButtons = true;
            comboTo.DropDownControl.ShowButtons = true;
        }

        public bool ShowWindow(string title)
        {
            session = Db.OpenSession();

            using (var transaction = session.BeginTransaction())
            {
                IList<EmailAddress> orgs = session.CreateSQLQuery("select name, email from organization")
                    .SetResultTransformer(Transformers.AliasToBean<EmailAddress>())
                    .List<EmailAddress>();

                IEnumerable<EmailAddress> emps = session.CreateSQLQuery("select p.name, e.email from employee e join organization o on (o.id = e.owner_id) join person p on (p.id = e.person_id) where e.email is not null")
                    .SetResultTransformer(Transformers.AliasToBean<EmailAddress>())
                    .List<EmailAddress>();

                comboFrom.DataSource = orgs.Concat(emps);

                IEnumerable<EmailAddress> contractors = session.CreateSQLQuery("select p.name || ' (' || c.short_name || ')' as name, e.email from employee e join contractor c on (e.owner_id = c.id) join person p on (e.person_id = p.id) where e.email is not null")
                    .SetResultTransformer(Transformers.AliasToBean<EmailAddress>())
                    .List<EmailAddress>();

                comboTo.DataSource = contractors;
            }

            textSubject.Text = title;
            string attachment = title.Replace('/', '-').Replace('\\', '-') + ".pdf";
            if (File.Exists(Path.GetTempPath() + attachment))
                attachments = new string[] { attachment };

            listFiles.DataSource = attachments;
            
            return ShowDialog() == DialogResult.OK;
        }

        private void ControlSizeChanged(object sender, EventArgs e)
        {
            if (sender is Control c)
            {
                int d = c.Height - 28;
                c.Parent.Height = 34 + d;
            }
        }

        private static async Task SendEmailAsync(Email email, EmailAddress emailFrom, IEnumerable<EmailAddress> emailTo, string subject, IEnumerable<string> attachments)
        {
            using (var client = new SmtpClient())
            {
                // SslHandshakeException: An error occurred while attempting to establish an SSL or TLS connection
                // https://stackoverflow.com/questions/59026301/sslhandshakeexception-an-error-occurred-while-attempting-to-establish-an-ssl-or
                //
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(email.Host, email.Port, SecureSocketOptions.Auto);
                client.Authenticate(email.Address, email.Password);

                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress(emailFrom.Name, emailFrom.Email));
                foreach (EmailAddress e in emailTo)
                {
                    message.To.Add(new MailboxAddress(e.Name, e.Email));
                }

                message.Subject = subject;

                BodyBuilder builder = new BodyBuilder();
                if (!string.IsNullOrEmpty(email.SignaturePlain))
                {
                    builder.TextBody = string.Format("\n--\n{0}", email.SignaturePlain);
                }

                if (!string.IsNullOrEmpty(email.SignatureHtml))
                {
                    builder.HtmlBody = string.Format("<br/>{0}", email.SignatureHtml);
                }

                if (attachments != null &&  attachments.Any())
                {
                    foreach (string fileName in attachments)
                    {
                        builder.Attachments.Add(Path.GetTempPath() + fileName);
                    }
                }

                message.Body = builder.ToMessageBody();

                await client.SendAsync(message);

                client.Disconnect(true);
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

            Email email = session.QueryOver<Email>().Where(x => x.Address == from.Email).SingleOrDefault();
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

            SendEmailAsync(email, from, to, textSubject.Text, attachments).GetAwaiter();
        }

        private void SelectEmailWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            session.Close();
        }
    }
}
