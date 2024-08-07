﻿using FluentAssertions;
using TradingApp.Evaluator.Indicators;
using TradingApp.TestUtils;

namespace TradingApp.Evaluator.Test.Indicators;

public class VwapCustomTests : QuotesTestBase
{
    [Fact]
    public void Calculate_Success()
    {
        // Arrange
        var decimalPlace = 4;
        // Act
        var results = VwapIndicator.Calculate(quotes.ToList(), decimalPlace).ToList();

        // Assert
        results.Should().HaveCount(502);

        var r1 = results[14];
        r1.Value.Should().BeApproximately(214.76M, 0.0002m);

        var r2 = results[501];
        r2.Value.Should().BeApproximately(244.5633M, 0.0002m);
    }

    [Fact]
    public void Calculate_NoQuotes_Success()
    {
        // Arrange
        var decimalPlace = 4;
        // Act
        var r0 = VwapIndicator.Calculate(noquotes.ToList(), decimalPlace).ToList();
        var r1 = VwapIndicator.Calculate(onequote.ToList(), decimalPlace).ToList();
        // Assert
        r0.Should().BeEmpty();
        r1.Should().HaveCount(1);
    }
}
