using System;

namespace RocketLanding
{
    public class LandingOptions
    {
        // Mandatory configuration.
        // Platform size can vary and should be configurable.
        public int PlatformStartX { get; set; }
        public int PlatformStartY { get; set; }

        // Optional configuration
        public int AreaSize { get; set; } = 100;
        public int PlatformSize { get; set; } = 10;

        public int UnderLimitSeparation { get; set; } = -1;
        public int UpperLimitSeparation { get; set; } = 1;
    }
}

