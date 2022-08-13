using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using RocketLanding;

namespace RocketLanding.App
{
    public class Program
    {
        private static readonly object _lock = new object();

        private static ILandingService landingService;

        public static void Main( string[] args )
        {
            ConfigureServices();

            Thread atlas = new Thread( CheckLanding );
            Thread saturn = new Thread( CheckLanding );

            atlas.Start( true );
            saturn.Start( true );
        }

        public static void CheckLanding( object secure )
        {
            int landingX;
            int landingY;

            lock ( _lock )
            {
                Console.Write( "Landing coordinates > X: " );
                if ( !int.TryParse( Console.ReadLine(), out landingX ) )
                {
                    throw new FormatException("Input was not in a correct format");
                }

                Console.Write( "Landing coordinates > Y: " );
                if ( !int.TryParse( Console.ReadLine(), out landingY ) )
                {
                    throw new FormatException( "Input was not in a correct format" );
                }

                var response = landingService.RequestLanding( landingX, landingY, (bool)secure );

                Console.WriteLine( response );
            }
        }

        private static void ConfigureServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection()
            .AddLandingServiceProvider( options =>
            {
                options.PlatformStartX = 5;
                options.PlatformStartY = 5;
            });

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            landingService = serviceProvider.GetRequiredService<ILandingService>();
        }
    }
}
