using appointly.BLL.DTOs.Treatments;
using appointly.BLL.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace appointly.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TreatmentsController(ITreatmentService treatmentService) : ControllerBase
{
    private readonly ITreatmentService _treatmentService = treatmentService;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTreatment(
        [FromBody] CreateTreatmentRequest createTreatmentRequest
    )
    {
        var response = await _treatmentService.CreateTreatmentAsync(createTreatmentRequest);
        return CreatedAtAction(nameof(GetTreatmentById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTreatmentById(int id)
    {
        var response = await _treatmentService.GetTreatmentByIdAsync(id);
        return Ok(response);
    }
}
