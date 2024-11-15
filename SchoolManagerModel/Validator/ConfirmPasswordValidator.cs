using System.Resources;
using FluentValidation;

namespace SchoolManagerModel.Validator;

internal class ConfirmPasswordValidator : AbstractValidator<(string password, string confirmPassword)>
{
    public ConfirmPasswordValidator(ResourceManager resourceManager)
    {
        RuleFor(x => x.confirmPassword)
            .Equal(x => x.password)
            .WithMessage(resourceManager.GetString("NotMatchPasswordConfirm"));
    }
}
