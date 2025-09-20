using appointly.DAL.Context;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace appointly.DAL.Repositories;

public class StaffRepository(ApplicationDbContext context) : IStaffRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Staff> CreateStaffAsync(Staff staff, CancellationToken cancellationToken)
    {
        await _context.Staff.AddAsync(staff, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return staff;
    }

    public async Task<List<Staff>> GetStaffAsync(CancellationToken cancellationToken)
    {
        var staff = await _context.Staff.AsNoTracking().ToListAsync(cancellationToken);
        return staff;
    }
}
