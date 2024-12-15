using FluentValidation;
using System.Resources;

namespace SchoolManagerModel.Validators;

public class PositiveNumberValidator : AbstractValidator<int>
{
    public PositiveNumberValidator(ResourceManager resourceManager)
    {
        RuleFor(x => x)
            .GreaterThan(0)
            .WithMessage(resourceManager.GetString("MustBeGreaterThan0"));
    }
}