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
    public async Task CreateStaffMemberAsync_WithValidData_ShouldReturnCreatedResult()
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
            .Setup(repo =>
                repo.CreateStaffMemberAsync(It.IsAny<Staff>(), It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(staff);

        // Act
        var result = await _staffService.CreateStaffMemberAsync(
            createStaffRequest,
            CancellationToken.None
        );

        // Assert
        Assert.Equal(ResultStatus.Created, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(staff.Id, result.Value.Id);
        Assert.Equal(createStaffRequest.FirstName, result.Value.FirstName);
        _staffRepositoryMock.Verify(
            repo => repo.CreateStaffMemberAsync(It.IsAny<Staff>(), It.IsAny<CancellationToken>()),
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

    [Fact]
    public async Task GetStaffMemberAsync_WhenStaffExists_ShouldReturnStaffResponse()
    {
        // Arrange
        var staff = new Staff
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Phone = "1234567890",
        };

        _staffRepositoryMock
            .Setup(repo => repo.GetStaffMemberAsync(staff.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(staff);

        // Act
        var result = await _staffService.GetStaffMemberAsync(staff.Id, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(staff.Id, result.Value.Id);
        Assert.Equal(staff.FirstName, result.Value.FirstName);
        _staffRepositoryMock.Verify(
            repo => repo.GetStaffMemberAsync(staff.Id, It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task GetStaffMemberAsync_WhenStaffDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var staffId = 1;
        _staffRepositoryMock
            .Setup(repo => repo.GetStaffMemberAsync(staffId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Staff?)null);

        // Act
        var result = await _staffService.GetStaffMemberAsync(staffId, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.NotFound, result.Status);
        _staffRepositoryMock.Verify(
            repo => repo.GetStaffMemberAsync(staffId, It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task DeleteStaffMemberAsync_WhenStaffExists_ShouldReturnSuccess()
    {
        // Arrange
        var staff = new Staff
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
        };

        _staffRepositoryMock
            .Setup(repo => repo.GetStaffMemberAsync(staff.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(staff);
        _staffRepositoryMock
            .Setup(repo => repo.DeleteStaffMemberAsync(staff, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _staffService.DeleteStaffMemberAsync(staff.Id, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        _staffRepositoryMock.Verify(
            repo => repo.GetStaffMemberAsync(staff.Id, It.IsAny<CancellationToken>()),
            Times.Once
        );
        _staffRepositoryMock.Verify(
            repo => repo.DeleteStaffMemberAsync(staff, It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task DeleteStaffMemberAsync_WhenStaffDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var staffId = 1;
        _staffRepositoryMock
            .Setup(repo => repo.GetStaffMemberAsync(staffId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Staff?)null);

        // Act
        var result = await _staffService.DeleteStaffMemberAsync(staffId, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.NotFound, result.Status);
        _staffRepositoryMock.Verify(
            repo => repo.GetStaffMemberAsync(staffId, It.IsAny<CancellationToken>()),
            Times.Once
        );
        _staffRepositoryMock.Verify(
            repo => repo.DeleteStaffMemberAsync(It.IsAny<Staff>(), It.IsAny<CancellationToken>()),
            Times.Never
        );
    }

    [Fact]
    public async Task UpdateStaffMemberAsync_WithValidData_ShouldReturnSuccessResult()
    {
        // Arrange
        var staffId = 1;
        var updateStaffRequest = new UpdateStaffRequest
        {
            FirstName = "Johnathan",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Phone = "1234567890",
        };

        var existingStaff = new Staff
        {
            Id = staffId,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Phone = "1234567890",
        };

        _staffRepositoryMock
            .Setup(repo => repo.GetStaffMemberAsync(staffId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingStaff);

        _staffRepositoryMock
            .Setup(repo =>
                repo.UpdateStaffMemberAsync(It.IsAny<Staff>(), It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(
                (Staff staff, CancellationToken token) =>
                {
                    existingStaff.FirstName = staff.FirstName;
                    return existingStaff;
                }
            );

        // Act
        var result = await _staffService.UpdateStaffMemberAsync(
            staffId,
            updateStaffRequest,
            CancellationToken.None
        );

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(staffId, result.Value.Id);
        Assert.Equal(updateStaffRequest.FirstName, result.Value.FirstName);
        _staffRepositoryMock.Verify(
            repo => repo.GetStaffMemberAsync(staffId, It.IsAny<CancellationToken>()),
            Times.Once
        );
        _staffRepositoryMock.Verify(
            repo => repo.UpdateStaffMemberAsync(It.IsAny<Staff>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdateStaffMemberAsync_WithPartialData_ShouldUpdateOnlyProvidedFields()
    {
        // Arrange
        var staffId = 1;
        var updateStaffRequest = new UpdateStaffRequest { FirstName = "Johnathan" };

        var existingStaff = new Staff
        {
            Id = staffId,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Phone = "1234567890",
        };

        _staffRepositoryMock
            .Setup(repo => repo.GetStaffMemberAsync(staffId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingStaff);

        _staffRepositoryMock
            .Setup(repo =>
                repo.UpdateStaffMemberAsync(It.IsAny<Staff>(), It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(
                (Staff staff, CancellationToken token) =>
                {
                    existingStaff.FirstName = staff.FirstName;
                    return existingStaff;
                }
            );

        // Act
        var result = await _staffService.UpdateStaffMemberAsync(
            staffId,
            updateStaffRequest,
            CancellationToken.None
        );

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(staffId, result.Value.Id);
        Assert.Equal(updateStaffRequest.FirstName, result.Value.FirstName);
        Assert.Equal(existingStaff.LastName, result.Value.LastName); // Should not change
        _staffRepositoryMock.Verify(
            repo => repo.GetStaffMemberAsync(staffId, It.IsAny<CancellationToken>()),
            Times.Once
        );
        _staffRepositoryMock.Verify(
            repo => repo.UpdateStaffMemberAsync(It.IsAny<Staff>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdateStaffMemberAsync_WhenStaffDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var staffId = 1;
        var updateStaffRequest = new UpdateStaffRequest();

        _staffRepositoryMock
            .Setup(repo => repo.GetStaffMemberAsync(staffId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Staff?)null);

        // Act
        var result = await _staffService.UpdateStaffMemberAsync(
            staffId,
            updateStaffRequest,
            CancellationToken.None
        );

        // Assert
        Assert.Equal(ResultStatus.NotFound, result.Status);
        _staffRepositoryMock.Verify(
            repo => repo.GetStaffMemberAsync(staffId, It.IsAny<CancellationToken>()),
            Times.Once
        );
        _staffRepositoryMock.Verify(
            repo => repo.UpdateStaffMemberAsync(It.IsAny<Staff>(), It.IsAny<CancellationToken>()),
            Times.Never
        );
    }
}
