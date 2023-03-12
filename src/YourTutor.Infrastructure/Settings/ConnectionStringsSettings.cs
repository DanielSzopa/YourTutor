using FluentValidation;
using YourTutor.Application.Abstractions.Settings;

namespace YourTutor.Application.Settings
{
    public sealed class ConnectionStringsSettings : Settings<ConnectionStringsSettings>, ISettings
    {
        public string DefaultConnectionString { get; set; }
        public static string SectionName => "ConnectionStrings";

        public ConnectionStringsSettings()
        {
            RuleFor(x => x.DefaultConnectionString)
                .NotEmpty()
                .WithMessage($"{nameof(DefaultConnectionString)} can not be empty");
        }
    }
}


