using appointly.BLL.DTOs.Treatments;
using Ardalis.Result;

namespace appointly.BLL.Services.IServices;

public interface ITreatmentService
{
    Task<Result<TreatmentResponse>> CreateTreatmentAsync(
        CreateTreatmentRequest createTreatmentRequest,
        CancellationToken cancellationToken
    );

    Task<Result<TreatmentResponse>> GetTreatmentByIdAsync(
        int id,
        CancellationToken cancellationToken
    );

    Task<Result<List<TreatmentResponse>>> GetAllTreatmentsAsync(
        CancellationToken cancellationToken
    );

    Task<Result> DeleteTreatmentByIdAsync(int id, CancellationToken cancellationToken);
}
