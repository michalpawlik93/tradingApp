using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Features.Srsi.Dto;

public record GetSrsiResponseDto(IEnumerable<SrsiDto> Srsi);

public record SrsiDto(Quote Ohlc, SrsiSignal SrsiSignal);
