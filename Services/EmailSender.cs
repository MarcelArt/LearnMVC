using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Configuration;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;

namespace WebPWrecover.Services;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public EmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var SENDER_API_KEY = _config["Resend:ApiKey"];
        await Execute($"Bearer {SENDER_API_KEY}", subject, message, toEmail);
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
            return;
        }
        
        Debug.WriteLine(await response.Content.ReadAsStringAsync());
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