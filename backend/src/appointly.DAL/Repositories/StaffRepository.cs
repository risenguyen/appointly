using appointly.DAL.Context;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace appointly.DAL.Repositories;

public class StaffRepository(ApplicationDbContext context) : IStaffRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Staff> CreateStaffMemberAsync(
        Staff staff,
        CancellationToken cancellationToken
    )
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

    public async Task<Staff?> GetStaffMemberAsync(int id, CancellationToken cancellationToken)
    {
        var staffMember = await _context
            .Staff.AsNoTracking()
            .SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
        return staffMember;
    }

    public async Task<Staff> UpdateStaffMemberAsync(
        Staff staff,
        CancellationToken cancellationToken
    )
    {
        _context.Staff.Update(staff);
        await _context.SaveChangesAsync(cancellationToken);
        return staff;
    }

    public async Task DeleteStaffMemberAsync(Staff staff, CancellationToken cancellationToken)
    {
        _context.Staff.Remove(staff);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
