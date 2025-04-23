using appointly.DAL.Entities;

namespace appointly.DAL.Repositories.IRepositories;

public interface ISalonServiceRespository
{
    Task<SalonService> CreateSalonServiceAsync(SalonService salonService);
}
