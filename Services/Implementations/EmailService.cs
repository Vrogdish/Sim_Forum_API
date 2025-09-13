using Sim_Forum.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Sim_Forum.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            // Récupère les variables d'environnement
            var smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST") ?? throw new InvalidOperationException("SMTP_HOST non défini.");
            var smtpPortStr = Environment.GetEnvironmentVariable("SMTP_PORT") ?? "587";
            if (!int.TryParse(smtpPortStr, out var smtpPort))
                smtpPort = 587;

            var smtpUser = Environment.GetEnvironmentVariable("SMTP_USER") ?? throw new InvalidOperationException("SMTP_USER non défini.");
            var smtpPass = Environment.GetEnvironmentVariable("SMTP_PASS") ?? throw new InvalidOperationException("SMTP_PASS non défini.");
            var fromEmail = Environment.GetEnvironmentVariable("SMTP_FROM") ?? smtpUser;

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            using var mailMessage = new MailMessage(fromEmail, to, subject, body)
            {
                IsBodyHtml = true // si tu veux du HTML
            };

            await client.SendMailAsync(mailMessage);
        }
    }
}