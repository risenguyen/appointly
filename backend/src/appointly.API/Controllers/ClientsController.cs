using appointly.BLL.DTOs.Client;
using appointly.BLL.Services.IServices;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace appointly.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController(IClientService clientService) : ControllerBase
{
    private readonly IClientService _clientService = clientService;

    [HttpGet]
    [ProducesResponseType(typeof(List<ClientResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ClientResponse>>> GetClients(
        CancellationToken cancellationToken
    )
    {
        var result = await _clientService.GetClientsAsync(cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientResponse>> GetClient(
        [FromRoute] int id,
        CancellationToken cancellationToken
    )
    {
        var result = await _clientService.GetClientAsync(id, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClientResponse>> CreateClient(
        [FromBody] CreateClientRequest createClientRequest,
        CancellationToken cancellationToken
    )
    {
        var result = await _clientService.CreateClientAsync(createClientRequest, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClientResponse>> UpdateClient(
        [FromRoute] int id,
        [FromBody] UpdateClientRequest updateClientRequest,
        CancellationToken cancellationToken
    )
    {
        var result = await _clientService.UpdateClientAsync(
            id,
            updateClientRequest,
            cancellationToken
        );
        return result.ToActionResult(this);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteClient(
        [FromRoute] int id,
        CancellationToken cancellationToken
    )
    {
        var result = await _clientService.DeleteClientAsync(id, cancellationToken);
        return result.ToActionResult(this);
    }
}
