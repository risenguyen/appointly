using appointly.BLL.DTOs.Request;
using appointly.BLL.DTOs.Response;

namespace appointly.BLL.Services.IServices;

public interface ITreatmentService
{
    Task<CreateTreatmentResponse> CreateTreatmentAsync(
        CreateTreatmentRequest createTreatmentRequest
    );
}
