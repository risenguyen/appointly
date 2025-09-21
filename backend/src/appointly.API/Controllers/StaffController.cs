using appointly.BLL.DTOs.Staff;
using appointly.BLL.Services.IServices;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace appointly.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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
        var result = await _staffService.CreateStaffMemberAsync(
            createStaffRequest,
            cancellationToken
        );
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

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StaffResponse>> GetStaffMember(
        [FromRoute] int id,
        CancellationToken cancellationToken
    )
    {
        var result = await _staffService.GetStaffMemberAsync(id, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StaffResponse>> UpdateStaffMember(
        [FromRoute] int id,
        [FromBody] UpdateStaffRequest updateStaffRequest,
        CancellationToken cancellationToken
    )
    {
        var result = await _staffService.UpdateStaffMemberAsync(
            id,
            updateStaffRequest,
            cancellationToken
        );
        return result.ToActionResult(this);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteStaffMember(
        [FromRoute] int id,
        CancellationToken cancellationToken
    )
    {
        var result = await _staffService.DeleteStaffMemberAsync(id, cancellationToken);
        return result.ToActionResult(this);
    }
}
