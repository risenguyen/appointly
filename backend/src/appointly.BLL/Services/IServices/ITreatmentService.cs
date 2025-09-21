using appointly.BLL.DTOs.Treatments;
using Ardalis.Result;

namespace appointly.BLL.Services.IServices;

public interface ITreatmentService
{
    Task<Result<TreatmentResponse>> CreateTreatmentAsync(
        CreateTreatmentRequest createTreatmentRequest,
        CancellationToken cancellationToken
    );
    Task<Result<List<TreatmentResponse>>> GetTreatmentsAsync(CancellationToken cancellationToken);
    Task<Result<TreatmentResponse>> GetTreatmentAsync(int id, CancellationToken cancellationToken);
    Task<Result<TreatmentResponse>> UpdateTreatmentAsync(
        int id,
        UpdateTreatmentRequest updateTreatmentRequest,
        CancellationToken cancellationToken
    );
    Task<Result> DeleteTreatmentAsync(int id, CancellationToken cancellationToken);
}
