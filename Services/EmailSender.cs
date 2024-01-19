using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;

namespace WebPWrecover.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;

    public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                       ILogger<EmailSender> logger)
    {
        Options = optionsAccessor.Value;
        _logger = logger;
    }

    //public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        //if (string.IsNullOrEmpty(Options.SendGridKey))
        //{
        //    throw new Exception("Null SendGridKey");
        //}
        await Execute("Bearer re_T4CUsX2e_J8zDmAXw8z4b3AGxg5HZWy4m", subject, message, toEmail);
    }

    public async Task Execute(string apiKey, string subject, string message, string toEmail)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.resend.com/emails");
        request.Headers.Add("Authorization", apiKey);

        message = $@"From: LearnMVC, \nTo: {toEmail}, \nMessage: \n{message}";

        var emailContent = GenerateEmailContent("onboarding@resend.dev", "marcel.aritonang@gmail.com", subject, message);
        var content = new StringContent(emailContent, Encoding.UTF8, "application/json");
        
        request.Content = content;
        var response = await client.SendAsync(request);
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error:", ex);
            //_logger.LogInformation($"Failure Email to {toEmail}");
            return;
        }
        
        Debug.WriteLine(await response.Content.ReadAsStringAsync());
        //_logger.LogInformation($"Email to {toEmail} queued successfully!");

        //var client = new SendGridClient(apiKey);
        //var msg = new SendGridMessage()
        //{
        //    From = new EmailAddress("Joe@contoso.com", "Password Recovery"),
        //    Subject = subject,
        //    PlainTextContent = message,
        //    HtmlContent = message
        //};
        //msg.AddTo(new EmailAddress(toEmail));

        //// Disable click tracking.
        //// See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        //msg.SetClickTracking(false, false);
        //var response = await client.SendEmailAsync(msg);
        //_logger.LogInformation(response.IsSuccessStatusCode
        //                       ? $"Email to {toEmail} queued successfully!"
        //                       : $"Failure Email to {toEmail}");
    }

    static string GenerateEmailContent(string from, string to, string subject, string html)
    {
        return $@"{{
            ""from"": ""{from}"",
            ""to"": ""{to}"",
            ""subject"": ""{subject}"",
            ""html"": ""{html}""
        }}";
    }
}