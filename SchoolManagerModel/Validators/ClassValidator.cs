using FluentValidation;
using System.Resources;

namespace SchoolManagerModel.Validators;

public class ClassValidator : AbstractValidator<(string year, string cls)>
{
    public ClassValidator(ResourceManager resourceManager)
    {
        RuleFor(x => x.year)
            .SetValidator(new NumberValidator(resourceManager))
            .GreaterThan("0");
        RuleFor(x => x.cls)
            .SetValidator(new CharLetterValidator(resourceManager));
    }
}