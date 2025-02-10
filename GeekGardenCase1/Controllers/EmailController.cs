using GeekGardenCase1.Models.Request;
using GeekGardenCase1.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeekGardenCase1.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController:ControllerBase
    {
        private readonly IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest req)
        {
            try
            {
                await emailService.SendEmailAsync(req.Penerima, req.Subject, req.Body);
                return Ok(new { Message = "Email Terkirim" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Bad Request" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEmails()
        {
            try
            {
                var emails = await emailService.GetEmailsAsync();
                return Ok(emails);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Bad Request" });
            }
        }
    }
}
