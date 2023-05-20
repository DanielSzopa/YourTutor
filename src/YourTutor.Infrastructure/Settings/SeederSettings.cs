using FluentValidation;
using YourTutor.Application.Abstractions.Settings;

namespace YourTutor.Infrastructure.Settings;

public class SeederSettings : Settings<SeederSettings>, ISettings
{
    public static string SectionName => "Seeder";

    public int Quantity { get; set; }
    public string Password { get; set; }
    public string Locale { get; set; }

    private readonly string[] _locales = new[] { "pl", "en" }; 

    public SeederSettings()
    {
        RuleFor(s => s.Quantity)
            .GreaterThan(-1)
            .WithMessage($"{nameof(Quantity)} should be 0 or greather than 0");

        RuleFor(s => s.Password)
            .Length(8, 50)
            .WithMessage("Must be between 8 and 50 characters");

        RuleFor(s => s.Locale)
            .Custom((locale, context) =>
            {
                var result = _locales.Contains(locale.ToLower());
                if(!result)
                    context.AddFailure(nameof(Locale), $"Locale: {locale} does not contain in available locales: {String.Join(';', _locales)}");
            });
    }
}


