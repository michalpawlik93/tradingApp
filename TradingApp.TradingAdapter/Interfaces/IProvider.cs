namespace TradingApp.TradingAdapter.Interfaces;

public interface IProvider : ITradingAdapter
{
    public abstract string ProviderName { get; }
    public void CreateClient(HttpClient client);
}
