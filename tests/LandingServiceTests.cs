using Microsoft.Extensions.DependencyInjection;

namespace RocketLanding.Tests;

public class LandingServiceTests
{
    private ILandingService? landingService;

    [Fact]
    public void RequestLandingSequenceTest()
    {
        ConfigureServices();

        var response = landingService?.RequestLanding( 5, 5, true );
        Assert.Equal( "ok for landing", response );

        response = landingService?.RequestLanding( 5, 6, true );
        Assert.Equal("clash", response);

        response = landingService?.RequestLanding( 6, 6, true );
        Assert.Equal( "clash", response );

        response = landingService?.RequestLanding( 10, 8, true );
        Assert.Equal( "ok for landing", response );
    }

    [Fact]
    public void PeekLandingTest()
    {
        ConfigureServices();

        var response = landingService?.RequestLanding( 5, 5 );
        Assert.Equal( "ok for landing", response );

        response = landingService?.RequestLanding( 5, 6, true );
        Assert.Equal( "ok for landing", response );

        response = landingService?.RequestLanding( 5, 5, true );
        Assert.Equal( "clash", response );
    }

    [Fact]
    public void OutOfPlatformTest()
    {
        ConfigureServices();

        var response = landingService?.RequestLanding( 2, 3 );
        Assert.Equal( "out of platform", response );
    }

    [Fact]
    public void OutOfAreaTest()
    {
        ConfigureServices();

        var exception = Assert.Throws<Exception>( () => landingService?.RequestLanding( 101, 90 ) );

        Assert.Equal( "Warning: Out of the landing area!", exception.Message );
    }

    private void ConfigureServices()
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
