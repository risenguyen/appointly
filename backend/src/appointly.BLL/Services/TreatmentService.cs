using appointly.BLL.DTOs.Treatments;
using appointly.BLL.Exceptions;
using appointly.BLL.Services.IServices;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;

namespace appointly.BLL.Services;

public class TreatmentService(ITreatmentRepository treatmentRepository) : ITreatmentService
{
    private readonly ITreatmentRepository _treatmentRepository = treatmentRepository;

    public async Task<TreatmentResponse> CreateTreatmentAsync(
        CreateTreatmentRequest createTreatmentRequest
    )
    {
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
