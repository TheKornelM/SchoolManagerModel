﻿using System.Resources;
using FluentValidation;

namespace SchoolManagerModel.Validators;

public class ConfirmPasswordValidator : AbstractValidator<(string password, string confirmPassword)>
{
    public ConfirmPasswordValidator(ResourceManager resourceManager)
    {
        RuleFor(x => x.confirmPassword)
            .Equal(x => x.password)
            .WithMessage(resourceManager.GetString("NotMatchPasswordConfirm"));
    }
}