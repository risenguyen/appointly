using appointly.BLL.DTOs.Staff;
using appointly.BLL.Services;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;
using Ardalis.Result;
using Moq;

namespace appointly.BLL.Tests;

public class StaffServiceTests
{
    private readonly Mock<IStaffRepository> _staffRepositoryMock;
    private readonly StaffService _staffService;

    public StaffServiceTests()
    {
        _staffRepositoryMock = new Mock<IStaffRepository>();
        _staffService = new StaffService(_staffRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateStaffAsync_WithValidData_ShouldReturnCreatedResult()
    {
        // Arrange
        var createStaffRequest = new CreateStaffRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Phone = "1234567890",
        };

        var staff = new Staff
        {
            Id = 1,
            FirstName = createStaffRequest.FirstName,
            LastName = createStaffRequest.LastName,
            Email = createStaffRequest.Email,
            Phone = createStaffRequest.Phone,
        };

        _staffRepositoryMock
            .Setup(repo => repo.CreateStaffAsync(It.IsAny<Staff>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(staff);

        // Act
        var result = await _staffService.CreateStaffAsync(
            createStaffRequest,
            CancellationToken.None
        );

        // Assert
        Assert.Equal(ResultStatus.Created, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(staff.Id, result.Value.Id);
        Assert.Equal(createStaffRequest.FirstName, result.Value.FirstName);
        _staffRepositoryMock.Verify(
            repo => repo.CreateStaffAsync(It.IsAny<Staff>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task GetStaffAsync_WhenStaffExist_ShouldReturnListOfStaffResponses()
    {
        // Arrange
        var staffList = new List<Staff>
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

        _staffRepositoryMock
            .Setup(repo => repo.GetStaffAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(staffList);

        // Act
        var result = await _staffService.GetStaffAsync(CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(2, result.Value.Count);
        Assert.Equal("John", result.Value[0].FirstName);
        Assert.Equal("Jane", result.Value[1].FirstName);
        _staffRepositoryMock.Verify(
            repo => repo.GetStaffAsync(It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task GetStaffAsync_WhenNoStaffExist_ShouldReturnEmptyList()
    {
        // Arrange
        _staffRepositoryMock
            .Setup(repo => repo.GetStaffAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        var result = await _staffService.GetStaffAsync(CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value);
        _staffRepositoryMock.Verify(
            repo => repo.GetStaffAsync(It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
}
