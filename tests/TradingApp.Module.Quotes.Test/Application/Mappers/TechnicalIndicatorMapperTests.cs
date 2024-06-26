﻿using FluentAssertions;
using TradingApp.Module.Quotes.Application.Mappers;
using TradingApp.Module.Quotes.Contract.Constants;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Mappers;

public class TechnicalIndicatorMapperTests
{
    [Fact]
    public void ToDomainModel_Should_Return_Correct_Model()
    {
        // Arrange
        var technicalIndicators = new string[] { nameof(TechnicalIndicator.Srsi), nameof(TechnicalIndicator.Srsi), "default" };
        // Act
        var result = TechnicalIndicatorMapper.ToDomainModel(technicalIndicators);

        // Assert
        result.Should().Contain(TechnicalIndicator.Rsi);
        result.Should().Contain(TechnicalIndicator.Srsi);
        result.Should().HaveCount(2);
    }
}

