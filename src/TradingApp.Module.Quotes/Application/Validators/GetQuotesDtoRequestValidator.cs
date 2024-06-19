using FluentValidation;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Contract.Constants;

namespace TradingApp.Module.Quotes.Application.Validators;

public class GetQuotesDtoRequestValidator : AbstractValidator<GetQuotesDtoRequest>
{
    public GetQuotesDtoRequestValidator()
    {
        RuleFor(x => x.StartDate).NotNull().WithMessage("StartDate is required");
        RuleFor(x => x.EndDate).NotNull().WithMessage("EndDate is required");
        RuleFor(x => x.Granularity)
            .NotEmpty()
            .WithMessage(("Granularity is required"))
            .Must(g => Enum.TryParse(g, out Granularity _))
            .WithMessage("Parameter Granularity must be one of Granularity enumeration.");

        RuleForEach(p => p.TechnicalIndicators)
            .ChildRules(
                p =>
                    p.RuleFor(x => x)
                        .NotEmpty()
                        .WithMessage(("TechnicalIndicators is required"))
                        .Must(type => Enum.TryParse(type, out TechnicalIndicator _))
                        .WithMessage(
                            "Parameter TechnicalIndicators must be one of TechnicalIndicator enumeration."
                        )
            );

        RuleFor(x => x.AssetName)
            .NotEmpty()
            .WithMessage(("AssetName is required"))
            .Must(name => Enum.TryParse(name, out AssetName _))
            .WithMessage("Parameter AssetName must be one of AssetName enumeration.");

        RuleFor(x => x.AssetType)
            .NotEmpty()
            .WithMessage(("Type is required"))
            .Must(type => Enum.TryParse(type, out AssetType _))
            .WithMessage("Parameter AssetType must be one of AssetType enumeration.");
    }
}
