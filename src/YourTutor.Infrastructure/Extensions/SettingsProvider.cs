﻿using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using YourTutor.Infrastructure.Settings.Base;

namespace YourTutor.Infrastructure.Extensions
{
    public static class SettingsProvider
    {
        internal static void RegisterSettings<TSettings>(this IServiceCollection services, IConfiguration configuration)
            where TSettings : Settings<TSettings>, ISettings, new()
        {
            services.AddOptions<TSettings>()
                .Bind(configuration.GetSection(TSettings.SectionName))
                .ValidateOnStart();
            services.AddSingleton<IValidateOptions<TSettings>, TSettings>();
        }

        public static TSettings GetSettings<TSettings>(this IServiceCollection services, IConfiguration configuration)
            where TSettings : Settings<TSettings>, ISettings, new()
        {
            TSettings settings = new();
            configuration.GetSection(TSettings.SectionName).Bind(settings);
            settings.ValidateAndThrow(settings);
            return settings;
        }
    }
}


