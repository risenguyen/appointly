using appointly.BLL.DTOs.Treatments;
using appointly.BLL.Services;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;
using Ardalis.Result;
using Moq;

namespace appointly.BLL.Tests;

public class TreatmentServiceTests
{
    private readonly Mock<ITreatmentRepository> _repo = new();
    private readonly TreatmentService _sut;

    public TreatmentServiceTests()
    {
        _sut = new TreatmentService(_repo.Object);
    }

    [Fact]
    public async Task CreateTreatmentAsync_ReturnsCreatedResponse()
    {
        // Arrange
        var request = new CreateTreatmentRequest()
        {
            Name = "Name",
            Description = "Desc",
            DurationInMinutes = 30,
            Price = 45,
        };
        _repo
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
        _repo.Verify(
            x =>
                x.CreateTreatmentAsync(
                    It.Is<Treatment>(t =>
                        t.Name == request.Name
                        && t.Description == request.Description
                        && t.DurationInMinutes == request.DurationInMinutes
                        && t.Price == request.Price
                    ),
                    It.IsAny<CancellationToken>()
                ),
            Times.Once
        );
    }

    [Fact]
    public async Task GetTreatmentByIdAsync_ReturnsNotFound_WhenNull()
    {
        // Arrange
        var id = 2;
        _repo
            .Setup(x => x.GetTreatmentByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((int id, CancellationToken cancellationToken) => null);

        // Act
        var result = await _sut.GetTreatmentByIdAsync(id, CancellationToken.None);

        // Assert
        Assert.Null(result.Value);
        Assert.Equal(ResultStatus.NotFound, result.Status);
        _repo.Verify(x => x.GetTreatmentByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetTreatmentByIdAsync_ReturnsSuccess_WhenFound()
    {
        // Arrange
        var id = 10;
        var expected = new Treatment()
        {
            Id = id,
            Name = "Name",
            Description = "Desc",
            DurationInMinutes = 30,
            Price = 35,
        };
        _repo
            .Setup(x => x.GetTreatmentByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _sut.GetTreatmentByIdAsync(id, CancellationToken.None);

        // Assert
        Assert.IsType<Result<TreatmentResponse>>(result);
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.Equal(expected.Id, result.Value.Id);
        Assert.Equal(expected.Name, result.Value.Name);
        Assert.Equal(expected.Description, result.Value.Description);
        Assert.Equal(expected.DurationInMinutes, result.Value.DurationInMinutes);
        Assert.Equal(expected.Price, result.Value.Price);
        _repo.Verify(x => x.GetTreatmentByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }
}
