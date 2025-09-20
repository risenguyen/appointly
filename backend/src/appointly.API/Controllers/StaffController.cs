using appointly.BLL.DTOs.Staff;
using appointly.BLL.Services.IServices;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace appointly.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StaffController(IStaffService staffService) : ControllerBase
{
    private readonly IStaffService _staffService = staffService;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StaffResponse>> CreateStaff(
        [FromBody] CreateStaffRequest createStaffRequest,
        CancellationToken cancellationToken
    )
    {
        var result = await _staffService.CreateStaffAsync(createStaffRequest, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpGet]
    [ProducesResponseType<List<StaffResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<StaffResponse>>> GetStaff(
        CancellationToken cancellationToken
    )
    {
        var result = await _staffService.GetStaffAsync(cancellationToken);
        return result.ToActionResult(this);
    }
}
