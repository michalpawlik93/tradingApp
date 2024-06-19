using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace TestUtils.Fixtures
{
    public abstract class ApiTestBase<T> : IClassFixture<WebApplicationFactory<T>> where T : class
    {
        protected readonly HttpClient Client;
        protected readonly IMediator Mediator = Substitute.For<IMediator>();

        protected ApiTestBase(WebApplicationFactory<T> factory)
            => Client = factory
                .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
                {
                    services.AddSingleton(Mediator);
                }))
                .CreateClient();
    }
}