using FluentAssertions;
using NSubstitute;
using TradingApp.Core.Domain;
using TradingApp.Core.EventBus;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Application.Services;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.ValueObjects;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Quotes.Application.EvaluateCipherB
{
    public class EvaluateCipherBCommandHandlerTests
    {
        private readonly IEventBus _eventBus = Substitute.For<IEventBus>();
        private readonly IDecisionService _decisionService = Substitute.For<IDecisionService>();
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
            var command = new EvaluateCipherBCommand(quotes);
            //Act
            var result = await _sut.Handle(command, CancellationToken.None);

            //Assert
            result.Errors.Should().BeEmpty();
            _decisionService.Received().MakeDecision(Arg.Any<IndexOutcome>());
            await _eventBus
                .Received()
                .Publish(Arg.Any<IAggregateRoot>(), Arg.Any<CancellationToken>());
        }
    }
}
