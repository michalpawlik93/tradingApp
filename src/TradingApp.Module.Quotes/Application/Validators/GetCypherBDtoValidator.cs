using FluentValidation;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;
using TradingApp.Module.Quotes.Contract.Constants;

namespace TradingApp.Module.Quotes.Application.Validators;

public class GetCypherBDtoValidator : AbstractValidator<GetCypherBDto>
{
    public GetCypherBDtoValidator()
    {
        RuleFor(request => request.Asset)
            .NotNull()
            .WithMessage("Parameter Asset is mandatory.")
            .SetValidator(new AssetValidator());

        RuleFor(request => request.MfiSettings)
            .NotNull()
            .WithMessage("Parameter MfiSettings is mandatory.")
            .SetValidator(new MfiSettingsValidator());

        RuleFor(request => request.SRsiSettings)
            .NotNull()
            .WithMessage("Parameter SRsiSettings is mandatory.")
            .SetValidator(new SRsiSettingsValidator());

        RuleFor(request => request.TimeFrame)
            .NotNull()
            .WithMessage("Parameter TimeFrame is mandatory.")
            .SetValidator(new TimeFrameValidator());

        RuleFor(request => request.WaveTrendSettings)
            .NotNull()
            .WithMessage("Parameter WaveTrendSettings is mandatory.")
            .SetValidator(new WaveTrendSettingsValidator());
    }
}

public class AssetValidator : AbstractValidator<AssetDto>
{
    public AssetValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(("Name is required"))
            .Must(name => Enum.TryParse(name, out AssetName _))
            .WithMessage("Parameter Name must be one of AssetName enumeration.");

        RuleFor(x => x.Type)
            .NotEmpty()
            .WithMessage(("Type is required"))
            .Must(type => Enum.TryParse(type, out AssetType _))
            .WithMessage("Parameter Type must be one of AssetType enumeration.");
    }
}

public class MfiSettingsValidator : AbstractValidator<MfiSettingsDto>
{
    public MfiSettingsValidator()
    {
        RuleFor(x => x.ChannelLength).GreaterThan(0).WithMessage(("ChannelLength is required"));
    }
}

public class SRsiSettingsValidator : AbstractValidator<SRsiSettingsDto>
{
    public SRsiSettingsValidator()
    {
        RuleFor(x => x.ChannelLength).GreaterThan(0).WithMessage("ChannelLength is required");
        RuleFor(x => x.ChannelLength).GreaterThan(0).WithMessage("StochDSmooth is required");
        RuleFor(x => x.StochDSmooth).GreaterThan(0).WithMessage("StochKSmooth is required");
        RuleFor(x => x.Overbought)
            .ExclusiveBetween(0, 100)
            .WithMessage("Overbought must be between 0 and 100");
        RuleFor(x => x.Oversold)
            .GreaterThan(-100)
            .LessThan(0)
            .WithMessage("Oversold must be between 0 and -100");
    }
}

public class TimeFrameValidator : AbstractValidator<TimeFrameDto>
{
    public TimeFrameValidator()
    {
        RuleFor(x => x.StartDate).NotNull().WithMessage("StartDate is required");
        RuleFor(x => x.EndDate).NotNull().WithMessage("EndDate is required");
        RuleFor(x => x.Granularity)
            .NotEmpty()
            .WithMessage(("Granularity is required"))
            .Must(type => Enum.TryParse(type, out Granularity _))
            .WithMessage("Parameter Granularity must be one of Granularity enumeration.");
    }
}

public class WaveTrendSettingsValidator : AbstractValidator<WaveTrendSettingsDto>
{
    public WaveTrendSettingsValidator()
    {
        RuleFor(x => x.ChannelLength).GreaterThan(0).WithMessage("ChannelLength is required");
        RuleFor(x => x.MovingAverageLength)
            .GreaterThan(0)
            .WithMessage("MovingAverageLength is required");
        RuleFor(x => x.AverageLength).GreaterThan(0).WithMessage("AverageLength is required");
        RuleFor(x => x.Overbought)
            .ExclusiveBetween(0, 100)
            .WithMessage("Overbought must be between 0 and 100");
        RuleFor(x => x.Oversold)
            .GreaterThan(-100)
            .LessThan(0)
            .WithMessage("Oversold must be between 0 and -100");
        RuleFor(x => x.OverboughtLevel2)
            .ExclusiveBetween(0, 100)
            .WithMessage("OverboughtLevel2 must be between 0 and 100");
        RuleFor(x => x.OversoldLevel2)
            .GreaterThan(-100)
            .LessThan(0)
            .WithMessage("OversoldLevel2 must be between 0 and -100");
    }
}
