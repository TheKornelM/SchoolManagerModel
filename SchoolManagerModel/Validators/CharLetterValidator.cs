using FluentValidation;
using System.Resources;

namespace SchoolManagerModel.Validators;

public class CharLetterValidator : AbstractValidator<string>
{
    public CharLetterValidator(ResourceManager resourceManager)
    {
        RuleFor(x => x)
            .Must((cls) => cls.Length == 1 && Char.IsLetter(cls[0]))
            .WithMessage("Must be a letter!");
    }
}