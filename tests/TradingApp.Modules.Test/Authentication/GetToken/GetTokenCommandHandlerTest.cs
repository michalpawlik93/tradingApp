using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using TradingApp.Modules.Abstraction;
using TradingApp.Modules.Authentication.GetToken;
using TradingApp.Modules.Models;

namespace TradingApp.Modules.Test.Authentication.GetToken
{
    [TestFixture]
    public class GetTokenCommandHandlerTest
    {
        private readonly Mock<IJwtProvider> JwtProviderMock = new();
        private readonly Mock<ILogger<GetTokenCommandHandler>> LoggerMock = new();

        private GetTokenCommandHandler _sut;
        private const string GeneratedToken = "mockTokenValue";

        [SetUp]
        public void SetUp()
        {
            JwtProviderMock.Setup(x => x.Generate(It.IsAny<User>()))
                .Returns(GeneratedToken);
            _sut = new GetTokenCommandHandler(JwtProviderMock.Object, LoggerMock.Object);
        }

        [TestCase(true, false, "jwtProvider")]
        [TestCase(false, true, "logger")]
        public void Constructor_DI(bool useNullJwtProvider, bool useNullLogger, string message)
        {
            //Arrange
            var JwtProvider = useNullJwtProvider ? null : JwtProviderMock.Object;
            var Logger = useNullLogger ? null : LoggerMock.Object;

            //Act
            Action sut = () => new GetTokenCommandHandler(JwtProvider, Logger);

            //Assert
            sut.Should().Throw<ArgumentNullException>().WithMessage($"Value cannot be null. (Parameter '{message}')");
        }

        [Test]
        [AutoData]
        public async Task Handle_CorrectInput_ValidResult(GetTokenCommand command)
        {
            //Act
            var result = await _sut.Handle(command, CancellationToken.None);

            //Assert
            result.Data.Should().Be(GeneratedToken);
        }
    }
}
