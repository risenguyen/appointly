using appointly.BLL.DTOs.Treatments;
using appointly.BLL.Services.IServices;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;
using Ardalis.Result;

namespace appointly.BLL.Services;

public class TreatmentService(ITreatmentRepository treatmentRepository) : ITreatmentService
{
    private readonly ITreatmentRepository _treatmentRepository = treatmentRepository;

    public async Task<Result<TreatmentResponse>> CreateTreatmentAsync(
        CreateTreatmentRequest createTreatmentRequest,
        CancellationToken cancellationToken
    )
    {
        var treatment = new Treatment()
        {
            Name = createTreatmentRequest.Name,
            Description = createTreatmentRequest.Description,
            DurationInMinutes = createTreatmentRequest.DurationInMinutes,
            Price = createTreatmentRequest.Price,
        };
        var createdTreatment = await _treatmentRepository.CreateTreatmentAsync(
            treatment,
            cancellationToken
        );
        var response = new TreatmentResponse()
        {
            Id = createdTreatment.Id,
            Name = createdTreatment.Name,
            Description = createdTreatment.Description,
            DurationInMinutes = createdTreatment.DurationInMinutes,
            Price = createdTreatment.Price,
        };
        return Result.Created(response, $"/api/Treatments/{response.Id}");
    }

    public async Task<Result<TreatmentResponse>> GetTreatmentByIdAsync(
        int id,
        CancellationToken cancellationToken
    )
    {
        var treatment = await _treatmentRepository.GetTreatmentByIdAsync(id, cancellationToken);
        if (treatment == null)
        {
            return Result.NotFound($"Treatment with ID {id} can not be found.");
        }

        var response = new TreatmentResponse()
        {
            Id = treatment.Id,
            Name = treatment.Name,
            Description = treatment.Description,
            DurationInMinutes = treatment.DurationInMinutes,
            Price = treatment.Price,
        };
        return Result.Success(response);
    }

    public async Task<Result<List<TreatmentResponse>>> GetAllTreatmentsAsync(
        CancellationToken cancellationToken
    )
    {
        var treatments = await _treatmentRepository.GetAllTreatmentsAsync(cancellationToken);
        var response = treatments
            .Select(treatment => new TreatmentResponse()
            {
                Id = treatment.Id,
                Name = treatment.Name,
                Description = treatment.Description,
                DurationInMinutes = treatment.DurationInMinutes,
                Price = treatment.Price,
            })
            .ToList();
        return Result.Success(response);
    }
}
