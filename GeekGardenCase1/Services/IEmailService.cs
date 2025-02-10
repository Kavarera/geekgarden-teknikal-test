using GeekGardenCase1.Models;

namespace GeekGardenCase1.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string penerima, string subject, string body);
        Task<List<Email>> GetEmailsAsync();
    }
}
