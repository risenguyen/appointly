using appointly.BLL.DTOs.Auth;
using FluentValidation;

namespace appointly.BLL.Validators.Auth;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email must be valid.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
    }
}
