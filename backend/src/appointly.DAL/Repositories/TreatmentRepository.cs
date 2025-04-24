using appointly.DAL.Context;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace appointly.DAL.Repositories;

public class TreatmentRepository(ApplicationDbContext context) : ITreatmentRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Treatment> CreateTreatmentAsync(Treatment treatment)
    {
        await _context.Treatments.AddAsync(treatment);
        await _context.SaveChangesAsync();
        return treatment;
    }

    public async Task<Treatment?> GetTreatmentByIdAsync(int id)
    {
        var treatment = await _context
            .Treatments.AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id == id);
        return treatment;
    }
}
