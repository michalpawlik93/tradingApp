using MediatR;
using TradingApp.Application.Models;
using TradingApp.TradingAdapter.Enums;

namespace TradingApp.Application.Quotes.GetCypherB;

public record GetCypherBCommand(HistoryType Granularity, DateTime? StartDate, DateTime? EndDate)
    : IRequest<ServiceResponse<GetCypherBResponse>>;

