using FluentValidation;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Contract.Constants;

namespace TradingApp.Module.Quotes.Application.Validators;

public class GetQuotesDtoRequestValidator : AbstractValidator<GetQuotesDtoRequest>
{
    public GetQuotesDtoRequestValidator()
    {
        CommonValidationRules.RuleForNullable(this, x => x.StartDate);
        CommonValidationRules.RuleForNullable(this, x => x.EndDate);
        CommonValidationRules.RuleForEnum<GetQuotesDtoRequest, string, Granularity>(
            this,
            dto => dto.Granularity
        );
        RuleForEach(p => p.TechnicalIndicators)
            .ChildRules(indicator =>
            {
                CommonValidationRules.RuleForEnum<string, string, TechnicalIndicator>(
                    indicator,
                    x => x
                );
            });
        CommonValidationRules.RuleForEnum<GetQuotesDtoRequest, string, AssetName>(
            this,
            dto => dto.AssetName
        );
        CommonValidationRules.RuleForEnum<GetQuotesDtoRequest, string, AssetType>(
            this,
            dto => dto.AssetType
        );
    }
}
