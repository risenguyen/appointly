using appointly.DAL.Context;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;

namespace appointly.DAL.Repositories;

public class TreatmentRepository(ApplicationDbContext context) : ITreatmentRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Treatment> CreateSalonServiceAsync(Treatment treatment)
    {
        await _context.Treatments.AddAsync(treatment);
        await _context.SaveChangesAsync();
        return treatment;
    }
}
