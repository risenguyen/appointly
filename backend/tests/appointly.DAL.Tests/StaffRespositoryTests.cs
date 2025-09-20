using appointly.DAL.Context;
using appointly.DAL.Entities;
using appointly.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace appointly.DAL.Tests;

public class StaffRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly StaffRepository _sut;

    public StaffRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _sut = new StaffRepository(_context);
    }

    [Fact]
    public async Task CreateStaffAsync_ShouldAddStaffToDatabase()
    {
        // Arrange
        var staff = new Staff
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Phone = "1234567890",
        };

        // Act
        var result = await _sut.CreateStaffAsync(staff, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Id);
        var staffInDb = await _context.Staff.FindAsync(result.Id);
        Assert.NotNull(staffInDb);
        Assert.Equal(staff.FirstName, staffInDb!.FirstName);
        Assert.Equal(staff.LastName, staffInDb.LastName);
        Assert.Equal(staff.Email, staffInDb.Email);
        Assert.Equal(staff.Phone, staffInDb.Phone);
    }

    [Fact]
    public async Task GetStaffAsync_ShouldReturnAllStaff()
    {
        // Arrange
        var staff1 = new Staff { FirstName = "John", LastName = "Doe" };
        var staff2 = new Staff { FirstName = "Jane", LastName = "Smith" };
        await _context.Staff.AddRangeAsync(staff1, staff2);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetStaffAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(result, s => s.FirstName == "John");
        Assert.Contains(result, s => s.FirstName == "Jane");
    }

    [Fact]
    public async Task GetStaffAsync_ShouldReturnEmptyList_WhenNoStaffExist()
    {
        // Arrange

        // Act
        var result = await _sut.GetStaffAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
