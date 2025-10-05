using appointly.DAL.Entities;

namespace appointly.DAL.Repositories.IRepositories;

public interface IClientRepository
{
    Task<List<Client>> GetClientsAsync(CancellationToken cancellationToken);
    Task<Client?> GetClientAsync(int id, CancellationToken cancellationToken);
    Task<Client> CreateClientAsync(Client client, CancellationToken cancellationToken);
    Task<Client> UpdateClientAsync(Client client, CancellationToken cancellationToken);
    Task DeleteClientAsync(Client client, CancellationToken cancellationToken);
}
