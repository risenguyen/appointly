using appointly.BLL.DTOs.Client;
using Ardalis.Result;

namespace appointly.BLL.Services.IServices;

public interface IClientService
{
    Task<Result<List<ClientResponse>>> GetClientsAsync(CancellationToken cancellationToken);
    Task<Result<ClientResponse>> GetClientAsync(int id, CancellationToken cancellationToken);
    Task<Result<ClientResponse>> CreateClientAsync(
        CreateClientRequest createClientRequest,
        CancellationToken cancellationToken
    );
    Task<Result<ClientResponse>> UpdateClientAsync(
        int id,
        UpdateClientRequest updateClientRequest,
        CancellationToken cancellationToken
    );
    Task<Result> DeleteClientAsync(int id, CancellationToken cancellationToken);
}
