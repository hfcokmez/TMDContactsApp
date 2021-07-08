using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Services
{
    public class EmailSendHelper : IEmailSendHelper
    {
        public bool SendEmailWithContext(string email, string htmlBody, string textBody)
        {
            try
            {
                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("TMD-ContactsApp", "brainwest400@gmail.com");
                message.From.Add(from);
                MailboxAddress to = new MailboxAddress("User", email);
                message.To.Add(to);
                message.Subject = "Şifre yenileme bilgilendirmesi.";

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<h1>Hello World!</h1>";
                bodyBuilder.TextBody = textBody;
                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("brainwest400@gmail.com", "samsungmonte");
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
