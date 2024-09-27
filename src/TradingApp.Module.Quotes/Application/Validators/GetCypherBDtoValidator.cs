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
        CommonValidationRules.RuleForNullable(this, request => request.Settings, new SettingsValidator());
        CommonValidationRules.RuleForNullable(this, request => request.TimeFrame, new TimeFrameValidator());
    }

    public class SettingsValidator : AbstractValidator<SettingsDto>
    {
        public SettingsValidator()
        {
            CommonValidationRules.RuleForNullable(this, request => request.SrsiSettings, new SRsiSettingsValidator());
            CommonValidationRules.RuleForNullable(this, request => request.WaveTrendSettings, new WaveTrendSettingsValidator());
            CommonValidationRules.RuleForNullable(this, request => request.MfiSettings, new MfiSettingsValidator());
        }
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

public class IndicatorsValidator : AbstractValidator<IndicatorsDto>
{
    public IndicatorsValidator()
    {
        CommonValidationRules.RuleForEnum<IndicatorsDto, string, TechnicalIndicator>(this, dto => dto.TechnicalIndicator);
        RuleForEach(p => p.SideIndicators)
            .ChildRules(indicator =>
            {
                CommonValidationRules.RuleForEnum<string, string, SideIndicator>(
                    indicator,
                    x => x
                );
            });
    }
}

public class MfiSettingsValidator : AbstractValidator<MfiSettingsDto>
{
    public MfiSettingsValidator()
    {
        CommonValidationRules.RuleForChannelLength(this, x => x.ChannelLength);
    }
}

public class SRsiSettingsValidator : AbstractValidator<SrsiSettingsDto>
{
    public SRsiSettingsValidator()
    {
        CommonValidationRules.RuleForChannelLength(this, x => x.ChannelLength);
        CommonValidationRules.RuleForChannelLength(this, x => x.StochKSmooth);
        CommonValidationRules.RuleForChannelLength(this, x => x.StochDSmooth);
        CommonValidationRules.RuleForOverbought(this, x => x.Overbought);
        CommonValidationRules.RuleForOverbought(this, x => x.Oversold);
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
