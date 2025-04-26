using appointly.BLL.DTOs.Treatments;
using appointly.BLL.Services.IServices;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace appointly.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TreatmentsController(ITreatmentService treatmentService) : ControllerBase
{
    private readonly ITreatmentService _treatmentService = treatmentService;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TreatmentResponse>> CreateTreatment(
        [FromBody] CreateTreatmentRequest createTreatmentRequest,
        CancellationToken cancellationToken
    )
    {
        var result = await _treatmentService.CreateTreatmentAsync(
            createTreatmentRequest,
            cancellationToken
        );
        return result.ToActionResult(this);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TreatmentResponse>> GetTreatmentById(
        int id,
        CancellationToken cancellationToken
    )
    {
        var result = await _treatmentService.GetTreatmentByIdAsync(id, cancellationToken);
        return result.ToActionResult(this);
    }
}
