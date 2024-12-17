using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace MIPC_Web.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUsername = "iustin.moldoveanu04@gmail.com";
        private readonly string _smtpPassword = "123456789";

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("MIPC - Your luxury transport service!", _smtpUsername));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;

            var body = new TextPart("html")
            {
                Text = htmlMessage
            };

            message.Body = body;

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, false);
                await client.AuthenticateAsync(_smtpUsername, _smtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}