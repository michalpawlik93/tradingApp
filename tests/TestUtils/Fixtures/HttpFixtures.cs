using System.Net;

namespace TestUtils.Fixtures;

public abstract class MockHttpMessageHandlerBase : HttpMessageHandler
{
    protected abstract string HttpContent { get; }
    protected abstract HttpStatusCode StatusCode { get; }
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    ) => Task.FromResult(MockSend(request, cancellationToken));

    protected override HttpResponseMessage Send(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    ) => MockSend(request, cancellationToken);

    public virtual HttpResponseMessage MockSend(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    ) => new() { Content = new StringContent(HttpContent), StatusCode = StatusCode };
}


