namespace ZRES.IntegrationTest.Fixtures
{
    using ZRES.Repositories;
    using ZRES.Services;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;

    public class TestStartup : Startup
    {
        private readonly Mock<ICarRepository> carRepositoryMock;
        private readonly Mock<IClockService> clockServiceMock;

        public TestStartup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
            : base(configuration, webHostEnvironment)
        {
            this.carRepositoryMock = new Mock<ICarRepository>(MockBehavior.Strict);
            this.clockServiceMock = new Mock<IClockService>(MockBehavior.Strict);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton(this.carRepositoryMock)
                .AddSingleton(this.clockServiceMock);

            base.ConfigureServices(services);

            services
                .AddSingleton(this.carRepositoryMock.Object)
                .AddSingleton(this.clockServiceMock.Object);
        }
    }
}
