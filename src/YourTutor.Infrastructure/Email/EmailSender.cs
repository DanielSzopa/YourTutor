using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.SendGrid;
using Microsoft.Extensions.Options;
using YourTutor.Application.Abstractions;
using YourTutor.Application.Models.EmailBase;
using YourTutor.Infrastructure.Settings;

namespace YourTutor.Infrastructure.Email
{
    internal sealed class EmailSender : IEmailSender
    {
        private readonly SendGridSender _sendGridSender;

        public EmailSender(IOptions<SendGridSettings> sendGridSettings)
        {
            _sendGridSender = new SendGridSender(sendGridSettings.Value.ApiKey);
        }
        public async Task<bool> SendEmail(EmailBase email)
        {
            IFluentEmail fluentEmail = FluentEmail.Core.Email
                .From(email.From)
                .To(email.To)
                .Subject(email.Subject)
                .Body(email.Body)
                .Tag(email.Tag);

            SendResponse response = await _sendGridSender.SendAsync(fluentEmail);

            return response.Successful;
        }
    }
}


