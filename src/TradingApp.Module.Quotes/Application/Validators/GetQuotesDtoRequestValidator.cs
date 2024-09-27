using FluentValidation;
using TradingApp.Module.Quotes.Application.Dtos;

namespace TradingApp.Module.Quotes.Application.Validators;

public class GetQuotesDtoRequestValidator : AbstractValidator<GetQuotesDtoRequest>
{
    public GetQuotesDtoRequestValidator()
    {
        CommonValidationRules.RuleForNullable(this, request => request.Asset, new AssetValidator());
        RuleForEach(p => p.Indicators)
            .ChildRules(indicator =>
            {
                CommonValidationRules.RuleForNullable(indicator, i => i, new IndicatorsValidator());
            });
        CommonValidationRules.RuleForNullable(
            this,
            request => request.TimeFrame,
            new TimeFrameValidator()
        );
        CommonValidationRules.RuleForNullable(
            this,
            request => request.Settings,
            new SettingsValidator()
        );
    }

    public class SettingsValidator : AbstractValidator<SettingsDto>
    {
        public SettingsValidator()
        {
            RuleFor(request => request.SrsiSettings).SetValidator(new SRsiSettingsValidator());
        }
    }
}
