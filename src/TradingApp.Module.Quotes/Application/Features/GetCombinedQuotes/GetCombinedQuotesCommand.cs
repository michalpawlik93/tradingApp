﻿using FluentResults;
using MediatR;
using System.Collections.Immutable;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes.Dto;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy;
using TradingApp.Module.Quotes.Application.Mappers;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;

public record GetCombinedQuotesCommand(
    IImmutableSet<TechnicalIndicator> TechnicalIndicators,
    TimeFrame TimeFrame,
    Asset Asset,
    TradingStrategy TradingStrategy,
    SrsiSettings? SrsiSettings = null
) : IRequest<IResult<GetCombinedQuotesResponseDto>>;

public static class GetCombinedQuotesCommandExtensions
{
    public static GetCombinedQuotesCommand CreateCommand(this GetQuotesDtoRequest request) =>
        new(
            TechnicalIndicatorMapper.ToDomainModel(request.TechnicalIndicators),
            TimeFrameDtoMapper.ToDomainModel(request.TimeFrame),
            AssetDtoMapper.ToDomainModel(request.Asset),
            TradingStrategyMapper.Map(request.TradingStrategy),
            SRsiSettingsDtoMapper.ToDomainModel(request.SrsiSettings)
        );
}
