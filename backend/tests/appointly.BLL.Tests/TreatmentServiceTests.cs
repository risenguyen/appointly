using appointly.BLL.DTOs.Treatments;
using appointly.BLL.Services;
using appointly.DAL.Entities;
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
    public async Task CreateTreatmentAsync_ShouldReturnCreatedResponse()
    {
        // Arrange
        var request = new CreateTreatmentRequest()
        {
            Name = "Name",
            Description = "Desc",
            Price = 45,
            DurationInMinutes = 30,
        };
        _repository
            .Setup(x =>
                x.CreateTreatmentAsync(It.IsAny<Treatment>(), It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(
                (Treatment treatment, CancellationToken cancellationToken) =>
                {
                    treatment.Id = 42;
                    return treatment;
                }
            );

        // Act
        var result = await _sut.CreateTreatmentAsync(request, CancellationToken.None);

        // Assert
        Assert.IsType<Result<TreatmentResponse>>(result);
        Assert.Equal(ResultStatus.Created, result.Status);
        Assert.Equal(42, result.Value.Id);
        Assert.Equal(request.Name, result.Value.Name);
        Assert.Equal(request.Description, result.Value.Description);
        Assert.Equal(request.DurationInMinutes, result.Value.DurationInMinutes);
        Assert.Equal(request.Price, result.Value.Price);
        _repository.Verify(
            x =>
                x.CreateTreatmentAsync(
                    It.Is<Treatment>(t =>
                        t.Name == request.Name
                        && t.Description == request.Description
                        && t.Price == request.Price
                        && t.DurationInMinutes == request.DurationInMinutes
                    ),
                    It.IsAny<CancellationToken>()
                ),
            Times.Once
        );
    }

    [Fact]
    public async Task DeleteTreatmentByIdAsync_ShouldReturnSuccess_WhenTreatmentExists()
    {
        // Arrange
        var id = 1;
        _repository
            .Setup(x => x.DeleteTreatmentAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _sut.DeleteTreatmentAsync(id, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.Ok, result.Status);
        _repository.Verify(
            x => x.DeleteTreatmentAsync(id, It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task DeleteTreatmentByIdAsync_ShouldReturnNotFound_WhenTreatmentDoesNotExist()
    {
        // Arrange
        var id = 1;
        _repository
            .Setup(x => x.DeleteTreatmentAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _sut.DeleteTreatmentAsync(id, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
        _repository.Verify(
            x => x.DeleteTreatmentAsync(id, It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task GetTreatmentByIdAsync_ShouldReturnNotFound_WhenNull()
    {
        // Arrange
        var id = 2;
        _repository
            .Setup(x => x.GetTreatmentAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((int id, CancellationToken cancellationToken) => null);

        // Act
        var result = await _sut.GetTreatmentAsync(id, CancellationToken.None);

        // Assert
        Assert.Null(result.Value);
        Assert.Equal(ResultStatus.NotFound, result.Status);
        _repository.Verify(x => x.GetTreatmentAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetTreatmentByIdAsync_ShouldReturnSuccess_WhenFound()
    {
        // Arrange
        var id = 10;
        var expectedTreatment = new Treatment()
        {
            Id = id,
            Name = "Name",
            Description = "Desc",
            Price = 35,
            DurationInMinutes = 30,
        };
        _repository
            .Setup(x => x.GetTreatmentAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedTreatment);

        // Act
        var result = await _sut.GetTreatmentAsync(id, CancellationToken.None);

        // Assert
        Assert.IsType<Result<TreatmentResponse>>(result);
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.Equal(expectedTreatment.Id, result.Value.Id);
        Assert.Equal(expectedTreatment.Name, result.Value.Name);
        Assert.Equal(expectedTreatment.Description, result.Value.Description);
        Assert.Equal(expectedTreatment.DurationInMinutes, result.Value.DurationInMinutes);
        Assert.Equal(expectedTreatment.Price, result.Value.Price);
        _repository.Verify(x => x.GetTreatmentAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetAllTreatmentsAsync_ShouldReturnTreatmentList()
    {
        // Arrange
        var expectedTreatments = new List<Treatment>
        {
            new()
            {
                Id = 1,
                Name = "Treatment 1",
                Description = "Description 1",
                Price = 45,
                DurationInMinutes = 30,
            },
            new()
            {
                Id = 2,
                Name = "Treatment 2",
                Description = "Description 2",
                Price = 90,
                DurationInMinutes = 60,
            },
        };
        _repository
            .Setup(x => x.GetTreatmentsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedTreatments);

        // Act
        var result = await _sut.GetTreatmentsAsync(CancellationToken.None);

        // Assert
        Assert.IsType<Result<List<TreatmentResponse>>>(result);
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.Equal(2, result.Value.Count);
        foreach (var expected in expectedTreatments)
        {
            Assert.Contains(
                result.Value,
                actual =>
                    actual.Id == expected.Id
                    && actual.Name == expected.Name
                    && actual.Description == expected.Description
                    && actual.DurationInMinutes == expected.DurationInMinutes
                    && actual.Price == expected.Price
            );
        }
        _repository.Verify(x => x.GetTreatmentsAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
