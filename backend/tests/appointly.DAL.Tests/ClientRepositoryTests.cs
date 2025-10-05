using appointly.DAL.Context;
using appointly.DAL.Entities;
using appointly.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace appointly.DAL.Tests;

public class ClientRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly ClientRepository _sut;

    public ClientRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _sut = new ClientRepository(_context);
    }

    [Fact]
    public async Task CreateClientAsync_ShouldAddClientToDatabase()
    {
        // Arrange
        var client = new Client
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890",
        };

        // Act
        var result = await _sut.CreateClientAsync(client, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Id);
        var clientInDb = await _context.Clients.FindAsync(result.Id);
        Assert.NotNull(clientInDb);
        Assert.Equal(client.FirstName, clientInDb!.FirstName);
        Assert.Equal(client.LastName, clientInDb.LastName);
        Assert.Equal(client.Email, clientInDb.Email);
        Assert.Equal(client.PhoneNumber, clientInDb.PhoneNumber);
    }

    [Fact]
    public async Task GetClientsAsync_ShouldReturnAllClients()
    {
        // Arrange
        var client1 = new Client { FirstName = "John", LastName = "Doe" };
        var client2 = new Client { FirstName = "Jane", LastName = "Smith" };
        await _context.Clients.AddRangeAsync(client1, client2);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetClientsAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, c => c.FirstName == "John");
        Assert.Contains(result, c => c.FirstName == "Jane");
    }

    [Fact]
    public async Task GetClientsAsync_ShouldReturnEmptyList_WhenNoClientsExist()
    {
        // Arrange

        // Act
        var result = await _sut.GetClientsAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetClientAsync_ShouldReturnClient_WhenClientExists()
    {
        // Arrange
        var client = new Client { FirstName = "John", LastName = "Doe" };
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetClientAsync(client.Id, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(client.Id, result.Id);
        Assert.Equal(client.FirstName, result.FirstName);
    }

    [Fact]
    public async Task GetClientAsync_ShouldReturnNull_WhenClientDoesNotExist()
    {
        // Arrange

        // Act
        var result = await _sut.GetClientAsync(1, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteClientAsync_ShouldRemoveClientFromDatabase()
    {
        // Arrange
        var client = new Client { FirstName = "John", LastName = "Doe" };
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();

        // Act
        await _sut.DeleteClientAsync(client, CancellationToken.None);

        // Assert
        var clientInDb = await _context.Clients.FindAsync(client.Id);
        Assert.Null(clientInDb);
    }

    [Fact]
    public async Task UpdateClientAsync_ShouldUpdateClientInDatabase()
    {
        // Arrange
        var client = new Client
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890",
        };
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();

        client.FirstName = "Jane";
        client.Email = "jane.doe@example.com";

        // Act
        var result = await _sut.UpdateClientAsync(client, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Jane", result.FirstName);
        Assert.Equal("jane.doe@example.com", result.Email);

        var clientInDb = await _context.Clients.FindAsync(client.Id);
        Assert.NotNull(clientInDb);
        Assert.Equal("Jane", clientInDb!.FirstName);
        Assert.Equal("jane.doe@example.com", clientInDb.Email);
    }
}
