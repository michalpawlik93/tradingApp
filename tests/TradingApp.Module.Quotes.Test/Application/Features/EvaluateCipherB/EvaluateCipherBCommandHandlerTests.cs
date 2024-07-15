using FluentAssertions;
using NSubstitute;
using TradingApp.Core.Domain;
using TradingApp.Core.EventBus;
using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Aggregates;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.EvaluateCipherB
{
    public class EvaluateCipherBCommandHandlerTests
    {
        private readonly IEventBus _eventBus = Substitute.For<IEventBus>();
        private readonly ICypherBDecisionService _decisionService = Substitute.For<ICypherBDecisionService>();
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
        public async Task Handle_GetRSIReturnsEmptyList_EarlyReturn()
        {
            //Arrange
            var quotes = new List<CypherBQuote>()
            {
                new(
                    new Quote(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m),
                    new WaveTrendResult(1m, 2m, 3m, true, null),
                    new MfiResult(1m)
                )
            };
            var command = new EvaluateCipherBCommand(quotes, Granularity.FiveMins, WaveTrendSettingsConst.WaveTrendSettingsDefault);
            //Act
            var result = await _sut.Handle(command, CancellationToken.None);

            //Assert
            result.Errors.Should().BeEmpty();
            _decisionService.Received().MakeDecision(Arg.Any<CypherBDecisionRequest>());
            await _eventBus
                .Received()
                .Publish(Arg.Any<IAggregateRoot>(), Arg.Any<CancellationToken>());
        }
    }
}
