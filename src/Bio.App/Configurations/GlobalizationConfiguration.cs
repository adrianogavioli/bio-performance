using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace Bio.App.Configurations
{
    public static class GlobalizationConfiguration
    {
        public static IApplicationBuilder UseGlobalizationConfiguration(this IApplicationBuilder application)
        {
            var defaultCulture = new CultureInfo("pt-BR");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };
            application.UseRequestLocalization(localizationOptions);

            return application;
        }
    }
}
