using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;

namespace RocketLanding
{
    public class LandingService : ILandingService
    {
        // in memory structure
        private readonly List<Landing> previousLandings = new List<Landing>();

        private readonly LandingOptions options = new LandingOptions();

        public LandingService( IOptions<LandingOptions> optionsAccessor )
        {
            options = optionsAccessor.Value;
        }

        // Query to see if it is on a good landing trajectory to land at any moment.
        // Use the secure parameter to secure landing (default value set to false).
        // This method returns the following messages: out of platform, clash or ok for landing.
        public string RequestLanding( int landingX, int landingY, bool secure = false )
        {
            if ( landingX > options.AreaSize || landingY > options.AreaSize )
            {
                throw new Exception("Warning: Out of the landing area!");
            }

            var isOutOfPlatform = IsOutOfPlatform( landingX, landingY );

            if ( isOutOfPlatform )
            {
                return LandingMessages.OutOfPlatform;
            }

            var hasClashing = previousLandings?.Any( previous => IsClashing( previous, landingX, landingY ) == true );

            if ( hasClashing == true )
            {
                return LandingMessages.Clash;
            }

            if ( secure )
            {
                previousLandings.Add( new Landing { X = landingX, Y = landingY } );
            }

            return LandingMessages.OkForLanding;
        }

        // Checks if the request landing is out of platform based on inferior
        // platform position X,Y and superior platform position X,Y
        private bool IsOutOfPlatform( int landingX, int landingY )
        {
            var landingAreaX = options.PlatformStartX + options.PlatformSize;
            var landingAreaY = options.PlatformStartY + options.PlatformSize;

            return landingX > landingAreaX
                || landingX < options.PlatformStartX
                || landingY > landingAreaY
                || landingY < options.PlatformStartY;
        }

        // Checks if the requested landing is possible.
        // One rocket can land on the same platform at the same time and the
        // rockets need to have at least X unit separation between their
        // landing positions.
        private bool IsClashing( Landing previous, int landingX, int landingY )
        {
            var marginX = previous.X - landingX;
            var marginY = previous.Y - landingY;

            return marginX >= options.UnderLimitSeparation
                    && marginX <= options.UpperLimitSeparation
                    && marginY >= options.UnderLimitSeparation
                    && marginY <= options.UpperLimitSeparation;
        }
    }
}

