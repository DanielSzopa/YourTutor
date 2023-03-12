using FluentValidation;
using YourTutor.Application.Abstractions.Settings;

namespace YourTutor.Application.Settings
{
    public sealed class SendGridSettings : Settings<SendGridSettings>, ISettings
    {
        public static string SectionName => "SendGrid";

        public string ApiKey { get; set; }

        public SendGridSettings()
        {
            RuleFor(x => x.ApiKey)
                .NotEmpty()
                .WithMessage("ApiKey for sendGrid is required");
        }
    }
}


