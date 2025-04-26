using appointly.BLL.DTOs.Treatments;
using FluentValidation;

namespace appointly.BLL.Validators.Treatments;

public class CreateTreatmentValidator : AbstractValidator<TreatmentRequest>
{
    public CreateTreatmentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Treatment name is required.")
            .MaximumLength(100)
            .WithMessage("Treatment name cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Treatment description is required.")
            .MaximumLength(500)
            .WithMessage("Treatment description cannot exceed 500 characters.");

        RuleFor(x => x.DurationInMinutes)
            .GreaterThan(0)
            .WithMessage("Duration must be a positive number of minutes.");

        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");
    }
}
