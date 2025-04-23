using appointly.BLL.DTOs.Request;
using appointly.BLL.DTOs.Response;
using appointly.BLL.Services.IServices;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;

namespace appointly.BLL.Services;

public class TreatmentService(ITreatmentRepository treatmentRepository) : ITreatmentService
{
    private readonly ITreatmentRepository _treatmentRepository = treatmentRepository;

    public async Task<CreateTreatmentResponse> CreateTreatmentAsync(
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

        var response = new CreateTreatmentResponse()
        {
            Id = createdTreatment.Id,
            Name = createdTreatment.Name,
            Description = createdTreatment.Description,
            DurationInMinutes = createdTreatment.DurationInMinutes,
            Price = createdTreatment.Price,
        };

        return response;
    }
}
