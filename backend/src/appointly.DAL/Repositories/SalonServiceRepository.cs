using appointly.DAL.Context;
using appointly.DAL.Entities;
using appointly.DAL.Repositories.IRepositories;

namespace appointly.DAL.Repositories;

public class SalonServiceRepository(ApplicationDbContext context) : ISalonServiceRespository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<SalonService> CreateSalonServiceAsync(SalonService salonService)
    {
        await _context.SalonServices.AddAsync(salonService);
        await _context.SaveChangesAsync();
        return salonService;
    }
}
