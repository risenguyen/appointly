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
        var treatment = new Treatment
        {
            Name = "Test Treatment",
            Description = "Test Description",
            DurationInMinutes = 60,
            Price = 50,
        };

        // Act
        var result = await _sut.CreateTreatmentAsync(treatment, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Id);
        var treatmentInDb = await _context.Treatments.FindAsync(result.Id);
        Assert.NotNull(treatmentInDb);
        Assert.Equal(treatment.Name, treatmentInDb!.Name);
        Assert.Equal(treatment.Description, treatmentInDb.Description);
        Assert.Equal(treatment.DurationInMinutes, treatmentInDb.DurationInMinutes);
        Assert.Equal(treatment.Price, treatmentInDb.Price);
    }

    [Fact]
    public async Task UpdateTreatmentAsync_ShouldUpdateTreatmentInDatabase()
    {
        // Arrange
        var treatment = new Treatment
        {
            Name = "Initial Treatment",
            Description = "Initial Description",
            DurationInMinutes = 30,
            Price = 25,
        };
        await _context.Treatments.AddAsync(treatment);
        await _context.SaveChangesAsync();

        treatment.Name = "Updated Treatment";
        treatment.Price = 30;
        treatment.Description = "Updated Description";

        // Act
        var result = await _sut.UpdateTreatmentAsync(treatment, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Treatment", result.Name);
        Assert.Equal(30, result.Price);
        Assert.Equal("Updated Description", result.Description);
        var treatmentInDb = await _context.Treatments.FindAsync(treatment.Id);
        Assert.NotNull(treatmentInDb);
        Assert.Equal(treatment.Name, treatmentInDb!.Name);
        Assert.Equal(treatment.Description, treatmentInDb.Description);
        Assert.Equal(treatment.DurationInMinutes, treatmentInDb.DurationInMinutes);
        Assert.Equal(treatment.Price, treatmentInDb.Price);
    }

    [Fact]
    public async Task DeleteTreatmentAsync_ShouldRemoveTreatmentFromDatabase()
    {
        // Arrange
        var treatment = new Treatment
        {
            Name = "To Be Deleted",
            Description = "To Be Deleted Description",
            DurationInMinutes = 45,
            Price = 40,
        };
        await _context.Treatments.AddAsync(treatment);
        await _context.SaveChangesAsync();

        // Act
        await _sut.DeleteTreatmentAsync(treatment, CancellationToken.None);

        // Assert
        var treatmentInDb = await _context.Treatments.FindAsync(treatment.Id);
        Assert.Null(treatmentInDb);
    }

    [Fact]
    public async Task GetTreatmentAsync_ShouldReturnTreatment_WhenTreatmentExists()
    {
        // Arrange
        var treatment = new Treatment
        {
            Name = "Existing Treatment",
            Description = "Existing Description",
            DurationInMinutes = 60,
            Price = 55,
        };
        await _context.Treatments.AddAsync(treatment);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetTreatmentAsync(treatment.Id, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(treatment.Name, result!.Name);
        Assert.Equal(treatment.Description, result.Description);
        Assert.Equal(treatment.DurationInMinutes, result.DurationInMinutes);
        Assert.Equal(treatment.Price, result.Price);
    }

    [Fact]
    public async Task GetTreatmentAsync_ShouldReturnNull_WhenTreatmentDoesNotExist()
    {
        // Arrange
        var nonExistentId = 999;

        // Act
        var result = await _sut.GetTreatmentAsync(nonExistentId, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetTreatmentsAsync_ShouldReturnAllTreatments()
    {
        // Arrange
        var treatment1 = new Treatment
        {
            Name = "Treatment 1",
            Description = "Description 1",
            DurationInMinutes = 30,
            Price = 30,
        };
        var treatment2 = new Treatment
        {
            Name = "Treatment 2",
            Description = "Description 2",
            DurationInMinutes = 60,
            Price = 60,
        };
        await _context.Treatments.AddRangeAsync(treatment1, treatment2);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetTreatmentsAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(
            result,
            t =>
                t.Name == treatment1.Name
                && t.Description == treatment1.Description
                && t.DurationInMinutes == treatment1.DurationInMinutes
                && t.Price == treatment1.Price
        );
        Assert.Contains(
            result,
            t =>
                t.Name == treatment2.Name
                && t.Description == treatment2.Description
                && t.DurationInMinutes == treatment2.DurationInMinutes
                && t.Price == treatment2.Price
        );
    }

    [Fact]
    public async Task GetTreatmentsAsync_ShouldReturnEmptyList_WhenNoTreatmentsExist()
    {
        // Arrange

        // Act
        var result = await _sut.GetTreatmentsAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
