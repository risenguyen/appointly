using appointly.BLL.DTOs.Client;
using appointly.BLL.Services.IServices;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;
using Ardalis.Result;

namespace appointly.BLL.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<Result<List<ClientResponse>>> GetClientsAsync(
        CancellationToken cancellationToken
    )
    {
        var clients = await _clientRepository.GetClientsAsync(cancellationToken);
        var response = clients
            .Select(c => new ClientResponse()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
            })
            .ToList();
        return Result.Success(response);
    }

    public async Task<Result<ClientResponse>> GetClientAsync(
        int id,
        CancellationToken cancellationToken
    )
    {
        var client = await _clientRepository.GetClientAsync(id, cancellationToken);
        if (client == null)
        {
            return Result.NotFound($"Client with ID {id} cannot be found.");
        }

        var response = new ClientResponse
        {
            Id = client.Id,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Email = client.Email,
            PhoneNumber = client.PhoneNumber,
        };
        return response;
    }

    public async Task<Result<ClientResponse>> CreateClientAsync(
        CreateClientRequest createClientRequest,
        CancellationToken cancellationToken
    )
    {
        var client = new Client
        {
            FirstName = createClientRequest.FirstName,
            LastName = createClientRequest.LastName,
            Email = createClientRequest.Email,
            PhoneNumber = createClientRequest.PhoneNumber,
        };
        var newClient = await _clientRepository.CreateClientAsync(client, cancellationToken);

        var response = new ClientResponse
        {
            Id = newClient.Id,
            FirstName = newClient.FirstName,
            LastName = newClient.LastName,
            Email = newClient.Email,
            PhoneNumber = newClient.PhoneNumber,
        };
        return Result.Created(response, $"/api/Clients/{response.Id}");
    }

    public async Task<Result<ClientResponse>> UpdateClientAsync(
        int id,
        UpdateClientRequest updateClientRequest,
        CancellationToken cancellationToken
    )
    {
        var client = await _clientRepository.GetClientAsync(id, cancellationToken);
        if (client == null)
        {
            return Result.NotFound($"Client with ID {id} cannot be found.");
        }

        client.FirstName = updateClientRequest.FirstName;
        client.LastName = updateClientRequest.LastName;
        client.Email = updateClientRequest.Email;
        client.PhoneNumber = updateClientRequest.PhoneNumber;

        var updatedClient = await _clientRepository.UpdateClientAsync(client, cancellationToken);

        var response = new ClientResponse
        {
            Id = updatedClient.Id,
            FirstName = updatedClient.FirstName,
            LastName = updatedClient.LastName,
            Email = updatedClient.Email,
            PhoneNumber = updatedClient.PhoneNumber,
        };
        return Result.Success(response);
    }

    public async Task<Result> DeleteClientAsync(int id, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientAsync(id, cancellationToken);
        if (client == null)
        {
            return Result.NotFound($"Client with ID {id} cannot be found.");
        }

        await _clientRepository.DeleteClientAsync(client, cancellationToken);
        return Result.Success();
    }
}
