using FluentValidation;
using YourTutor.Application.Settings.Base;

namespace YourTutor.Infrastructure.Settings
{
    public sealed class IdentitySettings : Settings<IdentitySettings>, ISettings
    {
        public static string SectionName => "Identity";

        public string CookieName { get; set; }
        public int ExpiresDays { get; set; }

        public IdentitySettings()
        {
           RuleFor(x => x.CookieName)
                .NotEmpty()
                .WithMessage($"{nameof(CookieName)} can not be empty");

            RuleFor(x => x.ExpiresDays)
               .GreaterThan(0)
               .WithMessage($"{nameof(ExpiresDays)} can not be less than 0");
        }       
    }
}


