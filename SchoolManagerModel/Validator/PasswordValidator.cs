using System.Resources;
using FluentValidation;

namespace SchoolManagerModel.Validator
{
    internal class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator(ResourceManager resourceManager)
        {
            RuleFor(password => password)
                .NotNull()
                .NotEmpty()
                    .WithMessage(resourceManager.GetString("MustNotBeEmpty"))
                .MinimumLength(8)
                    .WithMessage(resourceManager.GetString("MustHaveAtLeast8Characters"));
        }
    }
}
