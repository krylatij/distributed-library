using Azure;
using Azure.Communication.Email;
using DistributedLibrary.Services.Services;
using DistributedLibrary.Shared.Configuration;
using FakeItEasy;
using Microsoft.Extensions.Options;

namespace DistributedLibrary.UnitTests.Services
{
    public class NotificationServiceTests
    {
        [Fact]
        public async Task NotificationService_SendReservationMailAsync_Ok()
        {
            const string email = "john@doe.com";
            const string url = "/reservations";
            
            var options = Options.Create(new CommunicationServiceConfiguration
            {
                ConnectionString = "fake",
                Message = "name{0}url{1}",
                Sender = "fake@fake.com",
                Subject = "topic"
            });

            EmailMessage mail = null;

            var client = A.Fake<EmailClient>();
            A.CallTo(
                    () => client.SendAsync(A<WaitUntil>.Ignored, A<EmailMessage>.Ignored, CancellationToken.None))
                .Invokes((WaitUntil wait, EmailMessage x, CancellationToken token) => mail = x)
                .Returns(Task.FromResult(A.Fake<EmailSendOperation>())).Once();

            var service = new NotificationService(client, options);
            await service.SendReservationMailAsync(email, url);
           
            Assert.NotNull(mail);
            Assert.Equal(options.Value.Sender, mail.SenderAddress);
            Assert.Equal(options.Value.Subject, mail.Content.Subject);
            Assert.NotNull(mail.Content.PlainText);
            Assert.Contains(email, mail.Content.PlainText);
            Assert.Contains(url, mail.Content.PlainText);
        }
    }
}