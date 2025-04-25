using appointly.BLL.DTOs.Treatments;
using appointly.BLL.Services.IServices;
using appointly.BLL.Validators.Treatments;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace appointly.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TreatmentsController(
    ITreatmentService treatmentService,
    CreateTreatmentRequestValidator validator
) : ControllerBase
{
    private readonly ITreatmentService _treatmentService = treatmentService;
    private readonly CreateTreatmentRequestValidator _validator = validator;

    [HttpPost]
    [ProducesResponseType(typeof(TreatmentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTreatment(
        [FromBody] CreateTreatmentRequest createTreatmentRequest,
        CancellationToken cancellationToken
    )
    {
        await _validator.ValidateAndThrowAsync(createTreatmentRequest, cancellationToken);
        var response = await _treatmentService.CreateTreatmentAsync(
            createTreatmentRequest,
            cancellationToken
        );
        return CreatedAtAction(nameof(GetTreatmentById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TreatmentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTreatmentById(int id, CancellationToken cancellationToken)
    {
        var response = await _treatmentService.GetTreatmentByIdAsync(id, cancellationToken);
        return Ok(response);
    }
}
