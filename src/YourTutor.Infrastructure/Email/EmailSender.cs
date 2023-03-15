using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.SendGrid;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using YourTutor.Application.Abstractions.Email;
using YourTutor.Application.Models.EmailBase;
using YourTutor.Application.Settings;

namespace YourTutor.Infrastructure.Email
{
    internal sealed class EmailSender : IEmailSender
    {
        private readonly SendGridSender _sendGridSender;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IOptions<SendGridSettings> sendGridSettings, ILogger<EmailSender> logger)
        {
            _sendGridSender = new SendGridSender(sendGridSettings.Value.ApiKey);
            _logger = logger;
        }
        public async Task SendEmailAsync(EmailBase email)
        {
            IFluentEmail fluentEmail = FluentEmail.Core.Email
                .From(email.From)
                .To(email.To)
                .Subject(email.Subject)
                .Body(email.Body)
                .Tag(email.Tag);

            SendResponse response = await _sendGridSender.SendAsync(fluentEmail);

            if (!response.Successful)
                _logger.LogError("Problem with sending email by sendGrid, messageId: {@messageId}, error: {@error}", response.MessageId, JoinErrors(response.ErrorMessages));
        }

        private static string JoinErrors(IList<string> strings)
        {
            if(strings is null || strings.Count <= 0)
                return string.Empty;

            return string.Join("; ", strings);
        }
    }
}


