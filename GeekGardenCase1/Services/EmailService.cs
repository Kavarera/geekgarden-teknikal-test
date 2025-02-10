using GeekGardenCase1.Data;
using GeekGardenCase1.Models;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GeekGardenCase1.Services
{
    public class EmailService : IEmailService
    {

        private readonly SmtpSetting smtpSettings;
        private readonly EmailDbContext context;
        private readonly ILogger<EmailService> logger;

        public EmailService(IOptions<SmtpSetting> smtp, EmailDbContext dbContext, ILogger<EmailService> log)
        {
            smtpSettings = smtp.Value;
            context = dbContext;
            this.logger = log;
        }

        public async Task<List<Email>> GetEmailsAsync()
        {
            return await context.Emails.ToListAsync();
        }

        public async Task SendEmailAsync(string penerima, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Sender ", smtpSettings.Username));
                message.To.Add(new MailboxAddress("Receiver", penerima));
                message.Subject = subject;
                var builderBody = new BodyBuilder { HtmlBody = body };
                message.Body = builderBody.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(smtpSettings.Host, smtpSettings.Port,
                    MailKit.Security.SecureSocketOptions.StartTls);

                await client.AuthenticateAsync(smtpSettings.Username, smtpSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);


                var email = new Email
                {
                    Recipient = penerima,
                    Subject = subject,
                    Body = body,
                    SentAt = DateTime.UtcNow
                };

                context.Emails.Add(email);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error occured:\n{ex.Message}");
                throw;
            }
        }
    }
}
