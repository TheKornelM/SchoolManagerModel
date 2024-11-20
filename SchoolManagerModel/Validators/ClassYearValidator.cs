using FluentValidation;
using System.Resources;

namespace SchoolManagerModel.Validators;

public class ClassYearValidator : AbstractValidator<string>
{
    public ClassYearValidator(ResourceManager resourceManager)
    {
        RuleFor(x => x)
            .SetValidator(new NumberValidator(resourceManager))
            .GreaterThan("0");
    }
}