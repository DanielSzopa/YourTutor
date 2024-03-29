﻿using FluentValidation;
using YourTutor.Application.Abstractions.Settings;

namespace YourTutor.Application.Settings.Email
{
    public sealed class EmailSettings : Settings<EmailSettings>, ISettings
    {
        public static string SectionName => "Email";

        public bool RegistrationNotificationIsEnabled { get; set; }

        public string From { get; set; }

        public EmailSettings()
        {
            RuleFor(x => x.From)
                .NotEmpty()
                .WithMessage("Email which is as sender email should be determined");

            RuleFor(x => x.From)
                .EmailAddress()
                .WithMessage("Invalid email");
        }
    }
}


