using MediatR;
using TradingApp.Application.Models;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Application.Quotes.GetStooqQuotesTest;

public record GetStooqQuotesTestCommand() : IRequest<ServiceResponse<IEnumerable<Quote>>>;
