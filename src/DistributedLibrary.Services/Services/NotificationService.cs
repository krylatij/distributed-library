using Azure;
using Azure.Communication.Email;
using DistributedLibrary.Shared.Configuration;
using Microsoft.Extensions.Options;

namespace DistributedLibrary.Services.Services;

public class NotificationService
{
    private readonly EmailClient _emailClient;
    private readonly IOptions<CommunicationServiceConfiguration> _configuration;

    public NotificationService(EmailClient emailClient, IOptions<CommunicationServiceConfiguration> configuration)
    {
        _emailClient = emailClient;
        _configuration = configuration;
    }

    public async Task SendReservationMailAsync(string emailTo, string linkToFollow)
    {
        var config = _configuration.Value;

        var text = string.Format(config.Message, emailTo, linkToFollow);

        var mail = new EmailMessage(config.Sender, emailTo, new EmailContent(config.Subject){ PlainText = text});

        await _emailClient.SendAsync(WaitUntil.Started, mail);
        
    }
}