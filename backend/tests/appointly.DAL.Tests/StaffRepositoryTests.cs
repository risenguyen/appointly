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
    public async Task CreateStaffMemberAsync_ShouldAddStaffToDatabase()
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
        var result = await _sut.CreateStaffMemberAsync(staff, CancellationToken.None);

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

    [Fact]
    public async Task GetStaffMemberAsync_ShouldReturnStaff_WhenStaffExists()
    {
        // Arrange
        var staff = new Staff { FirstName = "John", LastName = "Doe" };
        await _context.Staff.AddAsync(staff);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetStaffMemberAsync(staff.Id, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(staff.Id, result.Id);
        Assert.Equal(staff.FirstName, result.FirstName);
    }

    [Fact]
    public async Task GetStaffMemberAsync_ShouldReturnNull_WhenStaffDoesNotExist()
    {
        // Arrange

        // Act
        var result = await _sut.GetStaffMemberAsync(1, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteStaffMemberAsync_ShouldRemoveStaffFromDatabase()
    {
        // Arrange
        var staff = new Staff { FirstName = "John", LastName = "Doe" };
        await _context.Staff.AddAsync(staff);
        await _context.SaveChangesAsync();

        // Act
        await _sut.DeleteStaffMemberAsync(staff, CancellationToken.None);

        // Assert
        var staffInDb = await _context.Staff.FindAsync(staff.Id);
        Assert.Null(staffInDb);
    }

    [Fact]
    public async Task UpdateStaffMemberAsync_ShouldUpdateStaffInDatabase()
    {
        // Arrange
        var staff = new Staff
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Phone = "1234567890",
        };
        await _context.Staff.AddAsync(staff);
        await _context.SaveChangesAsync();

        staff.FirstName = "Jane";
        staff.Email = "jane.doe@example.com";

        // Act
        var result = await _sut.UpdateStaffMemberAsync(staff, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Jane", result.FirstName);
        Assert.Equal("jane.doe@example.com", result.Email);

        var staffInDb = await _context.Staff.FindAsync(staff.Id);
        Assert.NotNull(staffInDb);
        Assert.Equal("Jane", staffInDb!.FirstName);
        Assert.Equal("jane.doe@example.com", staffInDb.Email);
    }
}
