using FluentValidation;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Contract.Constants;

namespace TradingApp.Module.Quotes.Application.Validators;

public class GetQuotesDtoRequestValidator : AbstractValidator<GetQuotesDtoRequest>
{
    public GetQuotesDtoRequestValidator()
    {
        CommonValidationRules.RuleForNullable(this, request => request.Asset, new AssetValidator());
        RuleForEach(p => p.TechnicalIndicators)
            .ChildRules(indicator =>
            {
                CommonValidationRules.RuleForEnum<string, string, TechnicalIndicator>(
                    indicator,
                    x => x
                );
            });
        CommonValidationRules.RuleForNullable(this, request => request.TimeFrame, new TimeFrameValidator());
    }
}
