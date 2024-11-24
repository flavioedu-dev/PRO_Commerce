using FluentValidation;
using PROCommerce.Authentication.API.Models.Auth.Login;
using PROCommerce.Authentication.API.Resources;

namespace PROCommerce.Authentication.API.Validators.Auth;

public class LoginModelValidator : AbstractValidator<LoginModel>
{
    public LoginModelValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(x => string.Format(ApiMessages.Auth_Field_Required, nameof(x.Username)));

        RuleFor(x => x.Username!.Length)
            .GreaterThanOrEqualTo(3)
            .WithMessage(x => string.Format(ApiMessages.Auth_Field_GreaterThanOrEqual, nameof(x.Username), 3));

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(x => string.Format(ApiMessages.Auth_Field_Required, nameof(x.Username)));

        RuleFor(x => x.Password!.Length)
            .GreaterThanOrEqualTo(8)
            .WithMessage(x => string.Format(ApiMessages.Auth_Field_GreaterThanOrEqual, nameof(x.Password), 8));

        RuleFor(x => x.Password)
            .Matches("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]+$")
            .WithMessage(ApiMessages.Auth_Password_Required_Characters);
    }
}
