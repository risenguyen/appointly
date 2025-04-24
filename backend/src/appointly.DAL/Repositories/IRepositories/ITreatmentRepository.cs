using appointly.DAL.Entities;

namespace appointly.DAL.Repositories.IRepositories;

public interface ITreatmentRepository
{
    Task<Treatment> CreateTreatmentAsync(Treatment treatment);
    Task<Treatment?> GetTreatmentByIdAsync(int id);
}
