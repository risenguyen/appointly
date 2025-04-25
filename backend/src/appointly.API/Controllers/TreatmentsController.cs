using appointly.BLL.DTOs.Treatments;
using appointly.BLL.Services.IServices;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace appointly.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TreatmentsController(ITreatmentService treatmentService) : ControllerBase
{
    private readonly ITreatmentService _treatmentService = treatmentService;

    [HttpPost]
    [TranslateResultToActionResult]
    [ExpectedFailures(ResultStatus.Invalid)]
    public async Task<Result<TreatmentResponse>> CreateTreatment(
        [FromBody] CreateTreatmentRequest createTreatmentRequest,
        CancellationToken cancellationToken
    )
    {
        return await _treatmentService.CreateTreatmentAsync(
            createTreatmentRequest,
            cancellationToken
        );
    }

    [HttpGet("{id}")]
    [TranslateResultToActionResult]
    [ExpectedFailures(ResultStatus.NotFound)]
    public async Task<Result<TreatmentResponse>> GetTreatmentById(
        int id,
        CancellationToken cancellationToken
    )
    {
        return await _treatmentService.GetTreatmentByIdAsync(id, cancellationToken);
    }
}
