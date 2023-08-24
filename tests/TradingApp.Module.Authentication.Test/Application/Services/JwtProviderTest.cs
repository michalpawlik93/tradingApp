using AutoFixture;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TradingApp.Module.Quotes.Authentication.Configuration;
using TradingApp.Module.Quotes.Authentication.Models;
using TradingApp.Module.Quotes.Authentication.Services;

namespace TradingApp.Module.Quotes.Test.Authentication.Services;

[TestFixture]
public class JwtProviderTest
{
    private IOptions<JwtOptions> JwtOptionsMock;
    private readonly Mock<ILogger<JwtProvider>> LoggerMock = new();
    private static string ApiSecretMock = "ThisIsAReallyLongKeyWithMoreThan520BitsOfData12345678901234567890123456789012345678901234567890";

    private JwtProvider _sut;

    [SetUp]
    public void SetUp()
    {
        var jwtOptions = new Fixture().Build<JwtOptions>().With(x => x.SecretKey, ApiSecretMock).Create();
        JwtOptionsMock = Options.Create(jwtOptions);
        _sut = new JwtProvider(LoggerMock.Object, JwtOptionsMock);
    }

    [TestCase(true, false, "jwtOptions")]
    [TestCase(false, true, "logger")]
    public void Constructor_DI(bool useNullOptions, bool useNullLogger, string message)
    {
        //Arrange
        var Options = useNullOptions ? null : JwtOptionsMock;
        var Logger = useNullLogger ? null : LoggerMock.Object;

        //Act
        Action sut = () => new JwtProvider(Logger, Options);

        //Assert
        sut.Should().Throw<ArgumentNullException>().WithMessage($"Value cannot be null. (Parameter '{message}')");
    }

    [Test, AutoData]
    public void Handle_CorrectInput_ValidTokenResulted(User user)
    {
        //Arrange
        user.ApiSecret = "ThisIsAReallyLongKeyWithMoreThan520BitsOfData12345678901234567890123456789012345678901234567890";

        //Act
        var result = _sut.Generate(user);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNullOrEmpty();
    }

    [Test]
    [InlineAutoData("")]
    [InlineAutoData(null)]
    public void Handle_InvalidUserName_ErrorReturned(string userName, User user)
    {
        //Arrange
        user.Name = userName;

        //Act
        var result = _sut.Generate(user);

        //Assert
        result.IsFailed.Should().BeTrue();
        result.ValueOrDefault.Should().BeNull();
        result.Errors.Should().NotBeNullOrEmpty();
    }

    [Test]
    [InlineAutoData("")]
    [InlineAutoData(null)]
    public void Handle_InvalidApiSecret_ErrorReturned(string apiSecret, User user)
    {
        //Arrange
        user.ApiSecret = apiSecret;

        //Act
        var result = _sut.Generate(user);

        //Assert
        result.IsFailed.Should().BeTrue();
        result.ValueOrDefault.Should().BeNull();
        result.Errors.Should().NotBeNullOrEmpty();
    }

    [Test, AutoData]
    public void Handle_IncorrectCredentials_ErrorReturned(User user)
    {
        //Act
        var result = _sut.Generate(user);

        //Assert
        result.IsFailed.Should().BeTrue();
        result.ValueOrDefault.Should().BeNull();
        result.Errors.Should().NotBeNullOrEmpty();
        result.Errors.Select(x => x.Message).Should().Contain(JwtProviderErrorMessages.IncorrectCrednetialsErrorMessage);
    }
}
