using appointly.BLL.DTOs.Client;
using appointly.BLL.Services;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;
using Ardalis.Result;
using Moq;

namespace appointly.BLL.Tests;

public class ClientServiceTests
{
    private readonly Mock<IClientRepository> _clientRepositoryMock;
    private readonly ClientService _clientService;

    public ClientServiceTests()
    {
        _clientRepositoryMock = new Mock<IClientRepository>();
        _clientService = new ClientService(_clientRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateClientAsync_WithValidData_ShouldReturnCreatedResult()
    {
        // Arrange
        var createClientRequest = new CreateClientRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890",
        };

        var client = new Client
        {
            Id = 1,
            FirstName = createClientRequest.FirstName,
            LastName = createClientRequest.LastName,
            Email = createClientRequest.Email,
            PhoneNumber = createClientRequest.PhoneNumber,
        };

        _clientRepositoryMock
            .Setup(repo =>
                repo.CreateClientAsync(It.IsAny<Client>(), It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(client);

        // Act
        var result = await _clientService.CreateClientAsync(
            createClientRequest,
            CancellationToken.None
        );

        // Assert
        Assert.Equal(ResultStatus.Created, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(client.Id, result.Value.Id);
        Assert.Equal(createClientRequest.FirstName, result.Value.FirstName);
        _clientRepositoryMock.Verify(
            repo => repo.CreateClientAsync(It.IsAny<Client>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task GetClientsAsync_WhenClientsExist_ShouldReturnListOfClientResponses()
    {
        // Arrange
        var clientList = new List<Client>
        {
            new()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
            },
            new()
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
            },
        };

        _clientRepositoryMock
            .Setup(repo => repo.GetClientsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(clientList);

        // Act
        var result = await _clientService.GetClientsAsync(CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(2, result.Value.Count);
        Assert.Equal("John", result.Value[0].FirstName);
        Assert.Equal("Jane", result.Value[1].FirstName);
        _clientRepositoryMock.Verify(
            repo => repo.GetClientsAsync(It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task GetClientsAsync_WhenNoClientsExist_ShouldReturnEmptyList()
    {
        // Arrange
        _clientRepositoryMock
            .Setup(repo => repo.GetClientsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        var result = await _clientService.GetClientsAsync(CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value);
        _clientRepositoryMock.Verify(
            repo => repo.GetClientsAsync(It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task GetClientAsync_WhenClientExists_ShouldReturnClientResponse()
    {
        // Arrange
        var client = new Client
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890",
        };

        _clientRepositoryMock
            .Setup(repo => repo.GetClientAsync(client.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(client);

        // Act
        var result = await _clientService.GetClientAsync(client.Id, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(client.Id, result.Value.Id);
        Assert.Equal(client.FirstName, result.Value.FirstName);
        _clientRepositoryMock.Verify(
            repo => repo.GetClientAsync(client.Id, It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task GetClientAsync_WhenClientDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var clientId = 1;
        _clientRepositoryMock
            .Setup(repo => repo.GetClientAsync(clientId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Client?)null);

        // Act
        var result = await _clientService.GetClientAsync(clientId, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.NotFound, result.Status);
        _clientRepositoryMock.Verify(
            repo => repo.GetClientAsync(clientId, It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task DeleteClientAsync_WhenClientExists_ShouldReturnSuccess()
    {
        // Arrange
        var client = new Client
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
        };

        _clientRepositoryMock
            .Setup(repo => repo.GetClientAsync(client.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(client);
        _clientRepositoryMock
            .Setup(repo => repo.DeleteClientAsync(client, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _clientService.DeleteClientAsync(client.Id, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        _clientRepositoryMock.Verify(
            repo => repo.GetClientAsync(client.Id, It.IsAny<CancellationToken>()),
            Times.Once
        );
        _clientRepositoryMock.Verify(
            repo => repo.DeleteClientAsync(client, It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task DeleteClientAsync_WhenClientDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var clientId = 1;
        _clientRepositoryMock
            .Setup(repo => repo.GetClientAsync(clientId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Client?)null);

        // Act
        var result = await _clientService.DeleteClientAsync(clientId, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.NotFound, result.Status);
        _clientRepositoryMock.Verify(
            repo => repo.GetClientAsync(clientId, It.IsAny<CancellationToken>()),
            Times.Once
        );
        _clientRepositoryMock.Verify(
            repo => repo.DeleteClientAsync(It.IsAny<Client>(), It.IsAny<CancellationToken>()),
            Times.Never
        );
    }

    [Fact]
    public async Task UpdateClientAsync_WithValidData_ShouldReturnSuccessResult()
    {
        // Arrange
        var clientId = 1;
        var updateClientRequest = new UpdateClientRequest
        {
            FirstName = "Johnathan",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890",
        };

        var existingClient = new Client
        {
            Id = clientId,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890",
        };

        _clientRepositoryMock
            .Setup(repo => repo.GetClientAsync(clientId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingClient);

        _clientRepositoryMock
            .Setup(repo =>
                repo.UpdateClientAsync(It.IsAny<Client>(), It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(
                (Client client, CancellationToken token) =>
                {
                    existingClient.FirstName = client.FirstName;
                    return existingClient;
                }
            );

        // Act
        var result = await _clientService.UpdateClientAsync(
            clientId,
            updateClientRequest,
            CancellationToken.None
        );

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(clientId, result.Value.Id);
        Assert.Equal(updateClientRequest.FirstName, result.Value.FirstName);
        _clientRepositoryMock.Verify(
            repo => repo.GetClientAsync(clientId, It.IsAny<CancellationToken>()),
            Times.Once
        );
        _clientRepositoryMock.Verify(
            repo => repo.UpdateClientAsync(It.IsAny<Client>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdateClientAsync_WithPartialData_ShouldUpdateOnlyProvidedFields()
    {
        // Arrange
        var clientId = 1;
        var updateClientRequest = new UpdateClientRequest
        {
            FirstName = "Johnathan",
            LastName = "Smith",
            Email = "",
            PhoneNumber = "",
        };

        var existingClient = new Client
        {
            Id = clientId,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890",
        };

        _clientRepositoryMock
            .Setup(repo => repo.GetClientAsync(clientId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingClient);

        _clientRepositoryMock
            .Setup(repo =>
                repo.UpdateClientAsync(It.IsAny<Client>(), It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(
                (Client client, CancellationToken token) =>
                {
                    existingClient.FirstName = client.FirstName;
                    return existingClient;
                }
            );

        // Act
        var result = await _clientService.UpdateClientAsync(
            clientId,
            updateClientRequest,
            CancellationToken.None
        );

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(clientId, result.Value.Id);
        Assert.Equal(updateClientRequest.FirstName, result.Value.FirstName);
        Assert.Equal(existingClient.LastName, result.Value.LastName); // Should not change
        _clientRepositoryMock.Verify(
            repo => repo.GetClientAsync(clientId, It.IsAny<CancellationToken>()),
            Times.Once
        );
        _clientRepositoryMock.Verify(
            repo => repo.UpdateClientAsync(It.IsAny<Client>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdateClientAsync_WhenClientDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var clientId = 1;
        var updateClientRequest = new UpdateClientRequest
        {
            FirstName = "Johnathan",
            LastName = "Smith",
            Email = "",
            PhoneNumber = "",
        };

        _clientRepositoryMock
            .Setup(repo => repo.GetClientAsync(clientId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Client?)null);

        // Act
        var result = await _clientService.UpdateClientAsync(
            clientId,
            updateClientRequest,
            CancellationToken.None
        );

        // Assert
        Assert.Equal(ResultStatus.NotFound, result.Status);
        _clientRepositoryMock.Verify(
            repo => repo.GetClientAsync(clientId, It.IsAny<CancellationToken>()),
            Times.Once
        );
        _clientRepositoryMock.Verify(
            repo => repo.UpdateClientAsync(It.IsAny<Client>(), It.IsAny<CancellationToken>()),
            Times.Never
        );
    }
}
