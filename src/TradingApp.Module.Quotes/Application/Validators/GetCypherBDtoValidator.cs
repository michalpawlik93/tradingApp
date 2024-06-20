using FluentValidation;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;
using TradingApp.Module.Quotes.Contract.Constants;

namespace TradingApp.Module.Quotes.Application.Validators;


public class GetCypherBDtoValidator : AbstractValidator<GetCypherBDto>
{
    public GetCypherBDtoValidator()
    {
        CommonValidationRules.RuleForNullable(this, request => request.Asset, new AssetValidator());
        CommonValidationRules.RuleForNullable(this, request => request.MfiSettings, new MfiSettingsValidator());
        CommonValidationRules.RuleForNullable(this, request => request.SRsiSettings, new SRsiSettingsValidator());
        CommonValidationRules.RuleForNullable(this, request => request.TimeFrame, new TimeFrameValidator());
        CommonValidationRules.RuleForNullable(this, request => request.WaveTrendSettings, new WaveTrendSettingsValidator());
    }
}

public class AssetValidator : AbstractValidator<AssetDto>
{
    public AssetValidator()
    {
        CommonValidationRules.RuleForEnum<AssetDto, string, AssetName>(this, dto => dto.Name);
        CommonValidationRules.RuleForEnum<AssetDto, string, AssetType>(this, dto => dto.Type);
    }
}

public class MfiSettingsValidator : AbstractValidator<MfiSettingsDto>
{
    public MfiSettingsValidator()
    {
        CommonValidationRules.RuleForChannelLength(this, x => x.ChannelLength);
    }
}

public class SRsiSettingsValidator : AbstractValidator<SRsiSettingsDto>
{
    public SRsiSettingsValidator()
    {
        CommonValidationRules.RuleForChannelLength(this, x => x.ChannelLength);
        CommonValidationRules.RuleForChannelLength(this, x => x.StochKSmooth);
        CommonValidationRules.RuleForChannelLength(this, x => x.StochDSmooth);
        CommonValidationRules.RuleForOverbought(this, x => x.Overbought);
        CommonValidationRules.RuleForOversold(this, x => x.Oversold);
    }
}

public class TimeFrameValidator : AbstractValidator<TimeFrameDto>
{
    public TimeFrameValidator()
    {
        CommonValidationRules.RuleForNullable(this, x => x.StartDate);
        CommonValidationRules.RuleForNullable(this, x => x.EndDate);
        CommonValidationRules.RuleForEnum<TimeFrameDto, string, Granularity>(this, dto => dto.Granularity);
    }
}

public class WaveTrendSettingsValidator : AbstractValidator<WaveTrendSettingsDto>
{
    public WaveTrendSettingsValidator()
    {
        CommonValidationRules.RuleForChannelLength(this, x => x.ChannelLength);
        CommonValidationRules.RuleForChannelLength(this, x => x.MovingAverageLength);
        CommonValidationRules.RuleForChannelLength(this, x => x.AverageLength);
        CommonValidationRules.RuleForOverbought(this, x => x.Overbought);
        CommonValidationRules.RuleForOversold(this, x => x.Oversold);
        CommonValidationRules.RuleForOverbought(this, x => x.OverboughtLevel2);
        CommonValidationRules.RuleForOversold(this, x => x.OversoldLevel2);
    }
}
