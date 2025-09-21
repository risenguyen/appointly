using appointly.BLL.DTOs.Treatments;
using appointly.BLL.Services;
using appointly.DAL.Entities;
using appointly.DAL.Enums;
using appointly.DAL.Repositories.IRepositories;
using Ardalis.Result;
using Moq;

namespace appointly.BLL.Tests;

public class TreatmentServiceTests
{
    private readonly Mock<ITreatmentRepository> _repository = new();
    private readonly TreatmentService _sut;

    public TreatmentServiceTests()
    {
        _sut = new TreatmentService(_repository.Object);
    }

    [Fact]
    public async Task CreateTreatmentAsync_ShouldReturnCreatedResult_WithTreatmentResponse()
    {
        // Arrange
        var createRequest = new CreateTreatmentRequest
        {
            Name = "Test Treatment",
            Description = "Test Description",
            Price = 50,
            DurationInMinutes = 60,
            TreatmentType = TreatmentType.Hair,
        };
        var treatment = new Treatment
        {
            Id = 1,
            Name = createRequest.Name,
            Description = createRequest.Description,
            Price = createRequest.Price,
            DurationInMinutes = createRequest.DurationInMinutes,
            TreatmentType = createRequest.TreatmentType,
        };
        _repository
            .Setup(r =>
                r.CreateTreatmentAsync(It.IsAny<Treatment>(), It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(treatment);

        // Act
        var result = await _sut.CreateTreatmentAsync(createRequest, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Created, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(treatment.Id, result.Value.Id);
        Assert.Equal(treatment.Name, result.Value.Name);
        Assert.Equal($"/api/Treatments/{treatment.Id}", result.Location);
        _repository.Verify(
            r =>
                r.CreateTreatmentAsync(
                    It.Is<Treatment>(t =>
                        t.Name == createRequest.Name
                        && t.Description == createRequest.Description
                        && t.Price == createRequest.Price
                        && t.DurationInMinutes == createRequest.DurationInMinutes
                        && t.TreatmentType == createRequest.TreatmentType
                    ),
                    CancellationToken.None
                ),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdateTreatmentAsync_ShouldReturnSuccessResult_WithUpdatedTreatmentResponse_WhenTreatmentExists()
    {
        // Arrange
        var treatmentId = 1;
        var updateRequest = new UpdateTreatmentRequest
        {
            Name = "Updated Treatment",
            Description = "Updated Description",
            Price = 75,
            DurationInMinutes = 90,
            TreatmentType = TreatmentType.Massage,
        };
        var existingTreatment = new Treatment
        {
            Id = treatmentId,
            Name = "Old Treatment",
            Description = "Old Description",
            Price = 50,
            DurationInMinutes = 60,
            TreatmentType = TreatmentType.Nails,
        };
        var updatedTreatment = new Treatment
        {
            Id = treatmentId,
            Name = updateRequest.Name,
            Description = updateRequest.Description,
            Price = updateRequest.Price,
            DurationInMinutes = updateRequest.DurationInMinutes,
            TreatmentType = updateRequest.TreatmentType,
        };

        _repository
            .Setup(r => r.GetTreatmentAsync(treatmentId, CancellationToken.None))
            .ReturnsAsync(existingTreatment);
        _repository
            .Setup(r =>
                r.UpdateTreatmentAsync(It.IsAny<Treatment>(), It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(updatedTreatment);

        // Act
        var result = await _sut.UpdateTreatmentAsync(
            treatmentId,
            updateRequest,
            CancellationToken.None
        );

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(treatmentId, result.Value.Id);
        Assert.Equal(updateRequest.Name, result.Value.Name);
        Assert.Equal(updateRequest.Description, result.Value.Description);
        Assert.Equal(updateRequest.Price, result.Value.Price);
        Assert.Equal(updateRequest.DurationInMinutes, result.Value.DurationInMinutes);
        Assert.Equal(updateRequest.TreatmentType, result.Value.TreatmentType);
        _repository.Verify(
            r => r.GetTreatmentAsync(treatmentId, CancellationToken.None),
            Times.Once
        );
        _repository.Verify(
            r =>
                r.UpdateTreatmentAsync(
                    It.Is<Treatment>(t =>
                        t.Id == treatmentId
                        && t.Name == updateRequest.Name
                        && t.Description == updateRequest.Description
                        && t.Price == updateRequest.Price
                        && t.DurationInMinutes == updateRequest.DurationInMinutes
                        && t.TreatmentType == updateRequest.TreatmentType
                    ),
                    CancellationToken.None
                ),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdateTreatmentAsync_ShouldReturnNotFound_WhenTreatmentDoesNotExist()
    {
        // Arrange
        var treatmentId = 1;
        var updateRequest = new UpdateTreatmentRequest
        {
            Name = "Updated Treatment",
            Description = "Updated Description",
            Price = 75,
            DurationInMinutes = 90,
            TreatmentType = TreatmentType.Hair,
        };
        _repository
            .Setup(r => r.GetTreatmentAsync(treatmentId, CancellationToken.None))
            .ReturnsAsync((Treatment?)null);

        // Act
        var result = await _sut.UpdateTreatmentAsync(
            treatmentId,
            updateRequest,
            CancellationToken.None
        );

        // Assert
        Assert.Equal(ResultStatus.NotFound, result.Status);
        Assert.Contains($"Treatment with ID {treatmentId} cannot be found.", result.Errors);
        _repository.Verify(
            r => r.GetTreatmentAsync(treatmentId, CancellationToken.None),
            Times.Once
        );
        _repository.Verify(
            r => r.UpdateTreatmentAsync(It.IsAny<Treatment>(), It.IsAny<CancellationToken>()),
            Times.Never
        );
    }

    [Fact]
    public async Task DeleteTreatmentAsync_ShouldReturnSuccess_WhenTreatmentExists()
    {
        // Arrange
        var treatmentId = 1;
        var existingTreatment = new Treatment
        {
            Id = treatmentId,
            Name = "Test",
            Description = "Test Desc",
            Price = 10,
            DurationInMinutes = 10,
            TreatmentType = TreatmentType.Nails,
        };
        _repository
            .Setup(r => r.GetTreatmentAsync(treatmentId, CancellationToken.None))
            .ReturnsAsync(existingTreatment);
        _repository
            .Setup(r => r.DeleteTreatmentAsync(existingTreatment, CancellationToken.None))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.DeleteTreatmentAsync(treatmentId, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        _repository.Verify(
            r => r.GetTreatmentAsync(treatmentId, CancellationToken.None),
            Times.Once
        );
        _repository.Verify(
            r => r.DeleteTreatmentAsync(existingTreatment, CancellationToken.None),
            Times.Once
        );
    }

    [Fact]
    public async Task DeleteTreatmentAsync_ShouldReturnNotFound_WhenTreatmentDoesNotExist()
    {
        // Arrange
        var treatmentId = 1;
        _repository
            .Setup(r => r.GetTreatmentAsync(treatmentId, CancellationToken.None))
            .ReturnsAsync((Treatment?)null);

        // Act
        var result = await _sut.DeleteTreatmentAsync(treatmentId, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.NotFound, result.Status);
        Assert.Contains($"Treatment with ID {treatmentId} cannot be found.", result.Errors);
        _repository.Verify(
            r => r.GetTreatmentAsync(treatmentId, CancellationToken.None),
            Times.Once
        );
        _repository.Verify(
            r => r.DeleteTreatmentAsync(It.IsAny<Treatment>(), It.IsAny<CancellationToken>()),
            Times.Never
        );
    }

    [Fact]
    public async Task GetTreatmentAsync_ShouldReturnSuccessResult_WithTreatmentResponse_WhenTreatmentExists()
    {
        // Arrange
        var treatmentId = 1;
        var treatment = new Treatment
        {
            Id = treatmentId,
            Name = "Test Treatment",
            Description = "Test Description",
            Price = 50,
            DurationInMinutes = 60,
            TreatmentType = TreatmentType.Hair,
        };
        _repository
            .Setup(r => r.GetTreatmentAsync(treatmentId, CancellationToken.None))
            .ReturnsAsync(treatment);

        // Act
        var result = await _sut.GetTreatmentAsync(treatmentId, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(treatment.Id, result.Value.Id);
        Assert.Equal(treatment.Name, result.Value.Name);
        _repository.Verify(
            r => r.GetTreatmentAsync(treatmentId, CancellationToken.None),
            Times.Once
        );
    }

    [Fact]
    public async Task GetTreatmentAsync_ShouldReturnNotFound_WhenTreatmentDoesNotExist()
    {
        // Arrange
        var treatmentId = 1;
        _repository
            .Setup(r => r.GetTreatmentAsync(treatmentId, CancellationToken.None))
            .ReturnsAsync((Treatment?)null);

        // Act
        var result = await _sut.GetTreatmentAsync(treatmentId, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.NotFound, result.Status);
        Assert.Contains($"Treatment with ID {treatmentId} cannot be found.", result.Errors);
        _repository.Verify(
            r => r.GetTreatmentAsync(treatmentId, CancellationToken.None),
            Times.Once
        );
    }

    [Fact]
    public async Task GetTreatmentsAsync_ShouldReturnSuccessResult_WithListOfTreatmentResponses()
    {
        // Arrange
        var treatments = new List<Treatment>
        {
            new()
            {
                Id = 1,
                Name = "Treatment 1",
                Description = "Desc 1",
                Price = 10,
                DurationInMinutes = 10,
                TreatmentType = TreatmentType.Hair,
            },
            new()
            {
                Id = 2,
                Name = "Treatment 2",
                Description = "Desc 2",
                Price = 20,
                DurationInMinutes = 20,
                TreatmentType = TreatmentType.Nails,
            },
        };
        _repository
            .Setup(r => r.GetTreatmentsAsync(CancellationToken.None))
            .ReturnsAsync(treatments);

        // Act
        var result = await _sut.GetTreatmentsAsync(CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(2, result.Value.Count);
        Assert.Equal(treatments.First().Name, result.Value.First().Name);
        Assert.Equal(treatments.Last().Name, result.Value.Last().Name);
        _repository.Verify(r => r.GetTreatmentsAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetTreatmentsAsync_ShouldReturnSuccessResult_WithEmptyList_WhenNoTreatmentsExist()
    {
        // Arrange
        _repository
            .Setup(r => r.GetTreatmentsAsync(CancellationToken.None))
            .ReturnsAsync(new List<Treatment>());

        // Act
        var result = await _sut.GetTreatmentsAsync(CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value);
        _repository.Verify(r => r.GetTreatmentsAsync(CancellationToken.None), Times.Once);
    }
}
