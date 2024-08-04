using FluentAssertions;
using FluentResults;
using NSubstitute;
using TradingApp.Core.Domain;
using TradingApp.Core.EventBus;
using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Constants;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.EvaluateCipherB
{
    public class EvaluateCipherBCommandHandlerTests
    {
        private readonly IEventBus _eventBus = Substitute.For<IEventBus>();
        private readonly ICypherBDecisionService _decisionService =
            Substitute.For<ICypherBDecisionService>();
        private readonly IEntityDataService<Decision> _decisionDataService = Substitute.For<
            IEntityDataService<Decision>
        >();
        private readonly EvaluateCipherBCommandHandler _sut;

        public EvaluateCipherBCommandHandlerTests()
        {
            _sut = new EvaluateCipherBCommandHandler(
                _eventBus,
                _decisionService,
                _decisionDataService
            );
        }

        [Fact]
        public async Task Handle_SuccessPath()
        {
            //Arrange

            var quotes = new List<Quote> { new(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m) };
            var command = new EvaluateCipherBCommand(
                quotes,
                Granularity.FiveMins,
                WaveTrendSettingsConst.WaveTrendSettingsDefault,
                MfiSettingsConst.MfiSettingsDefault,
                SRsiSettingsConst.SRsiSettingsDefault
            );
            //Act
            _decisionService
                .MakeDecision(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<CypherBDecisionSettings>())
                .Returns(
                    Result.Ok(
                        Decision.CreateNew(
                            new IndexOutcome("CypherB", 0.023M),
                            DateTime.UtcNow,
                            TradeAction.Buy,
                            MarketDirection.Bullish
                        )
                    )
                );
            var result = await _sut.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeEmpty();
            _decisionService
                .Received()
                .MakeDecision(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<CypherBDecisionSettings>());
            await _eventBus
                .Received()
                .Publish(Arg.Any<IAggregateRoot>(), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_DecisionServiceFailed_ReturnsFail()
        {
            //Arrange

            var quotes = new List<Quote> { new(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m) };
            var command = new EvaluateCipherBCommand(
                quotes,
                Granularity.FiveMins,
                WaveTrendSettingsConst.WaveTrendSettingsDefault,
                MfiSettingsConst.MfiSettingsDefault,
                SRsiSettingsConst.SRsiSettingsDefault
            );
            //Act
            _decisionService
                .MakeDecision(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<CypherBDecisionSettings>())
                .Returns(Result.Fail(""));
            var result = await _sut.Handle(command, CancellationToken.None);

            //Assert
            result.IsFailed.Should().BeTrue();
            _decisionService
                .Received()
                .MakeDecision(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<CypherBDecisionSettings>());
            await _eventBus
                .DidNotReceive()
                .Publish(Arg.Any<IAggregateRoot>(), Arg.Any<CancellationToken>());
        }
    }
}
