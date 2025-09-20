using appointly.BLL.DTOs.Staff;
using FluentValidation;

namespace appointly.BLL.Validators.Staff;

public class CreateStaffValidator : AbstractValidator<CreateStaffRequest>
{
    public CreateStaffValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);

        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);

        RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.Phone)
            .Matches(@"^\+?[1-9]\d{7,14}$") // E.164 international phone number format: +[country code][subscriber number], 8-15 digits
            .When(x => !string.IsNullOrEmpty(x.Phone));
    }
}
