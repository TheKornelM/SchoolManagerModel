using FluentValidation;
using System.Resources;
using SchoolManagerModel.Entities;

namespace SchoolManagerModel.Validators;

public class ClassValidator : AbstractValidator<Class>
{
    public ClassValidator(ResourceManager resourceManager)
    {
        RuleFor(x => x.Year)
            .SetValidator(new PositiveNumberValidator(resourceManager));
        RuleFor(x => x.SchoolClass)
            .SetValidator(new CharLetterValidator(resourceManager));
    }
}