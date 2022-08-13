using System;

namespace RocketLanding
{
    public interface ILandingService
    {
        string RequestLanding( int landingX, int landingY, bool secure = false );
    }
}

