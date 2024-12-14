using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public class EmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
        _smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("sendme5577@gmail.com", "emer cmam lthz lxbs"),
            EnableSsl = true,
        };
    }

    public async Task SendEmailAsync(string toEmail, string firstName, string lastName, string username, string pin, string verificationCode)
    {
        try
        {
            _logger.LogInformation("Preparing to send email to {ToEmail}", toEmail);

            var body = $@"
            <p>Hi {firstName} {lastName},</p>
            <p>Thank you for registering. Your login details are as follows:</p>
            <p>Username: {username}</p>
            <p>Email: {toEmail}</p>
            <p>PIN code: {pin}</p>
            <p><strong style='font-size: 24px;'>Verification code: {verificationCode}</strong></p>
            <p>Have a good day!!!</p>";

            var mailMessage = new MailMessage
            {
                From = new MailAddress("sendme5577@gmail.com"),
                Subject = "Email Verification",
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);

            _logger.LogInformation("Sending email to {ToEmail}", toEmail);
            await _smtpClient.SendMailAsync(mailMessage);
            _logger.LogInformation("Email sent successfully to {ToEmail}", toEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email to {ToEmail}", toEmail);
            throw;
        }
    }

}
