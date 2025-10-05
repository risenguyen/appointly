using appointly.DAL.Context;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace appointly.DAL.Repositories;

public class ClientRepository(ApplicationDbContext context) : IClientRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Client>> GetClientsAsync(CancellationToken cancellationToken)
    {
        var clients = await _context.Clients.AsNoTracking().ToListAsync(cancellationToken);
        return clients;
    }

    public async Task<Client?> GetClientAsync(int id, CancellationToken cancellationToken)
    {
        var client = await _context.Clients.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);
        return client;
    }

    public async Task<Client> CreateClientAsync(Client client, CancellationToken cancellationToken)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<Client> UpdateClientAsync(Client client, CancellationToken cancellationToken)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task DeleteClientAsync(Client client, CancellationToken cancellationToken)
    {
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
