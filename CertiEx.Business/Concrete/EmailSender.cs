using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CertiEx.Business.Concrete;

public class EmailSender : IEmailSender
{
    public string SendGridSecret { get; set; }
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(IConfiguration config, ILogger<EmailSender> logger)
    {
        _logger = logger;
        SendGridSecret = config.GetValue<string>("SendGridKey");
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if (string.IsNullOrEmpty(SendGridSecret))
        {
            throw new Exception("Null SendGridKey!!!");
        }
        await Execute(SendGridSecret, email, subject, htmlMessage);
    }

    public async Task Execute(string apiKey, string toEmail, string subject, string message)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage
        {
            From = new EmailAddress("no-reply@evildor.com", "Evildor - Trusted Leading Marketplace"),
            Subject = subject,
            //PlainTextContent = message,
            //HtmlContent = message,
            TemplateId = "d-6551dd0572b74096b662a9057c087155"
        };
        msg.AddTo(new EmailAddress(toEmail));
        msg.SetTemplateData(new
        {
            email = message
        });

        // Disable click tracking.
        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        msg.SetClickTracking(false, false);

        var response = await client.SendEmailAsync(msg);
        _logger.LogWarning(response.IsSuccessStatusCode
            ? $"Email to {toEmail} queued successfully!"
            : $"Failure Email to {toEmail}");
    }
}
