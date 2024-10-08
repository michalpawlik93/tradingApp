﻿using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Mvc.Testing;
using NSubstitute;
using System.Net;
using System.Net.Http.Json;
using TestUtils.Fixtures;
using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes.Dto;
using TradingApp.Module.Quotes.Application.Features.GetCypherB;
using TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;
using TradingApp.Module.Quotes.Application.Features.TickerMetadata;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Constants;
using Xunit;

namespace TradingApp.TradingWebApi.Test.Modules;

public class QuotesModuleTests(WebApplicationFactory<Program> factory) : ApiTestBase<Program>(factory)
{
    [Fact]
    public async Task GetCypherB_ReturnsOk()
    {
        // Arrange
        Mediator
            .Send(Arg.Any<GetCypherBCommand>())
            .Returns(Result.Ok(new GetCypherBResponseDto([])));
        var request = new GetCypherBDto
        {
            Asset = MockAsset(),
            Settings = new SettingsDto()
            {
                WaveTrendSettings = new WaveTrendSettingsDto
                {
                    Overbought = WaveTrendSettingsConst.Overbought,
                    Oversold = WaveTrendSettingsConst.Oversold,
                    OverboughtLevel2 = WaveTrendSettingsConst.OverboughtLevel2,
                    OversoldLevel2 = WaveTrendSettingsConst.OversoldLevel2,
                    AverageLength = WaveTrendSettingsConst.AverageLength,
                    ChannelLength = WaveTrendSettingsConst.ChannelLength,
                    Enable = true,
                    EnableVwap = false,
                    MovingAverageLength = WaveTrendSettingsConst.MovingAverageLength
                },
                MfiSettings = new MfiSettingsDto()
                {
                    ChannelLength = MfiSettingsConst.ChannelLength,
                    ScaleFactor = MfiSettingsConst.ScaleFactor
                },
                SrsiSettings = MockSrsiSettings(),
            },
            TimeFrame = MockTimeFrame(),
        };
        // Act
        var result = await Client.PostAsJsonAsync("/quotes/cypherb", request);
        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetCombinedQuotes_ReturnsOk()
    {
        // Arrange
        Mediator
            .Send(Arg.Any<GetCombinedQuotesCommand>())
            .Returns(Result.Ok(new GetCombinedQuotesResponseDto([])));
        var request = new GetQuotesDtoRequest
        {
            Asset = MockAsset(),
            TimeFrame = MockTimeFrame(),
            Settings = new SettingsDto() { SrsiSettings = MockSrsiSettings() },
            Indicators = MockIndicators(),
        };
        // Act
        var result = await Client.PostAsJsonAsync("/quotes/combinedquotes", request);
        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetTickerMetadata_ReturnsOk()
    {
        // Arrange
        Mediator
            .Send(Arg.Any<GetTickerMetadataQuery>())
            .Returns(Result.Ok(Array.Empty<CryptocurrencyMetadata>()));
        // Act
        var result = await Client.GetAsync("/quotes/tingo/tickermetadata?AssetType=Cryptocurrency&AssetName=BTCUSD");
        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    private static IndicatorsDto[] MockIndicators() =>
    [
        new IndicatorsDto()
        {
            SideIndicators = [],
            TechnicalIndicator = nameof(TechnicalIndicator.Srsi)
        }
    ];
    private static AssetDto MockAsset() =>
        new()
        {
            Name = nameof(AssetName.BTCUSD),
            Type = nameof(AssetType.Cryptocurrency)
        };

    private static TimeFrameDto MockTimeFrame() =>
        new()
        {
            StartDate = "2023-07-09T10:30:00.000Z",
            EndDate = "2023-08-09T10:30:00.000Z",
            Granularity = nameof(Granularity.Daily)
        };

    private static SrsiSettingsDto MockSrsiSettings() =>
        new()
        {
            StochDSmooth = SRsiSettingsConst.StochDSmooth,
            StochKSmooth = SRsiSettingsConst.StochKSmooth,
            ChannelLength = SRsiSettingsConst.ChannelLength,
            Enabled = true,
            Overbought = SRsiSettingsConst.Overbought,
            Oversold = SRsiSettingsConst.Oversold
        };
}
