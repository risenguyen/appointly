using appointly.DAL.Context;
using appointly.DAL.Entities;
using appointly.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace appointly.DAL.Tests;

public class TreatmentRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly TreatmentRepository _sut;

    public TreatmentRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _sut = new TreatmentRepository(_context);
    }

    [Fact]
    public async Task CreateTreatmentAsync_ShouldAddTreatmentToDatabase()
    {
        // Arrange
        var treatment = new Treatment()
        {
            Name = "Name",
            Description = "Desc",
            DurationInMinutes = 30,
            Price = 45,
        };

        // Act
        var createdTreatment = await _sut.CreateTreatmentAsync(treatment, CancellationToken.None);
        var treatmentInDb = await _context.Treatments.FindAsync(createdTreatment.Id);

        // Assert
        Assert.NotNull(createdTreatment);
        Assert.NotNull(treatmentInDb);
        Assert.True(createdTreatment.Id > 0);
        Assert.Equal(treatment.Name, treatmentInDb.Name);
        Assert.Equal(treatment.Description, treatmentInDb.Description);
        Assert.Equal(treatment.DurationInMinutes, treatmentInDb.DurationInMinutes);
        Assert.Equal(treatment.Price, treatmentInDb.Price);
    }

    [Fact]
    public async Task GetTreatmentByIdAsync_ShouldReturnTreatment_WhenExists()
    {
        // Arrange
        var expectedTreatment = new Treatment()
        {
            Name = "Name",
            Description = "Desc",
            DurationInMinutes = 30,
            Price = 45,
        };
        await _context.Treatments.AddAsync(expectedTreatment);
        await _context.SaveChangesAsync();

        // Act
        var retrievedTreatment = await _sut.GetTreatmentByIdAsync(
            expectedTreatment.Id,
            CancellationToken.None
        );

        // Assert
        Assert.NotNull(retrievedTreatment);
        Assert.Equal(expectedTreatment.Id, retrievedTreatment.Id);
        Assert.Equal(expectedTreatment.Name, retrievedTreatment.Name);
        Assert.Equal(expectedTreatment.Description, retrievedTreatment.Description);
        Assert.Equal(expectedTreatment.DurationInMinutes, retrievedTreatment.DurationInMinutes);
        Assert.Equal(expectedTreatment.Price, retrievedTreatment.Price);
    }

    [Fact]
    public async Task GetTreatmentByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        var nonExistentId = 999;

        // Act
        var retrievedTreatment = await _sut.GetTreatmentByIdAsync(
            nonExistentId,
            CancellationToken.None
        );

        // Assert
        Assert.Null(retrievedTreatment);
    }
}
