using TradingApp.TradingAdapter.Mappers;
using DomainQuote = TradingApp.TradingAdapter.Models.DomainQuote;
using Quote = Skender.Stock.Indicators.Quote;

namespace TradingApp.TradingAdapter.Test.Mappers;

public class SkenderQuoteMapperTests
{
    [Fact]
    public void MapToSkenderQuotes_ReturnsCorrectlyMappedQuotes()
    {
        // Arrange
        IEnumerable<DomainQuote> domainQuotes = new List<DomainQuote>
        {
          new DomainQuote(DateTime.Now, 10, 15, 9, 12, 1000),
          new DomainQuote(DateTime.Now, 11, 14, 10, 13, 1500)
        };

        // Act
        IEnumerable<Quote> skenderQuotes = domainQuotes.MapToSkenderQuotes();

        // Assert
        Assert.Equal(domainQuotes.Count(), skenderQuotes.Count());

        for (int i = 0; i < domainQuotes.Count(); i++)
        {
            Assert.Equal(domainQuotes.ElementAt(i).Open, skenderQuotes.ElementAt(i).Open);
            Assert.Equal(domainQuotes.ElementAt(i).Close, skenderQuotes.ElementAt(i).Close);
            Assert.Equal(domainQuotes.ElementAt(i).High, skenderQuotes.ElementAt(i).High);
            Assert.Equal(domainQuotes.ElementAt(i).Low, skenderQuotes.ElementAt(i).Low);
            Assert.Equal(domainQuotes.ElementAt(i).Date, skenderQuotes.ElementAt(i).Date);
            Assert.Equal(domainQuotes.ElementAt(i).Volume, skenderQuotes.ElementAt(i).Volume);
        }
    }
}
