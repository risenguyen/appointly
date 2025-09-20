using appointly.DAL.Context;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;

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
}
