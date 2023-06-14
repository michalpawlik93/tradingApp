using MediatR;
using TradingApp.Application.Models;
using TradingApp.TradingAdapter.Enums;

namespace TradingApp.Application.Quotes.GetStooqQuotes;

public record GetStooqCombinedQuotesCommand(HistoryType HistoryType)
    : IRequest<ServiceResponse<GetStooqCombinedQuotesResponse>>;
