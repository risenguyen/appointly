using appointly.DAL.Context;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace appointly.DAL.Repositories;

public class TreatmentRepository(ApplicationDbContext context) : ITreatmentRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Treatment> CreateTreatmentAsync(
        Treatment treatment,
        CancellationToken cancellationToken
    )
    {
        await _context.Treatments.AddAsync(treatment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return treatment;
    }

    public async Task<bool> DeleteTreatmentAsync(int id, CancellationToken cancellationToken)
    {
        var treatment = await _context.Treatments.SingleOrDefaultAsync(
            t => t.Id == id,
            cancellationToken
        );
        if (treatment == null)
        {
            return false;
        }

        _context.Treatments.Remove(treatment);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Treatment?> GetTreatmentAsync(int id, CancellationToken cancellationToken)
    {
        var treatment = await _context
            .Treatments.AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
        return treatment;
    }

    public async Task<List<Treatment>> GetTreatmentsAsync(CancellationToken cancellationToken)
    {
        var treatments = await _context.Treatments.AsNoTracking().ToListAsync(cancellationToken);
        return treatments;
    }
}
