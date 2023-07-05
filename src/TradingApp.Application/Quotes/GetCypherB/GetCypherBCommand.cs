using MediatR;
using TradingApp.Application.Models;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Application.Quotes.GetCypherB;

public record GetCypherBCommand(TimeFrame TimeFrame, Asset Asset)
    : IRequest<ServiceResponse<GetCypherBResponse>>;

