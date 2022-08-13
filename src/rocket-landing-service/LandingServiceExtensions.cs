using System;
using RocketLanding;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LandingServiceExtensions
    {
        public static IServiceCollection AddLandingServiceProvider( this IServiceCollection services, Action<LandingOptions> configure )
        {
            services.AddTransient<ILandingService, LandingService>()
                .Configure<LandingOptions>( configure );

            return services;
        }
    }
}

