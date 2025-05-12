using appointly.DAL.Entities;

namespace appointly.DAL.Repositories.IRepositories;

public interface ITreatmentRepository
{
    Task<Treatment> CreateTreatmentAsync(Treatment treatment, CancellationToken cancellationToken);
    Task<Treatment?> GetTreatmentByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<Treatment>> GetAllTreatmentsAsync(CancellationToken cancellationToken);
    Task<bool> DeleteTreatmentByIdAsync(int id, CancellationToken cancellationToken);
}
