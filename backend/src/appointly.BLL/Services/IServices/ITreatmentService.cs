using appointly.BLL.DTOs.Treatments;

namespace appointly.BLL.Services.IServices;

public interface ITreatmentService
{
    Task<TreatmentResponse> CreateTreatmentAsync(
        CreateTreatmentRequest createTreatmentRequest,
        CancellationToken cancellationToken = default
    );

    Task<TreatmentResponse> GetTreatmentByIdAsync(
        int id,
        CancellationToken cancellationToken = default
    );
}
