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

        RuleFor(x => x.Phone).Matches(@"^\d{10}$").When(x => !string.IsNullOrEmpty(x.Phone));
    }
}
