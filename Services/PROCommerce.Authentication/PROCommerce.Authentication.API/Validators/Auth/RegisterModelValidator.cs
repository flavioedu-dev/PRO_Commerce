﻿using FluentValidation;
using PROCommerce.Authentication.API.Models.Auth.Register;
using PROCommerce.Authentication.API.Resources;

namespace PROCommerce.Authentication.API.Validators.Auth;

public class RegisterModelValidator : AbstractValidator<RegisterModel>
{
    public RegisterModelValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage(x => string.Format(ApiMessages.Auth_Field_Required, nameof(x.FullName)));

        RuleFor(x => x.FullName!.Length)
            .GreaterThanOrEqualTo(3)
            .WithMessage(x => string.Format(ApiMessages.Auth_Field_GreaterThanOrEqual, nameof(x.FullName), 3))
            .When(x => x.FullName is not null);

        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(x => string.Format(ApiMessages.Auth_Field_Required, nameof(x.Username)));

        RuleFor(x => x.Username!.Length)
            .GreaterThanOrEqualTo(3)
            .WithMessage(x => string.Format(ApiMessages.Auth_Field_GreaterThanOrEqual, nameof(x.Username), 3))
            .When(x => x.Username is not null);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(x => string.Format(ApiMessages.Auth_Field_Required, nameof(x.Email)));

        RuleFor(x => x.Email)
            .Matches("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")
            .WithMessage(x => string.Format(ApiMessages.Auth_Field_Invalid, nameof(x.Email)));

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(x => string.Format(ApiMessages.Auth_Field_Required, nameof(x.Password)));

        RuleFor(x => x.Password!.Length)
            .GreaterThanOrEqualTo(8)
            .WithMessage(x => string.Format(ApiMessages.Auth_Field_GreaterThanOrEqual, nameof(x.Password), 8))
            .When(x => x.Password is not null);

        RuleFor(x => x.Password)
            .Matches("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]+$")
            .WithMessage(ApiMessages.Auth_Password_Required_Characters);
    }
}
