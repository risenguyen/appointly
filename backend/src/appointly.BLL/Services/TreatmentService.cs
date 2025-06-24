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
            Price = createTreatmentRequest.Price,
            DurationInMinutes = createTreatmentRequest.DurationInMinutes,
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
            Price = createdTreatment.Price,
            DurationInMinutes = createdTreatment.DurationInMinutes,
        };
        return Result.Created(response, $"/api/Treatments/{response.Id}");
    }

    public async Task<Result<TreatmentResponse>> UpdateTreatmentAsync(
        int id,
        UpdateTreatmentRequest updateTreatmentRequest,
        CancellationToken cancellationToken
    )
    {
        var treatmentToUpdate = await _treatmentRepository.GetTreatmentAsync(id, cancellationToken);
        if (treatmentToUpdate == null)
        {
            return Result.NotFound($"Treatment with ID {id} can not be found.");
        }

        treatmentToUpdate.Name = updateTreatmentRequest.Name;
        treatmentToUpdate.Description = updateTreatmentRequest.Description;
        treatmentToUpdate.Price = updateTreatmentRequest.Price;
        treatmentToUpdate.DurationInMinutes = updateTreatmentRequest.DurationInMinutes;

        await _treatmentRepository.UpdateTreatmentAsync(treatmentToUpdate, cancellationToken);

        var response = new TreatmentResponse()
        {
            Id = treatmentToUpdate.Id,
            Name = treatmentToUpdate.Name,
            Description = treatmentToUpdate.Description,
            Price = treatmentToUpdate.Price,
            DurationInMinutes = treatmentToUpdate.DurationInMinutes,
        };
        return Result.Success(response);
    }

    public async Task<Result> DeleteTreatmentAsync(int id, CancellationToken cancellationToken)
    {
        var treatment = await _treatmentRepository.GetTreatmentAsync(id, cancellationToken);
        if (treatment == null)
        {
            return Result.NotFound($"Treatment with ID {id} can not be found.");
        }

        await _treatmentRepository.DeleteTreatmentAsync(treatment, cancellationToken);
        return Result.Success();
    }

    public async Task<Result<TreatmentResponse>> GetTreatmentAsync(
        int id,
        CancellationToken cancellationToken
    )
    {
        var treatment = await _treatmentRepository.GetTreatmentAsync(id, cancellationToken);
        if (treatment == null)
        {
            return Result.NotFound($"Treatment with ID {id} can not be found.");
        }

        var response = new TreatmentResponse()
        {
            Id = treatment.Id,
            Name = treatment.Name,
            Description = treatment.Description,
            Price = treatment.Price,
            DurationInMinutes = treatment.DurationInMinutes,
        };
        return Result.Success(response);
    }

    public async Task<Result<List<TreatmentResponse>>> GetTreatmentsAsync(
        CancellationToken cancellationToken
    )
    {
        var treatments = await _treatmentRepository.GetTreatmentsAsync(cancellationToken);
        var response = treatments
            .Select(treatment => new TreatmentResponse()
            {
                Id = treatment.Id,
                Name = treatment.Name,
                Description = treatment.Description,
                Price = treatment.Price,
                DurationInMinutes = treatment.DurationInMinutes,
            })
            .ToList();
        return Result.Success(response);
    }
}
