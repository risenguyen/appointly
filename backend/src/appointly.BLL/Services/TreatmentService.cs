using appointly.BLL.DTOs.Treatments;
using appointly.BLL.Exceptions;
using appointly.BLL.Services.IServices;
using appointly.BLL.Validators.Treatments;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;
using FluentValidation;

namespace appointly.BLL.Services;

public class TreatmentService(
    ITreatmentRepository treatmentRepository,
    CreateTreatmentRequestValidator validator
) : ITreatmentService
{
    private readonly ITreatmentRepository _treatmentRepository = treatmentRepository;
    private readonly CreateTreatmentRequestValidator _validator = validator;

    public async Task<TreatmentResponse> CreateTreatmentAsync(
        CreateTreatmentRequest createTreatmentRequest
    )
    {
        var validationResult = await _validator.ValidateAsync(createTreatmentRequest);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var treatment = new Treatment()
        {
            Name = createTreatmentRequest.Name,
            Description = createTreatmentRequest.Description,
            DurationInMinutes = createTreatmentRequest.DurationInMinutes,
            Price = createTreatmentRequest.Price,
        };
        var createdTreatment = await _treatmentRepository.CreateTreatmentAsync(treatment);

        var response = new TreatmentResponse()
        {
            Id = createdTreatment.Id,
            Name = createdTreatment.Name,
            Description = createdTreatment.Description,
            DurationInMinutes = createdTreatment.DurationInMinutes,
            Price = createdTreatment.Price,
        };
        return response;
    }

    public async Task<TreatmentResponse> GetTreatmentByIdAsync(int id)
    {
        var treatment =
            await _treatmentRepository.GetTreatmentByIdAsync(id)
            ?? throw new NotFoundException("Treatment was not found");
        var response = new TreatmentResponse()
        {
            Id = treatment.Id,
            Name = treatment.Name,
            Description = treatment.Description,
            DurationInMinutes = treatment.DurationInMinutes,
            Price = treatment.Price,
        };
        return response;
    }
}
