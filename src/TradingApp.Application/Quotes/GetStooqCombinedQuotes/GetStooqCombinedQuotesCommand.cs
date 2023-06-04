using MediatR;
using TradingApp.Application.Models;

namespace TradingApp.Application.Quotes.GetStooqQuotes;

public record GetStooqCombinedQuotesCommand() : IRequest<ServiceResponse<GetStooqCombinedQuotesResponse>>;
